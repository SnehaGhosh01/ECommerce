using ECommerceApplication.DTO;
using ECommerceApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderForUserRepository repo;
        public OrderDetailsController(IOrderForUserRepository repo_)
        {
            repo = repo_;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string userId,[FromBody] PaymentMethodDto payment)
        {
            //string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {

                //int id = (payment.Method == "E-Wallet") ? 1 : 0;

                OrderDetailsShowDto res = await repo.PlaceOrder(payment, userId);
                if (res != null)
                {
                    return Ok(res);
                }
            }
            return BadRequest("Something Went wrong");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder(string userId)
        {
            //string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
               List< OrderListsForUserDTO> list = await repo.ShowAllOrders(userId);
                if (list != null)
                {
                    return Ok(list);
                }
                else { return BadRequest("No Orders Found"); }
            }
            return Ok();
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetExactOrder(string userId,[FromRoute]Guid id)
        {
           // string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var list = await repo.ShowExactOrder(userId, id);
                if (list != null)
                {
                    return Ok(list);
                }
                else { return BadRequest("No Orders Found"); }
            }
            return Ok();
        }
    }
}
