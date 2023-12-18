using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfEmptyDeliveryDal : EfEntityRepositoryBase<EmptyDelivery, AvenSellContext>, IEmptyDeliveryDal
    {

    }
}

