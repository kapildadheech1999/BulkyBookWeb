using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context; 
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context; 
            Categories = new CategoryRepository(context);
            Covers = new CoverRepository(context);
            Products = new ProductRepository(context);
            Companies = new CompanyRepository(context);
            ApplicationUsers = new ApplicationUserRepository(context);   
            ShoppingCarts = new ShoppingCartRepository(context);
            OrderHeaders = new OrderHeaderRepository(context);
            OrderDetails = new OrderDetailRepository(context);
        }
        public ICategoryRepository Categories { get; private set; }

        public ICoverRepository Covers { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderHeaderRepository OrderHeaders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
