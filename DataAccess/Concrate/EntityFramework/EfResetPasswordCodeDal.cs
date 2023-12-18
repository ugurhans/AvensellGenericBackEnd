using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfResetPasswordCodeDal : EfEntityRepositoryBase<ResetPasswordCode,AvenSellContext>,IResetPasswordCodeDal
    {
        ResetPasswordCode IResetPasswordCodeDal.Add(ResetPasswordCode resetPassword)
        {
            using (var context = new AvenSellContext())
            {
                var reset = new ResetPasswordCode
                {
                    UserId = resetPassword.UserId,
                    ExpirationDate = resetPassword.ExpirationDate,
                    Code = resetPassword.Code,



                };

                var result = context.ResetPasswordCodes.Add(reset);
                context.SaveChanges();
                return result.Entity;
            }
        }

    }
}
