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
    public class EfCouponCategoryDal : EfEntityRepositoryBase<CouponCategory,AvenSellContext> , ICategoryCouponDal 
    {
        public List<CouponCategory> GetAllDto(Expression<Func<CouponCategory, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.couponCategories
                             join p in context.Products
                             on c.ProductId equals p.Id
                             select new CouponCategory()
                             {
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 StartDate = c.StartDate,
                                 CategoryId = c.CategoryId,
                                 Code = c.Code,
                                 CouponImageUrl=c.CouponImageUrl,
                                 Discount = c.Discount,
                                 MinBasketCost = c.MinBasketCost,
                                 Name = c.Name,

                             };
                return filter == null

                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
