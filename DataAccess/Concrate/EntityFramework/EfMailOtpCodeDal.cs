using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;

namespace DataAccess.Concrate.EntityFramework;

public class EfMailOtpCodeDal: EfEntityRepositoryBase<MailOtpCode, AvenSellContext>, IMailOtpCodeDal
{
    
}