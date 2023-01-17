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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext context;  
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context; 
        }


        public void Update(Category category)
        {
            context.Update(category);
        }
    }
}
