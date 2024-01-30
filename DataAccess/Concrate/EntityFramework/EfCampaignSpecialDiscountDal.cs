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
    public class EfCampaignSpecialDiscountDal : EfEntityRepositoryBase<CampaignSpecialDiscount, AvenSellContext>, ICampaignSpecialDiscountDal
    {
        public List<CampaignSpecialDiscount> GetAllDto(Expression<Func<CampaignSpecialDiscount, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
