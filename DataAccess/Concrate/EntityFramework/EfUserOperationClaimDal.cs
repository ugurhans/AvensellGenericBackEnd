using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, AvenSellContext>, IUserOperationClaimsDal
    {
        public void DeleteRange(int userId)
        {
            using AvenSellContext context = new AvenSellContext();
            context.UserOperationClaims.RemoveRange(context.UserOperationClaims.Where(uoc => uoc.UserId == userId));
            context.SaveChanges();
        }
    }
}
