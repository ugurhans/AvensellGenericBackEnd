using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ISliderService
    {
        IDataResult<List<Slider>> GetAll();

        IResult Update(Slider kgVariable);
        IResult Add(Slider kgVariable);

    }
}

