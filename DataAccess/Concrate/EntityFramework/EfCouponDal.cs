﻿using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCouponDal : EfEntityRepositoryBase<Coupon, AvenSellContext>, ICouponDal
    {
    }
}

