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
    public interface ICampaignGiftProductDal : IEntityRepository<CampaignGiftProduct>
    {

        List<CampaignGiftProduct> GetAllDto(Expression<Func<CampaignGiftProduct, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignGiftProducts.RemoveRange(context.CampaignGiftProducts.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}
