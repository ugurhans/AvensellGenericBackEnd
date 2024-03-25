using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, AvenSellContext>, ICategoryDal
    {
        public List<CategoryAndSubDto> GetallCategoryAndSubCategoriesDto()
        {
            using var context = new AvenSellContext();

            var categories = context.Categories
                 .Include(c => c.SubCategories.Where(sc => sc.Products != null && sc.Products
                         .Any()))
                 .Select(c => new CategoryAndSubDto
                 {
                     Id = c.Id,
                     OrderBy = c.OrderBy,
                     Name = c.Name,
                     ImageUrl = "https://kadimgross.com.tr/" + c.ImageUrl,
                     SubCategories = c.SubCategories.Where(sc => sc.Products != null && sc.Products
                        .Any()).Select(sc => new SubCategoryDto
                     {
                         Id = sc.Id,
                         Name = sc.Name,
                         OrderBy = sc.OrderBy,
                     }).ToList()
                 })  .Where(cat => cat.SubCategories.Any()).ToList();

            return categories;
        }

        public List<CategorySimpleDto> GetAllSimpleDtoDto()
        {
            using var context = new AvenSellContext();

            var categories = context.Categories
                 .Include(c => c.SubCategories)
                 .Select(c => new CategorySimpleDto
                 {
                     Id = c.Id,
                     OrderBy = c.OrderBy,
                     Name = c.Name,
                     ImageUrl = "https://kadimgross.com.tr/" + c.ImageUrl,

                 }).ToList();

            return categories;
        }

        public List<CategoryDtoWithSubAndProduct> GetDto()
        {
            using var context = new AvenSellContext();

            var categories = context.Categories
                 .Include(c => c.SubCategories.Where(sc => sc.Products != null &&sc.Products.Count>0&& sc.Products
                     .Any()))
                     .ThenInclude(sc => sc.Products)
                         .ThenInclude(p => p.Reviews)
                 .Include(c => c.SubCategories)
                     .ThenInclude(sc => sc.Products)
                     .ThenInclude(p => p.Brand)
             .Select(c => new CategoryDtoWithSubAndProduct
             {
                 Id = c.Id,
                 OrderBy = c.OrderBy,
                 Name = c.Name,
                 ImageUrl = "https://kadimgross.com.tr/" + c.ImageUrl,
                 SubCategories = c.SubCategories.Select(sc => new SubCategoryDto
                 {
                     Id = sc.Id,
                     Name = sc.Name,
                     OrderBy = sc.OrderBy,
                     Products = sc.Products.Select(p => new ProductSimpleDto
                     {
                         Id = p.Id,
                         Name = p.Name,
                         UnitPrice = p.UnitPrice,
                         Discount = p.Discount,
                         OrderBy = p.OrderBy,
                         BrandName = p.Brand.Name,
                         UnitType = p.UnitType,
                         UnitQuantity = p.UnitQuantity,
                         ImageUrl = p.ImageUrl,
                         PaidPrice = p.UnitPrice - p.Discount,
                         UnitCount = p.UnitCount,
                     }).ToList()
                 }).ToList()
             }).ToList();

            return categories;

        }
    }
}