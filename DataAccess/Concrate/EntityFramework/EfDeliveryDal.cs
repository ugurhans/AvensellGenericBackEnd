using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.Dto;

namespace DataAccess.Concrate
{
    public class EfDeliveryDal : EfEntityRepositoryBase<Delivery, AvenSellContext>, IDeliveryDal
    {

    }
}

