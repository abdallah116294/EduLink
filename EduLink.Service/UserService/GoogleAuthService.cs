using EduLink.Core.Entities;
using EduLink.Core.IServices.UserService;
using EduLink.Repository.Data;
using EduLink.Utilities.DTO;
using EduLink.Utilities.DTO.User;
using EduLink.Utilities.Helpers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EduLink.Service.UserService
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly UserManager<User>_userManager;
        private readonly EduLinkDbContext _context;
        private readonly GoogleAuthConfig _googleAuthConfig;

        public GoogleAuthService(UserManager<User> userManager, EduLinkDbContext context, IOptions<GoogleAuthConfig> googleAuthConfig)
        {
            _userManager = userManager;
            _context = context;
            _googleAuthConfig = googleAuthConfig.Value;
            if(FirebaseApp.DefaultInstance == null)
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Firebase", "edulink-d8baa-firebase-adminsdk-fbsvc-be811598c0.json");
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path), // Download from Firebase Console
                    ProjectId = "edulink-d8baa"
                });
            }
        }

        public async Task<ResponseDTO<User>> GoogleSignIn(GoogleSignInVM model)
        {
            try
            {
                // 🔹 Use Firebase Admin SDK to verify the token
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(model.IdToken);

                // 🔹 Add debugging to see what data we're getting
                Console.WriteLine($"Decoded Token UID: {decodedToken.Uid}");
                Console.WriteLine($"Claims Count: {decodedToken.Claims.Count}");

                foreach (var claim in decodedToken.Claims)
                {
                    Console.WriteLine($"Claim: {claim.Key} = {claim.Value}");
                }

                var userToBeCreated = new CreateUserFromSocialLogin
                {
                    FirstName = decodedToken.Claims.ContainsKey("given_name") ? decodedToken.Claims["given_name"].ToString() :
                               decodedToken.Claims.ContainsKey("name") ? decodedToken.Claims["name"].ToString().Split(' ').FirstOrDefault() ?? "" : "",

                    LastName = decodedToken.Claims.ContainsKey("family_name") ? decodedToken.Claims["family_name"].ToString() :
                              decodedToken.Claims.ContainsKey("name") ? decodedToken.Claims["name"].ToString().Split(' ').LastOrDefault() ?? "" : "",

                    Email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : "",

                    ProfilePicture = decodedToken.Claims.ContainsKey("picture") ? decodedToken.Claims["picture"].ToString() : "",

                    LoginProviderSubject = decodedToken.Uid,
                };

                // 🔹 Add debugging for user creation data
                Console.WriteLine($"User to be created:");
                Console.WriteLine($"FirstName: '{userToBeCreated.FirstName}'");
                Console.WriteLine($"LastName: '{userToBeCreated.LastName}'");
                Console.WriteLine($"Email: '{userToBeCreated.Email}'");
                Console.WriteLine($"LoginProviderSubject: '{userToBeCreated.LoginProviderSubject}'");

                // 🔹 Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(userToBeCreated.Email);
                if (existingUser != null)
                {
                    Console.WriteLine($"User already exists with email: {userToBeCreated.Email}");

                    // Check if this user already has Google login linked
                    var logins = await _userManager.GetLoginsAsync(existingUser);
                    var googleLogin = logins.FirstOrDefault(l => l.LoginProvider == "Google");

                    if (googleLogin != null)
                    {
                        Console.WriteLine("Google login already linked to existing user");
                        return new ResponseDTO<User>
                        {
                            IsSuccess = true,
                            Message = "Google Sign In Successful (existing user)",
                            Data = existingUser,
                        };
                    }
                    else
                    {
                        // Link Google account to existing user
                        var linkResult = await _userManager.AddLoginAsync(existingUser, new UserLoginInfo("Google", userToBeCreated.LoginProviderSubject, "Google"));
                        if (linkResult.Succeeded)
                        {
                            Console.WriteLine("Successfully linked Google account to existing user");
                            return new ResponseDTO<User>
                            {
                                IsSuccess = true,
                                Message = "Google account linked to existing user",
                                Data = existingUser,
                            };
                        }
                        else
                        {
                            Console.WriteLine($"Failed to link Google account: {string.Join(", ", linkResult.Errors.Select(e => e.Description))}");
                        }
                    }
                }

                // 🔹 Try creating new user
                Console.WriteLine("Attempting to create new user from social login...");
                var user = await _userManager.CreateUserFromSocialLogin(_context, userToBeCreated, LoginProviders.Google);

                if (user is not null)
                {
                    Console.WriteLine($"Successfully created user with ID: {user.Id}");
                    return new ResponseDTO<User>
                    {
                        IsSuccess = true,
                        Message = "Google Sign In Successful",
                        Data = user,
                    };
                }
                else
                {
                    Console.WriteLine("CreateUserFromSocialLogin returned null");

                    // 🔹 Try manual user creation as fallback
                    var newUser = new User
                    {
                        UserName = userToBeCreated.Email,
                        Email = userToBeCreated.Email,
                        FullName = $"{userToBeCreated.FirstName} {userToBeCreated.LastName}".Trim(),
                        EmailConfirmed = true,
                        CreateAt = DateTime.UtcNow,
                        Role = 0, // Set appropriate default role
                        PhoneNumber = "" // Set default or get from token if available
                    };

                    var createResult = await _userManager.CreateAsync(newUser);
                    if (createResult.Succeeded)
                    {
                        // Add Google login info
                        var loginInfo = new UserLoginInfo("Google", userToBeCreated.LoginProviderSubject, "Google");
                        await _userManager.AddLoginAsync(newUser, loginInfo);

                        Console.WriteLine($"Manually created user with ID: {newUser.Id}");
                        return new ResponseDTO<User>
                        {
                            IsSuccess = true,
                            Message = "Google Sign In Successful (manual creation)",
                            Data = newUser,
                        };
                    }
                    else
                    {
                        Console.WriteLine($"Manual user creation failed: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                        return new ResponseDTO<User>
                        {
                            IsSuccess = false,
                            Message = $"Failed to create user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GoogleSignIn: {ex}");
                return new ResponseDTO<User>
                {
                    IsSuccess = false,
                    Message = $"Failed to authenticate with Firebase. {ex.Message}"
                };
            }
        }
    }
}
