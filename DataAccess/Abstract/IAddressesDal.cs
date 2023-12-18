using System;
using Core.DataAccess;
using Entity.Concrete;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface IAddressDal : IEntityRepository<Address>
    {
        public List<AddressDto> GetAllAdresses(int userId);
        void DeleteRange(int userId);

    }
}

