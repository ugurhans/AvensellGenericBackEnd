using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IAddressService
    {
        public IDataResult<List<AddressDto>> GetAllByUserId(int userId);
        public IDataResult<Address> GetById(int addressId);
        public IResult Add(Address address);
        public IResult Update(Address address);
        public IResult Delete(int id);
        public IResult SetIsActive(int id);
        public IDataResult<List<CityTable>> GetAllCity();
        public IDataResult<List<DistrictTable>> GetAllDistrictWithCityId(int CityId);
        public IDataResult<List<MuhitTable>> GetAllMuhitWithDistrictId(int DistrictId);
        public IDataResult<List<NeighbourhoodTable>> GetAllNeighbourhoodWithMuhitId(int MuhitId);

    }

}
