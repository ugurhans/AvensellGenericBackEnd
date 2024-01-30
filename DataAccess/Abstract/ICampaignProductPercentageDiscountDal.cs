using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Concrate.paytr;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICampaignProductPercentageDiscountDal : IEntityRepository<CampaignProductPercentageDiscount>
    {

        List<CampaignProductPercentageDiscount> GetAllDto(Expression<Func<CampaignProductPercentageDiscount, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignProductGroups.RemoveRange(context.CampaignProductGroups.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}
