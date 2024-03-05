using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignGiftProductDal : EfEntityRepositoryBase<CampaignGiftProduct, AvenSellContext>, ICampaignGiftProductDal
    {
        public List<CampaignGiftProduct> GetAllDto(Expression<Func<CampaignGiftProduct, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from c in context.CampaignGiftProducts
                        
                             join pS in context.Products
                             on c.GiftProductId equals pS.Id
                             select new CampaignGiftProduct()
                             {
                               
                                 Id = c.Id,
                                 EndDate = c.EndDate,
                                 IsActive = c.IsActive,
                                 StartDate = c.StartDate,
                                 MinPurchaseAmount = c.MinPurchaseAmount,
                                 CampaignDetail = c.CampaignDetail,
                                 CampaignImageUrl = c.CampaignImageUrl,
                                 CampaignName = c.CampaignName,
                                 GiftProductId = c.GiftProductId,
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
