using System;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IDataResult<List<AddressDto>> GetAll(int userId);
        IDataResult<Address> GetSelectedAddress(int addressId);
        IResult Add(Address address);
        IResult Update(Address address);
        IResult Delete(int id);
        IResult SetIsActive(int id);
    }

}
