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
        public List<AddressDto> GetAllByUserId(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from a in context.Addresses
                             join u in context.Users
                             on a.UserId equals u.Id
                             join c in context.CityTable
                             on a.CityId equals c.CityId
                             join d in context.DistrictTable
                             on a.DistrictId equals d.DistrictId
                             join m in context.MuhitTable
                             on a.MuhitId equals m.MuhitId
                             join n in context.NeighbourhoodTable
                             on a.NeighborhoodId equals n.NeighbourhoodId
                             where a.UserId == userId
                             select new AddressDto
                             {
                                 Id = a.Id,
                                 ApartmentNo = a.ApartmentNo,
                                 BuildingNo = a.BuildingNo,
                                 CityId = a.CityId,
                                 DistrictId = a.DistrictId,
                                 DateCreated = a.DateCreated,
                                 DateModified = a.DateModified,
                                 Desc = a.Desc,
                                 Floor = a.Floor,
                                 Header = a.Header,
                                 IsActive = a.IsActive,
                                 Latitude = a.Latitude,
                                 Longitude = a.Longitude,
                                 NeighborhoodId = a.NeighborhoodId,
                                 Phone = a.Phone,
                                 Type = a.Type,
                                 UserId = a.UserId,
                                 CityName = c.CityName,
                                 DistrictName = d.DistrictName,
                                 MuhitName = m.MuhitName,
                                 NeighborhoodName = n.NeighbourhoodName,
                                 MuhitId = a.MuhitId
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

        public List<CityTable> GetAllCity()
        {
            using var _context = new AvenSellContext();
            var city = _context.CityTable.ToList();
            return city;
        }
        public List<DistrictTable> GetAllDistrictWithCityId(int cityId)
        {
            using var _context = new AvenSellContext();
            var districts = _context.DistrictTable.Where(x => x.CityId == cityId).ToList();
            return districts;
        }

        public List<MuhitTable> GetAllMuhitWithDistrictId(int districtId)
        {
            using var _context = new AvenSellContext();
            var Neighborhood = _context.MuhitTable.Where(x => x.DistrictId == districtId).ToList();
            return Neighborhood;
        }

        public List<NeighbourhoodTable> GetAllNeighbourhoodWithMuhitId(int muhitId)
        {
            using var _context = new AvenSellContext();
            var Neighbourhood = _context.NeighbourhoodTable.Where(x => x.MuhitId == muhitId).ToList();
            return Neighbourhood;
        }


    }
}

