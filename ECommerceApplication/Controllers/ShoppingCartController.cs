using ECommerceApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository_) {
            shoppingCartRepository = shoppingCartRepository_;

        }
        [HttpGet]
       // [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll(string userId)
        {
           // var userId = HttpContext.Session.GetString("UserId");
            var list = await shoppingCartRepository.GetAllAsync(userId);
            if (list == null)
            {
                return Ok("Something went wrong");
            }
            var name = "lol";
            return Ok(list);
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> AddToCart(string userId,[FromRoute] Guid id)
        {
           // var userId = HttpContext.Session.GetString("UserId");
            var res = await shoppingCartRepository.AddAsync(id, userId);
            if (res == null)
            {
                Ok("Something went wrong");
            }
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteItem(string userId, [FromRoute] Guid id)
        {
            //string userId = HttpContext.Session.GetString("UserId");
            var name = "lol";
            var list = shoppingCartRepository.DeleteAsync(id, userId);
            
            return Ok(list);
        }
        [HttpPut("IncreaseCountByone/{id:Guid}")]
        public async Task<IActionResult> UpdatePlusitem(string userId,[FromRoute] Guid id)
        {
            //string userId = HttpContext.Session.GetString("UserId");
            var list = await shoppingCartRepository.UpdateCountPlusAsync(id, userId);
            if (list == null)
            {
                return Ok("getting null");
            }
            return Ok(list);
        }

        [HttpPut("DecreaseCountByone/{id:Guid}")]
        public async Task<IActionResult> UpdateMinusitem(string userId,[FromRoute] Guid id)
        {
           // string userId = HttpContext.Session.GetString("UserId");
            var list = await shoppingCartRepository.UpdateCountMinusAsync(id, userId);
            if (list == null)
            {
                return Ok("getting null");
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(string userId,[FromRoute] Guid id)
        {
            //string userId= HttpContext.Session.GetString("UserId");
            var list=shoppingCartRepository.GetByIdAsync(id,userId);
            if(list == null)
            {
                Ok("Something wrong");
            }
            return Ok(list);
        }
    }
}
