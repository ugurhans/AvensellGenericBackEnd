using System;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;
using Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, AvenSellContext>, IProductDal
    {
        public List<ProductDto> GetAllDto()
        {

            return new List<ProductDto>();

        }

        public List<ProductDto> GetAllTopFiveDto()
        {
            throw new NotImplementedException();
        }

        public ProductDto GetDtoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductSimpleDto> GetDtoByCategoryId(int categoryId)
        {
            using var context = new AvenSellContext();
            var result = from p in context.Products
                         where p.CategoryId == categoryId
                         join b in context.Brands on p.BrandId equals b.Id
                         join c in context.Categories on p.CategoryId equals c.Id
                         join sc in context.SubCategories on p.SubCategoryId equals sc.Id
                         select new ProductSimpleDto()
                         {
                             Id = p.Id,
                             //BrandId = p.BrandId,
                             BrandName = b.Name,
                             //CategoryId = p.CategoryId,
                             //CategoryName = c.Name,
                             //Description = p.Description,
                             ImageUrl = p.ImageUrl,
                             Discount = p.Discount,
                             //IsActive = p.IsActive,
                             //IsFavorite = null, // eğer kullanıcının favorisi varsa true döner, yoksa false
                             //IsFeatured = p.IsFeatured,
                             //Manufacturer = p.Manufacturer,
                             Name = p.Name,
                             OrderBy = p.OrderBy,
                             PaidPrice = p.UnitPrice - p.Discount,
                             //Rating = p.Rating,
                             //Reviews = p.Reviews,
                             //SubCategoryId = p.SubCategoryId,
                             //SubCategoryName = sc.Name,
                             UnitCount = p.UnitCount,
                             UnitPrice = p.UnitPrice,
                             UnitQuantity = p.UnitQuantity,
                             //UnitsInStock = p.UnitsInStock,
                             UnitType = p.UnitType
                         };
            return result.ToList();
        }

        public ProductDto GetDetailWithId(int id, int? userId)
        {
            using var context = new AvenSellContext();
            var result = from p in context.Products
                         where p.Id == id
                         join b in context.Brands on p.BrandId equals b.Id
                         join c in context.Categories on p.CategoryId equals c.Id
                         join sc in context.SubCategories on p.SubCategoryId equals sc.Id

                         join uf in context.UserFavorites on p.Id equals uf.ProductId into favorites
                         from uf in favorites.DefaultIfEmpty()
                         where uf.UserId == userId || uf == null // kullanıcının favori ürünleri veya hiçbir kullanıcının favorisi olmayan ürünler

                         select new ProductDto()
                         {
                             Id = p.Id,
                             BrandId = p.BrandId,
                             BrandName = b.Name,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name,
                             CreatedDate = p.CreatedDate,
                             Description = p.Description,
                             ImageUrl = p.ImageUrl,
                             Discount = p.Discount,
                             IsActive = p.IsActive,
                             IsFavorite = uf != null, // eğer kullanıcının favorisi varsa true döner, yoksa false
                             IsFeatured = p.IsFeatured,
                             Manufacturer = p.Manufacturer,
                             ModifiedDate = p.ModifiedDate,
                             Name = p.Name,
                             OrderBy = p.OrderBy,
                             PaidPrice = p.UnitPrice - p.Discount,
                             Rating = p.Rating,
                             Reviews = p.Reviews,
                             SubCategoryId = p.SubCategoryId,
                             SubCategoryName = sc.Name,
                             UnitCount = p.UnitCount,
                             UnitPrice = p.UnitPrice,
                             UnitQuantity = p.UnitQuantity,
                             UnitsInStock = p.UnitsInStock,
                             UnitType = p.UnitType
                         };
            return result.SingleOrDefault();
        }


        public List<ProductDto> SearchProduct(string productName)
        {
            using (var context = new AvenSellContext())
            {
                var result = from p in context.Products
                             where p.Name.Contains(productName)
                             join b in context.Brands on p.BrandId equals b.Id
                             join c in context.Categories on p.CategoryId equals c.Id
                             join sc in context.SubCategories on p.SubCategoryId equals sc.Id

                             //join uf in context.UserFavorites on p.Id equals uf.ProductId into favorites
                             //from uf in favorites.DefaultIfEmpty()
                             //where uf.UserId == userId || uf == null // kullanıcının favori ürünleri veya hiçbir kullanıcının favorisi olmayan ürünler

                             select new ProductDto()
                             {
                                 Id = p.Id,
                                 BrandId = p.BrandId,
                                 BrandName = b.Name,
                                 CategoryId = p.CategoryId,
                                 CategoryName = c.Name,
                                 CreatedDate = p.CreatedDate,
                                 Description = p.Description,
                                 ImageUrl = p.ImageUrl,
                                 Discount = p.Discount,
                                 IsActive = p.IsActive,
                                 //IsFavorite = uf != null, // eğer kullanıcının favorisi varsa true döner, yoksa false
                                 IsFeatured = p.IsFeatured,
                                 Manufacturer = p.Manufacturer,
                                 ModifiedDate = p.ModifiedDate,
                                 Name = p.Name,
                                 OrderBy = p.OrderBy,
                                 PaidPrice = p.UnitPrice - p.Discount,
                                 Rating = p.Rating,
                                 Reviews = p.Reviews,
                                 SubCategoryId = p.SubCategoryId,
                                 SubCategoryName = sc.Name,
                                 UnitCount = p.UnitCount,
                                 UnitPrice = p.UnitPrice,
                                 UnitQuantity = p.UnitQuantity,
                                 UnitsInStock = p.UnitsInStock,
                                 UnitType = p.UnitType

                             };
                return result.ToList();
            }
        }

        public List<ProductSimpleDto> GetAllSimpleDto(Expression<Func<ProductSimpleDto, bool>> filter = null)
        {
            using var context = new AvenSellContext();
            var result = from p in context.Products
                         join b in context.Brands on p.BrandId equals b.Id
                         join c in context.Categories on p.CategoryId equals c.Id
                         join sc in context.SubCategories on p.SubCategoryId equals sc.Id
                         select new ProductSimpleDto()
                         {
                             Id = p.Id,
                             //BrandId = p.BrandId,
                             BrandName = b.Name,
                             //CategoryId = p.CategoryId,
                             //CategoryName = c.Name,
                             //Description = p.Description,
                             ImageUrl = p.ImageUrl,
                             Discount = p.Discount,
                             //IsActive = p.IsActive,
                             //IsFavorite = null, // eğer kullanıcının favorisi varsa true döner, yoksa false
                             //IsFeatured = p.IsFeatured,
                             //Manufacturer = p.Manufacturer,
                             Name = p.Name,
                             OrderBy = p.OrderBy,
                             PaidPrice = p.UnitPrice - p.Discount,
                             //Rating = p.Rating,
                             //Reviews = p.Reviews,
                             //SubCategoryId = p.SubCategoryId,
                             //SubCategoryName = sc.Name,
                             UnitCount = p.UnitCount,
                             UnitPrice = p.UnitPrice,
                             UnitQuantity = p.UnitQuantity,
                             //UnitsInStock = p.UnitsInStock,
                             UnitType = p.UnitType
                         };
            return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();

        }
    }
}

