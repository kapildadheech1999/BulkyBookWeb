using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICoverRepository Covers { get; }
        IProductRepository Products { get; }
        ICompanyRepository Companies { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IShoppingCartRepository ShoppingCarts { get;}
        IOrderHeaderRepository OrderHeaders { get; }
        IOrderDetailRepository OrderDetails { get; }
        void Save();
    }
}
