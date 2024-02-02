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
        public async Task<IActionResult> PlaceOrder([FromBody] PaymentMethodDto payment)
        {
            string userId = HttpContext.Session.GetString("UserId");
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
        public async Task<IActionResult> GetAllOrder()
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var list = await repo.ShowAllOrders(userId);
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
        public async Task<IActionResult> GetExactOrder([FromRoute]Guid orderId)
        {
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                var list = await repo.ShowExactOrder(userId, orderId);
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
