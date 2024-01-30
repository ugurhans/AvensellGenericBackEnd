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
    public interface ITimedCouponDal : IEntityRepository<CouponTimed>
    {


        public void DeleteRange(int Id)
        {
            using AvenSellContext context = new AvenSellContext();
            context.couponTimeds.RemoveRange(context.couponTimeds.Where(a => a.Id == Id));
            context.SaveChanges();

        }
    }
}
