using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class SliderManager : ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public IResult Add(Slider slider)
        {
            slider.IsActive = true;
            _sliderDal.Add(slider);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<List<Slider>> GetAll()
        {
            return new SuccessDataResult<List<Slider>>(_sliderDal.GetAll(x => x.IsActive == true));
        }

        public IResult Update(Slider slider)
        {
            _sliderDal.Update(slider);
            return new SuccessResult(Messages.Updated);
        }
    }
}

