using Core.DataAccess;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface IAddressDal : IEntityRepository<Address>
    {
        public List<AddressDto> GetAllByUserId(int userId);
        public void DeleteRange(int userId);
        public List<CityTable> GetAllCity();
        public List<DistrictTable> GetAllDistrictWithCityId(int cityId);
        public List<MuhitTable> GetAllMuhitWithDistrictId(int districtId);
        public List<NeighbourhoodTable> GetAllNeighbourhoodWithMuhitId(int muhitId);

    }
}

