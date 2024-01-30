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
    public interface ICampaignSpecialDiscountDal : IEntityRepository<CampaignSpecialDiscount>
    {

        List<CampaignSpecialDiscount> GetAllDto(Expression<Func<CampaignSpecialDiscount, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignSpecialDiscounts.RemoveRange(context.CampaignSpecialDiscounts.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}
