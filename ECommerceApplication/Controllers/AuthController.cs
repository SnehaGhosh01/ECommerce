﻿using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using ECommerceApplication.Repository;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailService emailService;
        public AuthController(UserManager <ApplicationUser> userManager, ITokenRepository tokenRepository_, IEmailService _emailService) {
            this.userManager= userManager;
            this.tokenRepository= tokenRepository_;
            this.emailService= _emailService;
        }
        //Post: /api/Auth/Register
       [HttpPost]
        [Route("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] VenderRegisterRequestDto registerRequestDto)
        {
            var applicationUser = new ApplicationUser
            {
                Address = registerRequestDto.Address,
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
                Name=registerRequestDto.Name,
                PhoneNumber=registerRequestDto.Phone_Number
            };

            var identityResult = await userManager.CreateAsync(applicationUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                //Add Roles to this user
               identityResult= await userManager.AddToRoleAsync(applicationUser, "User");
                if(identityResult.Succeeded)
                {
                    string message = $"Dear {registerRequestDto.Name},\n\n" +
    "You have successfully registered as a User to ECommerce Application Successfully!!!!\n\n" +
    "Happy shopping...\n\n" +
    "Customer Care Manager,\n" +
    "John Doe";
                    await emailService.SendRegistrationEmailAsync(registerRequestDto.Email,"Registration Confirmation",message);
                    return Ok("User registered successfully");
                }
            }
            return BadRequest();
       }
        [HttpPost]
        [Route("VenderRegister")]
        public async Task<IActionResult> VenderRegister([FromBody] VenderRegisterRequsetDto venderregisterRequestDto)
        {
            var applicationUser = new ApplicationUser
            {
                Address = venderregisterRequestDto.Address,
                UserName = venderregisterRequestDto.Email,
                Email = venderregisterRequestDto.Email,
                Name = venderregisterRequestDto.Name,
                PhoneNumber = venderregisterRequestDto.Phone_Number,
                VenderLicenceNumber=venderregisterRequestDto.LicenceNumber
            };

            var identityResult = await userManager.CreateAsync(applicationUser, venderregisterRequestDto.Password);
            if (identityResult.Succeeded)
            {
                //Add Roles to this user
                identityResult = await userManager.AddToRoleAsync(applicationUser, "Vender");
                if (identityResult.Succeeded)
                {
                    string message = $"Dear {venderregisterRequestDto.Name},\n\n" +
                        "Congratulations!!! We have verified your vender Licence."+
    "You have successfully registered  as a Vender to ECommerce Application Successfully!!!!\n\n" +
    "Happy Journey...\n\n" +
    "Customer Care Manager,\n" +
    "John Doe";
                    await emailService.SendRegistrationEmailAsync(venderregisterRequestDto.Email, "Registration Confirmation", message);
                    return Ok("Vender registered successfully");
                }
            }
            return BadRequest();
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            
            
           var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        
                        
                        HttpContext.Session.SetString("UserId",user.Id);
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
