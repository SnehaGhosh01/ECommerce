using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using F23.StringSimilarity;

namespace ECommerceApplication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceAuthDbContext db;
        public ProductRepository(ECommerceAuthDbContext db_)
        {
            db= db_;
        }


        public async Task<List<Product>> SearchByProductName(string name)
        {
            name = name.ToUpper();
            List<Product> products= new List<Product>();
            products = await db.Products
                .Where(c => c.ProductName.ToUpper().StartsWith(name) || (c.ProductName.ToUpper().Contains(name) && !c.ProductName.ToUpper().StartsWith(name)))
                .OrderByDescending(c => c.ProductName.ToUpper().StartsWith(name))
                .ThenBy(c => c.ProductName.ToUpper())
                .ToListAsync();
            products.AddRange(await SearchByName(name));
            return products;
        }

        public async Task<List<Product>> SearchByName(string name)
        {
            name = name.ToUpper();

            var categories = await db.Categories
                .Where(c => c.Name.ToUpper().StartsWith(name) || (c.Name.ToUpper().Contains(name) && !c.Name.ToUpper().StartsWith(name)) || c.subCategory.ToUpper().StartsWith(name) || (c.subCategory.ToUpper().Contains(name) && !c.subCategory.ToUpper().StartsWith(name)))
                .OrderByDescending(c => c.Name.ToUpper().StartsWith(name))
                .ThenBy(c => c.Name.ToUpper())
                .ToListAsync();

            var products = new List<Product>();

            foreach (var category in categories)
            {
                var categoryId = category.Id;

                var categoryProducts = await db.Products
                    .Where(p => p.CategoryId == categoryId)
                    .ToListAsync();

                products.AddRange(categoryProducts);
            }

            return products;
        }



        public async Task<Product> CreateAsync(ProductCreationDto product)
        {
            Product newProduct=new Product();
            var category = await db.Categories.FirstOrDefaultAsync(c=>c.Name.ToUpper() ==product.CategoryName.ToUpper() && c.subCategory.ToUpper()==product.SubCategoryName.ToUpper());
            if(category == null)
            {
                return null;
            }
            var vender = await db.ApplicationUsers.FirstOrDefaultAsync(c => c.Email == product.VenderEmail);
            if (vender == null)
            {
                return null;
            }
            newProduct.ProductName = product.ProductName;
            newProduct.Description= product.Description;
            newProduct.Price= product.Price;
            newProduct.Stock= product.Stock;
            newProduct.ImageUrl= product.ImageUrl;
            newProduct.CategoryId = category.Id;
            newProduct.VenderId = vender.Id;
            await db.Products.AddAsync(newProduct);
            await db.SaveChangesAsync();
            return newProduct;

        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            var existingProduct = await db.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (existingProduct == null)
            {
                return null;
            }
            db.Products.Remove(existingProduct);
            await db.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
           // var list = new List<ProductCreationDto>();
            var list1 = await db.Products.ToListAsync();
            
           
            return list1;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await db.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            return product;
        }

        public async Task<Product?> UpdateAsync(Guid id, ProductCreationDto product)
        {
            Product newProduct = await db.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Name.ToUpper() == product.CategoryName.ToUpper()&&c.subCategory.ToUpper()==product.SubCategoryName.ToUpper());
            //var vender = await db.ApplicationUsers.FirstOrDefaultAsync(c => c.Email == product.VenderEmail);
            newProduct.ProductName = product.ProductName;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.Stock = product.Stock;
            newProduct.ImageUrl = product.ImageUrl;
            newProduct.CategoryId = category.Id;
           // newProduct.VenderId = vender.Id;

            await db.SaveChangesAsync();
            return newProduct;
        }
        
        public async Task<List<Product>> GetSuggestedProduct(string categoryname, string subcategory, int count)
        {

            var categoryId = await db.Categories
    .FirstOrDefaultAsync(c => c.Name.ToUpper() == categoryname.ToUpper() && c.subCategory.ToUpper() == subcategory.ToUpper());

            if (categoryId != null)
            {
                var randomProducts = await db.Products
                    .Where(p => p.CategoryId == categoryId.Id)
                    .OrderBy(p => Guid.NewGuid())
                    .Take(count)
                    .ToListAsync();
                return randomProducts;
            }
            else
            {
                // Handle the case when the category is not found
                return new List<Product>();
            }
        }

    }
}
