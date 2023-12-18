using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.Dto;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfAddressDal : EfEntityRepositoryBase<Address, AvenSellContext>, IAddressDal
    {
        public List<AddressDto> GetAllAdresses(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from a in context.Addresses
                             join u in context.Users
                             on a.UserId equals u.Id
                             where a.UserId == userId
                             select new AddressDto
                             {
                                 Id = a.Id,
                                 ApartmentNo = a.ApartmentNo,
                                 BuildingNo = a.BuildingNo,
                                 City = a.City,
                                 Country = a.Country,
                                 DateCreated = a.DateCreated,
                                 DateModified = a.DateModified,
                                 Desc = a.Desc,
                                 Floor = a.Floor,
                                 Header = a.Header,
                                 IsActive = a.IsActive,
                                 Latitude = a.Latitude,
                                 Longitude = a.Longitude,
                                 Neighborhood = a.Neighborhood,
                                 Phone = a.Phone,
                                 Type = a.Type,
                                 UserId = a.UserId

                             };
                return result.ToList();
            }
        }

        public void DeleteRange(int userId)
        {
            using AvenSellContext context = new AvenSellContext();
            context.Addresses.RemoveRange(context.Addresses.Where(a => a.UserId == userId));
            context.SaveChanges();

        }
    }
}

