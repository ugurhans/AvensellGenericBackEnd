using System;
using Core.DataAccess;
using Entity.Concrate;

namespace DataAccess.Abstract
{
    public interface IOrderItemDal : IEntityRepository<OrderItem>
    {
    }
}

