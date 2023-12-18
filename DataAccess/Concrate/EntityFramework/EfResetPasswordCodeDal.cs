using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfResetPasswordCodeDal : EfEntityRepositoryBase<ResetPasswordCode, AvenSellContext>, IResetPasswordCodeDal
    {

    }
}
