using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        AppDbContext db = new AppDbContext();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = db.Set<T>();
        }

        public void Delete(T item)
        {
            _object.Remove(item);
            db.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _object.ToList();
        }

        public T GetById(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T item)
        {
            _object.Add(item);
            db.SaveChanges();
        }

        public void Update(T item)
        {
            _object.Update(item);
            db.SaveChanges();
        }
    }
}
