using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFProductDal : GenericRepository<Product>, IProductDal
    {
        public new List<Product> GetAll()
        {
            using (var c = new AppDbContext())
            {
                return c.Products.Include(x => x.Category).ToList();
            }
        }

        public new Product GetById(Expression<Func<Product, bool>> filter)
        {
            using (var c = new AppDbContext())
            {
                return c.Products.Include(x => x.Category).SingleOrDefault(filter);
            }
        }
    }
}
