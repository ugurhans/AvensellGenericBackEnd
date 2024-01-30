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
    public class EfCampaignCombinedDiscountDal : EfEntityRepositoryBase<CampaignCombinedDiscount, AvenSellContext>, ICampaignCombinedDiscountDal
    {
        public List<CampaignCombinedDiscount> GetAllDto(Expression<Func<CampaignCombinedDiscount, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
