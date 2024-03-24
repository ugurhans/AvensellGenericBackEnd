using Core.Utilities.Results;
using Entity.Entities;

namespace Business.Abstract
{
    public interface ICityService
    {
        IResult Add(City city);
        IResult Update(City city);
        IDataResult<City> GetCity(int cityId);
        IDataResult<List<City>> GetAllCityList();
    }
}
