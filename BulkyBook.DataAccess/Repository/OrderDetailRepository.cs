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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext context;  
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context; 
        }


        public void Update(OrderDetail orderDetail)
        {
            context.Update(orderDetail);
        }
    }
}
