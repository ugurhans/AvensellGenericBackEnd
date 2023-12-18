using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfPaymentTypeDal : EfEntityRepositoryBase<PaymentType, AvenSellContext>, IPaymentTypeDal
    {

    }
}

