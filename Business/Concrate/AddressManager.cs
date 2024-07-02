using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Business.Constants;
using Entity.Dto;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal adressDal)
        {
            _addressDal = adressDal;
        }

        public IDataResult<List<AddressDto>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<AddressDto>>(_addressDal.GetAllByUserId(userId));
        }


        public IDataResult<Address> GetById(int addressId)
        {
            return new SuccessDataResult<Address>(_addressDal.Get(x => x.Id == addressId));
        }
        public IDataResult<List<CityTable>> GetAllCity()
        {
            return new SuccessDataResult<List<CityTable>>(_addressDal.GetAllCity());

        }

        public IResult Add(Address address)
        {
            address.DateCreated = DateTime.Now;
            address.IsActive = true;
            _addressDal.Add(address);

            return new SuccessResult(Messages.Added);
        }

        public IResult Update(Address address)
        {
            address.DateModified = DateTime.Now;
            _addressDal.Update(address);
            return new SuccessResult(Messages.Updated);
        }


        public IResult Delete(int id)
        {
            _addressDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IResult SetIsActive(int id)
        {
            var address = _addressDal.Get(x => x.Id == id);
            if (address != null)
            {
                if (address.IsActive == false)
                {
                    address.DateModified = DateTime.Now;
                    address.IsActive = true;
                    _addressDal.Update(address);
                    return new SuccessResult("Adres Aktifleştirildi");

                }
                address.DateModified = DateTime.Now;
                address.IsActive = false;
                _addressDal.Update(address);
                return new SuccessResult("Adres Listeden Kaldırıldı.");
            }
            else
            {
                return new ErrorResult("Adres Bulunamadı");
            }

        }

        public IDataResult<List<DistrictTable>> GetAllDistrictWithCityId(int cityId)
        {
            return new SuccessDataResult<List<DistrictTable>>(_addressDal.GetAllDistrictWithCityId(cityId));
        }

        public IDataResult<List<MuhitTable>> GetAllMuhitWithDistrictId(int districtId)
        {
            return new SuccessDataResult<List<MuhitTable>>(_addressDal.GetAllMuhitWithDistrictId(districtId));

        }

        public IDataResult<List<NeighbourhoodTable>> GetAllNeighbourhoodWithMuhitId(int muhitId)
        {
            return new SuccessDataResult<List<NeighbourhoodTable>>(_addressDal.GetAllNeighbourhoodWithMuhitId(muhitId));

        }

    }
}
