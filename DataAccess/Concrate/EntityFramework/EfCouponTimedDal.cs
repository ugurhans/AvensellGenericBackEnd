using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCouponTimedDal : EfEntityRepositoryBase<CouponTimed, AvenSellContext>, ITimedCouponDal
    {
        public List<CouponTimed> GetAllDto(Expression<Func<CouponTimed, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.couponTimeds
                             join p in context.Products
                             on c.CombinedProduct equals p.Id.ToString()
                             select new CouponTimed()
                             {
                                 Id = c.Id,
                                 IsActive = c.IsActive,
                                 CategoryId = c.CategoryId,
                                 StartTime = c.StartTime,
                                 Code = c.Code,
                                 CombinedProduct = c.CombinedProduct,
                                 CouponImageUrl=c.CouponImageUrl,
                                 Discount = c.Discount,
                                 MinBasketCost = c.MinBasketCost,
                                 EndTime = c.EndTime,
                                 Name = c.Name,
              
                             };
                return filter == null

                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
