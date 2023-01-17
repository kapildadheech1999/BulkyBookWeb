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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext context; 
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context; 
        }
        public void Update(Company company)
        {
           context.Update(company);
        }
    }
}
