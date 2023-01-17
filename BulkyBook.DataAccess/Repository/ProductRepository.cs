using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context; 
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context; 
        }
        public void Update(Product product)
        {
            var productFromDb = context.Products.FirstOrDefault(x => x.Id == product.Id);
            if(product != null)
            {
                productFromDb.Title = product.Title;    
                productFromDb.ISBN = product.ISBN;
                productFromDb.Description = product.Description;    
                productFromDb.Price = product.Price;    
                productFromDb.CategoryId = product.CategoryId;  
                productFromDb.CoverId = product.CoverId;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.Price100 = product.Price100;  
                productFromDb.Price50 = product.Price50;  
                productFromDb.Author = product.Author;
                if(product.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;  
                }
            }
        }
    }
}
