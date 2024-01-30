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
    public interface ICategoryCouponDal :IEntityRepository<CouponCategory>
    {
        public void DeleteRange(int Id)
        {
            using AvenSellContext context = new AvenSellContext();
            context.couponCategories.RemoveRange(context.couponCategories.Where(a => a.Id == Id));
            context.SaveChanges();

        }

    }
}
