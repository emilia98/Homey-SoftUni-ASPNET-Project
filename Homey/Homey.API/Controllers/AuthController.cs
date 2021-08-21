using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homey.Data.Models;
using Homey.InputModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Homey.API.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var user = await GetUserByEmail(loginInputModel.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    ErrorMsg = "Invalid username!"
                });
            }

            var loginResult = await signInManager.CheckPasswordSignInAsync(user, loginInputModel.Password, false);

            if (!loginResult.Succeeded)
            {
                return Unauthorized(new
                {
                    ErrorMsg = "Invalid username or password!"
                });
            }

            return Ok("Welcome back!");
        }

        public async Task<IActionResult> Register(RegisterInputModel registerInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var user = await GetUserByEmail(registerInputModel.Email);

            if (user != null)
            {
                return BadRequest(new { ErrorMsg = "This email is already taken!" });
            }

            var newUser = new ApplicationUser
            {
                Email = registerInputModel.Email,
                UserName = registerInputModel.Username
            };

            var registerResult = await userManager.CreateAsync(newUser, registerInputModel.Password);

            if (!registerResult.Succeeded)
            {
                return BadRequest(new { ErrorMsg = "An error occurred while trying to register!" });
            }

            var roleResult = await userManager.AddToRoleAsync(newUser, "User");

            if (!roleResult.Succeeded)
            {
                return BadRequest(new { ErrorMsg = "An error occurred while trying to register!" });
            }

            var loginResult = await signInManager.CheckPasswordSignInAsync(newUser, registerInputModel.Password, false);

            if (!loginResult.Succeeded)
            {
                return Unauthorized(new
                {
                    ErrorMsg = "Error while logging in!"
                });
            }

            return Ok("Successfully registered in the app!");
        }

        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
