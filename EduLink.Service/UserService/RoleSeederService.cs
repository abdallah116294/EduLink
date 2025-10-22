using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service.UserService
{
    public class RoleSeederService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleSeederService> _logger;

        public RoleSeederService(
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleSeederService> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedRolesAsync()
        {
            try
            {
                _logger.LogInformation("Starting role seeding...");

                var roles = new[] { "ADMIN", "ACADEMIC", "NONACADEMIC", "STUDENT", "PARENT" };

                foreach (var roleName in roles)
                {
                    try
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(roleName);
                        
                        if (!roleExists)
                        {
                            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                            
                            if (result.Succeeded)
                            {
                                _logger.LogInformation($"Role '{roleName}' created successfully.");
                            }
                            else
                            {
                                _logger.LogError($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                            }
                        }
                        else
                        {
                            _logger.LogInformation($"Role '{roleName}' already exists.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error creating role '{roleName}'");
                    }
                }

                _logger.LogInformation("Role seeding completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error during role seeding");
                throw;
            }
        }
    }
}
