using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICampaignCategoryPercentageDiscountDal  : IEntityRepository<CampaignCategoryPercentageDiscount>
    {
        List<CampaignCategoryPercentageDiscount> GetAllDto(Expression<Func<CampaignCategoryPercentageDiscount, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignCategoryPercentageDiscounts.RemoveRange(context.CampaignCategoryPercentageDiscounts.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}
