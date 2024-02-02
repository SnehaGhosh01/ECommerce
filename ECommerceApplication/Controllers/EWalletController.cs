using ECommerceApplication.DTO;
using ECommerceApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Org.BouncyCastle.Ocsp;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EWalletController : ControllerBase
    {
        private readonly IUserWalletRepository repo;
        public EWalletController(IUserWalletRepository repo_)
        {
            repo = repo_; 
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserWalletRegisterDto req)
        {
           var userId= HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                UserWalletRegisterDto res =await repo.Registration(req, userId);
                if(res != null)
                {
                    return Ok("Successfully registered");
                }
                else
                {
                    return Ok("You already have an e-wallet.Use that");
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("RechargeWallet")]
        public async Task<IActionResult> Recharge([FromBody] WalletRechargeDto req)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                string res = await repo.RechargeWallet(req, userId);
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDto req)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                string res = await repo.ChangePasswordForWallet(req, userId);
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto req)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                string res = await repo.ForgetPassword(req, userId);
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("CheckBalance")]
        public async Task<IActionResult> CheckBalance()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                string res = await repo.CheckBalance(userId);
                
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
