using System;
using Core.Utilities.Results;
using Entity.Entities;

namespace Business.Abstract
{
    public interface IDistrictsService
    {
        IResult Add(District district);
        IResult Update(District district);
        IDataResult<District> GetDistrict(int districtId);
        IDataResult<List<District>> GetAllDistricts(int cityId);
        IDataResult<List<District>> GetAllDistrictsWithCities(List<int> cityIds);
    }
}

