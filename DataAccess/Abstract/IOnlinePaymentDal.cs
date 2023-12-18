using System;
using Core.DataAccess;
using Entity.Concrete;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface IOnlinePaymentDal : IEntityRepository<OnlinePayment>
    {
    }
}

