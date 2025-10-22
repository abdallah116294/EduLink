using EduLink.API.Extensions;
using EduLink.Core.IServices.UserService;
using EduLink.Service.NotificationService.Hubs;
using EduLink.Service.UserService;
using EduLink.Utilities.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EduLink.API
{
    public class Program
    {
        public static async  Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()    // allow all domains
                        .AllowAnyMethod()    // allow all HTTP methods (GET, POST, etc.)
                        .AllowAnyHeader();   // allow all headers
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerEx();
            var configure = builder.Configuration;
            builder.Services.AddAppServices(configure);
            builder.Services.AddConnectionString(configure);
            //Google Auth Config
            builder.Services.Configure<GoogleAuthConfig>(configure.GetSection("Google"));
            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            #region Authentication  
            var JWTSection = configure.GetSection("JWT");
            var secretKey = JWTSection["Key"];
            var issuer = JWTSection["ValidIssuer"];
            var audience = JWTSection["ValidAudience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            #endregion
            #region AddCors for SingnalR 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("SignalRCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:4200") // Add your frontend URLs
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
            #endregion
            var app = builder.Build();
             var wwwRootPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
 if (!Directory.Exists(wwwRootPath))
 {
     Directory.CreateDirectory(wwwRootPath);
 }
 //Custom middleware to handle exception and log them 
 app.Use(async (context, next) =>
 {
     try
     {
         await next.Invoke();
     }
     catch (Exception ex)
     {
         var logFilePath = Path.Combine(wwwRootPath, "log.txt");

         // Capture Request Data
         var url = context.Request.Path + context.Request.QueryString;
         var method = context.Request.Method;
         var queryParams = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "None";

         // Read request body (if any)
         string bodyContent = "No Body";
         if (context.Request.ContentLength > 0 &&
             (method == "POST" || method == "PUT" || method == "PATCH"))
         {
             context.Request.EnableBuffering();
             using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
             bodyContent = await reader.ReadToEndAsync();
             context.Request.Body.Position = 0; // Reset position for next middleware
         }

         // Prepare Log Entry in Table Format
         var logMessage = new StringBuilder();
         logMessage.AppendLine("====================================================================");
         logMessage.AppendLine($"Date/Time   : {DateTime.Now}");
         logMessage.AppendLine($"HTTP Method : {method}");
         logMessage.AppendLine($"Request URL : {url}");
         logMessage.AppendLine($"Query Params: {queryParams}");
         logMessage.AppendLine($"Request Body: {bodyContent}");
         logMessage.AppendLine($"Exception   : {ex.Message}");
         logMessage.AppendLine($"Stack Trace : {ex.StackTrace}");
         logMessage.AppendLine("====================================================================");
         logMessage.AppendLine();

         await File.AppendAllTextAsync(logFilePath, logMessage.ToString());

         context.Response.StatusCode = 500;
         context.Response.ContentType = "application/json";
         await context.Response.WriteAsJsonAsync(new
         {
             error = "An unexpected error occurred. Please check the logs."
         });
     }
 });
            // Configure the HTTP request pipeline.
            #region Roles Seeder 
            using (var scope = app.Services.CreateScope())
            {
                var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeederService>();
                await roleSeeder.SeedRolesAsync();
            }
            #endregion
            
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseCors("SignalRCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #region SignalR
            app.MapHub<NotificationHub>("/service/notificationService/hubs/notificationHub");
            #endregion
            app.Run();
        }
    }
}
