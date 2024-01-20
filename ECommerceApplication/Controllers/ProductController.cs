
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using ECommerceApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository categoryRepository_)
        {
            productRepository = categoryRepository_;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //
            var list = await productRepository.GetAllAsync();
            //var productdto = new List<ProductCreationDto>();

            return Ok(list);
        }
        [HttpGet]
        [Authorize(Roles = "Vender")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var category = await productRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        [Authorize(Roles="Vender")]
        public async Task<IActionResult> Add([FromBody] ProductCreationDto productreq)
        {

            var prop=await productRepository.CreateAsync(productreq);
            if(prop == null)
            {
                return Ok("Vender Email or Category Name is invalid");
            }
            return Ok(prop);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductCreationDto updateproductreq)
        {
            //Map dto to domain model

            var category = await productRepository.UpdateAsync(id, updateproductreq);
            if (category == null) { return Ok("product Not Found"); }
            //Model to dto

            return Ok(category);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productModel = await productRepository.DeleteAsync(id);
            if (productModel == null) { return NotFound(); }
            return Ok(productModel);
        }

        [HttpGet("SearchBycategoryName")]
        public async Task<IActionResult> SearchBycategoryName([FromQuery] string name)
        {
            var productModel = await productRepository.SearchByName(name);
            if (productModel == null) { return NotFound(); }
            return Ok(productModel);
        }
        [HttpGet("SearchByProductName")]
        public async Task<IActionResult> SearchByProductName([FromQuery] string name)
        {
            var productModel = await productRepository.SearchByProductName(name);
            if (productModel == null) { return NotFound(); }
            return Ok(productModel);
        }


    }
}