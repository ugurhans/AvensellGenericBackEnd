using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Entity.Dto;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        IAddressDal _adressDal;

        public AddressManager(IAddressDal adressDal)
        {
            _adressDal = adressDal;
        }

        public IDataResult<List<AddressDto>> GetAll(int userId)
        {
            return new SuccessDataResult<List<AddressDto>>(_adressDal.GetAllAdresses(userId));
        }

        public IDataResult<Address> GetSelectedAddress(int addressId)
        {
            return new SuccessDataResult<Address>(_adressDal.Get(a => a.Id == addressId));
        }

        public IResult Add(Address address)
        {
            address.DateCreated = DateTime.Now;
            address.IsActive = true;
            _adressDal.Add(address);

            return new SuccessResult(Messages.Added);
        }

        public IResult Update(Address address)
        {
            address.DateModified = DateTime.Now;
            _adressDal.Update(address);
            return new SuccessResult(Messages.Updated);
        }

        public IResult SetIsActive(int id)
        {
            var address = _adressDal.Get(x => x.Id == id);
            if (address != null)
            {
                address.DateModified = DateTime.Now;
                address.IsActive = false;
                _adressDal.Update(address);
                return new SuccessResult("Adres Listeden Kaldırıldı.");
            }
            else
            {
                return new ErrorResult("Adres Bulunamadı");
            }

        }

        public IResult Delete(int id)
        {
            _adressDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
