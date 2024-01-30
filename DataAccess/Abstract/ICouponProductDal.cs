using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductCouponDal :IEntityRepository<CouponProduct>
    {

     


        public void DeleteRange(int Id)
        {
            using AvenSellContext context = new AvenSellContext();
            context.couponProducts.RemoveRange(context.couponProducts.Where(a => a.Id == Id));
            context.SaveChanges();

        }
    }
}
