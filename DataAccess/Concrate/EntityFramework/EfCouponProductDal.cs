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
    public class EfCouponProductDal : EfEntityRepositoryBase<CouponProduct,AvenSellContext> , IProductCouponDal
    {
        public List<CouponProduct> GetAllDto(Expression<Func<CouponProduct, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.couponProducts
                             join pF in context.Products
                              on c.CombinedProduct equals pF.Id.ToString()
                             select new CouponProduct()
                             {
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 StartDate = c.StartDate,
                                 Code = c.Code,
                                 CombinedProduct = c.CombinedProduct,
                                 CouponImageUrl =c.CouponImageUrl,
                                 MinBasketCost = c.MinBasketCost,
                                 Discount = c.Discount,
                                 Name = c.Name,
                                 

                             };
                return filter == null

                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
