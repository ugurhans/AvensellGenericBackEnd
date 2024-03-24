using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Entities;


namespace DataAccess.Concrate.EntityFramework
{
    public class EfCityDal : EfEntityRepositoryBase<City, AvenSellContext>, ICityDal
    {
    }
}

