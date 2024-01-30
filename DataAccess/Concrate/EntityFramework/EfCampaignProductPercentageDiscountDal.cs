using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate.paytr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCampaignProductPercentageDiscountDal : EfEntityRepositoryBase<CampaignProductPercentageDiscount, AvenSellContext>, ICampaignProductPercentageDiscountDal
    {
        public List<CampaignProductPercentageDiscount> GetAllDto(Expression<Func<CampaignProductPercentageDiscount, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
