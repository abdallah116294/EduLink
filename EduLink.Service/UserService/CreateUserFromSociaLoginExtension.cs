using EduLink.Core.Entities;
using EduLink.Repository.Data;
using EduLink.Utilities.DTO.User;
using Google;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Service.UserService
{
    public static class CreateUserFromSocialLoginExtension
    {
        /// <summary>
        /// Creates user from social login
        /// </summary>
        /// <param name="userManager">the usermanager</param>
        /// <param name="context">the context</param>
        /// <param name="model">the model</param>
        /// <param name="loginProvider">the login provider</param>
        /// <returns>System.Threading.Tasks.Task&lt;User&gt;</returns>

        public static async Task<User> CreateUserFromSocialLogin(
           this UserManager<User> userManager,
           EduLinkDbContext context,
           CreateUserFromSocialLogin model,
           LoginProviders loginProvider)
        {
            try
            {
                // Check if user already exists
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    // User exists, just add the login provider
                    var loginInfo = new UserLoginInfo(loginProvider.GetDisplayName(), model.LoginProviderSubject, loginProvider.GetDisplayName());
                    await userManager.AddLoginAsync(existingUser, loginInfo);
                    return existingUser;
                }

                // Create new user
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = $"{model.FirstName} {model.LastName}".Trim(),
                    EmailConfirmed = true,
                    CreateAt = DateTime.UtcNow,
                    Role = 0, // Set appropriate default role - this might be causing issues
                    PhoneNumber = string.Empty // Required field - might be causing issues if not set
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    // Add the external login
                    var loginInfo = new UserLoginInfo(loginProvider.GetDisplayName(), model.LoginProviderSubject, loginProvider.GetDisplayName());
                    await userManager.AddLoginAsync(user, loginInfo);

                    return user;
                }
                else
                {
                    Console.WriteLine($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CreateUserFromSocialLogin: {ex}");
                return null;
            }
        }
    }
}
