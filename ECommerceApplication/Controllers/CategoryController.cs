using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using ECommerceApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository_)
        {
            categoryRepository = categoryRepository_;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //
            var list = await categoryRepository.GetAllAsync();
            

            return Ok(list);
        }
       
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoriesRequestDto categoryreq)
        {
            var category = new Category
            {
                Name = categoryreq.Name,
                subCategory = categoryreq.SubCategory,
            };
            await categoryRepository.CreateAsync(category);

            return Ok(categoryreq);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoriesRequestDto categoryreq)
        {
            //Map dto to domain model
            var category = new Category
            {
                Name = categoryreq.Name,
                subCategory=categoryreq.SubCategory,
            };
            var categoryupdated = await categoryRepository.UpdateAsync(id, category);
            if (categoryupdated == null) { return NotFound(); }
            //Model to dto

            return Ok(categoryreq);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryModel = await categoryRepository.DeleteAsync(id);
            if (categoryModel == null) { return NotFound(); }
            return Ok(categoryModel);
        }
        
    }
}
