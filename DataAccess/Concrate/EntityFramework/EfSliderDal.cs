using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSliderDal : EfEntityRepositoryBase<Slider, AvenSellContext>, ISliderDal
    {

    }
}

