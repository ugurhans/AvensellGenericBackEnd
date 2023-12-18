using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dto;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserFavoriteDal : EfEntityRepositoryBase<UserFavorite, AvenSellContext>, IUserFavoriteDal
    {
        public List<UserFavoriteDto> GetByUserId(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from uf in context.UserFavorites
                             where uf.UserId == userId
                             join p in context.Products on uf.ProductId equals p.Id
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join sc in context.SubCategories on p.SubCategoryId equals sc.Id
                             select new UserFavoriteDto()
                             {
                                 Name = p.Name,
                                 UnitPrice = p.UnitPrice,
                                 CategoryId = p.CategoryId,
                                 CategoryName = c.Name,
                                 Discount = p.Discount,
                                 ProductId = p.Id,
                                 Id = p.Id,
                                 BrandName = b.Name,
                                 SubCategoryName = sc.Name,
                                 ImageUrl = p.ImageUrl,
                                 SubCategoryId = p.SubCategoryId,
                                 UnitCount = p.UnitCount,
                                 UnitDiscount = p.Discount,
                                 UnitPaidPrice = p.UnitPrice - p.Discount,
                                 UnitQuantity = p.UnitQuantity,
                                 UnitType = p.UnitType,
                             };
                return result.ToList();
            }
        }

        public List<UserFavoriteDto> GetByUserIdAndCategoryId(int userId, int categoryId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from p in context.Products.Where(p => p.CategoryId == categoryId)
                             join uf in context.UserFavorites.Where(u => u.UserId == userId) on p.Id equals uf.ProductId into fav
                             from uf in fav.DefaultIfEmpty()

                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             select new UserFavoriteDto()
                             {

                             };
                return result.ToList();
            }
        }
        public List<UserFavoriteDto> GetAllTopFiveDto(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from p in context.Products.Where(p => p.IsFeatured == true)
                             join uf in context.UserFavorites.Where(u => u.UserId == userId) on p.Id equals uf.ProductId into fav
                             from uf in fav.DefaultIfEmpty()
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             select new UserFavoriteDto()
                             {

                             };
                return result.ToList();
            }
        }

        public void DeleteRange(int userId)
        {
            using AvenSellContext context = new AvenSellContext();
            context.UserFavorites.RemoveRange(context.UserFavorites.Where(uf => uf.UserId == userId));
            context.SaveChanges();
        }
    }
}
