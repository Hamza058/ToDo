using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void TAdd(Product t)
        {
            _productDal.Insert(t);
        }

        public void TDelete(Product t)
        {
            _productDal.Delete(t);
        }

        public Product TGetById(int id)
        {
            return _productDal.GetById(x => x.ProductId == id);
        }

        public List<Product> TGetList()
        {
            return _productDal.GetAll();
        }

        public void TUpdate(Product t)
        {
            _productDal.Update(t);
        }
    }
}
