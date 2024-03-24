using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfSubCategoryDal : EfEntityRepositoryBase<SubCategory, AvenSellContext>, ISubCategoryDal
    {
            
            
        public List<SubCategory> GetAllWithCategoryId(int categoryId)
        {
            using var context = new AvenSellContext();

            var subCategories = context.SubCategories.Where(x => x.CategoryId == categoryId)
                .Include(sc => sc.Products) 
                .Where(sc => sc.Products != null && sc.Products.Any())
                .ToList();

            return subCategories;

        }
        public List<SubCategoryDto> GetAllDto()
        {
            using var context = new AvenSellContext();

            var subCategories = context.SubCategories.Include(sc => sc.Products)
                         .ThenInclude(p => p.Reviews)
                          .Include(sc => sc.Products)
                         .ThenInclude(p => p.Brand)
                 .Select(sc => new SubCategoryDto()
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
                 }).ToList();

            return subCategories;

        }

        public List<SubCategoryDto> GetAllDtoWithCategoryId(int categoyId)
        {
            using var context = new AvenSellContext();

            var subCategories = context.SubCategories.Where(sc => sc.CategoryId == categoyId).Include(sc => sc.Products)
                         .ThenInclude(p => p.Reviews)
                          .Include(sc => sc.Products)
                         .ThenInclude(p => p.Brand)
                .Select(sc => new SubCategoryDto()
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
                }).ToList();

            return subCategories;
        }

        public List<SubCategoryDto> SearchProductWithSubCategory(string searchString)
        {
            using (var context = new AvenSellContext())
            {
                var subCategories = context.SubCategories
                     .Include(sc => sc.Products)
                         .ThenInclude(p => p.Reviews)
                     .Include(sc => sc.Products)
                         .ThenInclude(p => p.Brand)
                     .Select(sc => new SubCategoryDto()
                     {
                         Id = sc.Id,
                         Name = sc.Name,
                         OrderBy = sc.OrderBy,
                         Products = sc.Products
                             .Select(p => new ProductSimpleDto
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
                     }).ToList();

                return subCategories.Where(sc =>
                        sc.Name.Contains(searchString) ||
                        sc.Products.Any(p =>
                            p.Name.Contains(searchString) ||
                            p.BrandName.Contains(searchString)
                        )
                    ).ToList();

            }
        }
    }
}
