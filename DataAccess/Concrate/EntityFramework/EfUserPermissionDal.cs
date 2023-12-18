using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfUserPermissionDal : EfEntityRepositoryBase<UserPermission, AvenSellContext>, IUserPermissionDal
    {

    }
}

