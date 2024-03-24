using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Entities;

namespace Business.Concrate
{
    public class DistrictManager : IDistrictsService
    {
        private readonly IDistrictDal _district;
        public DistrictManager(IDistrictDal districtDal)
        {
            _district = districtDal;
        }
        public IResult Add(District district)
        {
            _district.Add(district);
            return new SuccessResult();
        }

        public IDataResult<List<District>> GetAllDistricts(int cityId)
        {
            return new SuccessDataResult<List<District>>(_district.GetAll(x => x.CityId == cityId).OrderBy(x => x.Name).ToList());
        }

        public IDataResult<List<District>> GetAllDistrictsWithCities(List<int> cityIds)
        {
            return new SuccessDataResult<List<District>>(_district.GetAll(x => cityIds.Contains(x.CityId)).OrderBy(x => x.Name).ToList());
        }

        public IDataResult<District> GetDistrict(int districtId)
        {
            return new SuccessDataResult<District>(_district.Get(x => x.Id == districtId));
        }

        public IResult Update(District district)
        {
            var findDistrict = GetDistrict(district.Id);
            if (findDistrict.Data != null)
            {
                var datum = findDistrict.Data;
                datum = district;
                _district.Update(datum);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}

