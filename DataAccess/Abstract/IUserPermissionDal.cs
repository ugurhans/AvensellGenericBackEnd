using System;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entity.Concrate;

namespace DataAccess.Abstract
{
    public interface IUserPermissionDal : IEntityRepository<UserPermission>
    {
    }
}

