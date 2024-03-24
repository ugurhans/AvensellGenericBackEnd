using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Entities;

namespace Business.Concrate
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public IResult Add(City city)
        {
            _cityDal.Add(city);
            return new SuccessResult();
        }

        public IDataResult<List<City>> GetAllCityList()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetAll().OrderBy(x => x.Name).ToList());
        }

        public IDataResult<City> GetCity(int cityId)
        {
            return new SuccessDataResult<City>(_cityDal.Get(x => x.Id == cityId));
        }

        public IResult Update(City city)
        {
            var findDatum = GetCity(city.Id);
            if (findDatum.Data != null)
            {
                var datum = findDatum.Data;
                datum = city;
                _cityDal.Update(datum);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }

}

