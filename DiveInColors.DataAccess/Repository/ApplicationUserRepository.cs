using DiveInColors.DataAccess.Repository.IRepository;
using DiveInColors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiveInColors.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        //Get the ApplicationDBContext using Dependency Injection
        //Also pass the db context to the Repository base class (the Repository base class also expects the db context)
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
