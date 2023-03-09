using DiveInColors.DataAccess.Repository.IRepository;
using DiveInColors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiveInColors.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        //Get the ApplicationDBContext using Dependency Injection
        //Also pass the db context to the Repository base class (the Repository base class also expects the db context)
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            //_db.Products.Update(obj);
            //to update a single field
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id == obj.Id);
            if (objFromDb != null) 
            {
                objFromDb.Title= obj.Title;
                objFromDb.Dimension = obj.Dimension;
                objFromDb.Description = obj.Description;
                objFromDb.Artist = obj.Artist;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Currency = obj.Currency;
                objFromDb.InStock = true;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                objFromDb.CategoryId = obj.CategoryId;
                if (objFromDb !=null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
            }
        }
    }
}
