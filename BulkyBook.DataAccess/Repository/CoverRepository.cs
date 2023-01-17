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
    public class CoverRepository : Repository<Cover>, ICoverRepository
    {
        private readonly ApplicationDbContext context; 
        public CoverRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context; 
        }
        public void Update(Cover cover)
        {
           context.Update(cover);
        }
    }
}
