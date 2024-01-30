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
    public class EfCampaignGiftProductDal : EfEntityRepositoryBase<CampaignGiftProduct, AvenSellContext>, ICampaignGiftProductDal
    {
        public List<CampaignGiftProduct> GetAllDto(Expression<Func<CampaignGiftProduct, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
