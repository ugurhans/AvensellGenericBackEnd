using System;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using Business.Abstract;
using Core.Entities.Concrete;
using Entity.Concrate;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Concrate
{
    public static class FakeDataGenerator
    {
        private static readonly Random _random = new Random();

        public static List<Product> GenerateProducts(int count, int subId, int cId)
        {


            var products = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.UnitsInStock, f => f.Random.Number(0, 1000))
                .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.CategoryId, cId)
                .RuleFor(p => p.Discount, f => f.Random.Decimal(0, 1))
                .RuleFor(p => p.IsFeatured, f => f.Random.Bool())
                .RuleFor(p => p.IsActive, f => f.Random.Bool())
                .RuleFor(p => p.OrderBy, f => f.IndexFaker + 1)
                .RuleFor(p => p.SubCategoryId, subId)
                .RuleFor(p => p.BrandId, f => f.Random.Number(1, 20))
                .RuleFor(p => p.UnitType, f => f.Random.Number(1, 5))
                .RuleFor(p => p.UnitQuantity, f => f.Random.Number(1, 10))
                .RuleFor(p => p.UnitCount, f => f.Random.Number(1, 100))
                .RuleFor(p => p.ImageUrl, f => f.Image.Food())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.CreatedDate, f => f.Date.Past(2))
                .RuleFor(p => p.ModifiedDate, f => f.Date.Past())
                .RuleFor(p => p.Manufacturer, f => f.Company.CompanyName())
                .RuleFor(p => p.Rating, f => f.Random.Double(0, 5))
                .Generate(count);

            return products;
        }

        public static List<Brand> GenerateBrands(int count)
        {
            var brands = new Faker<Brand>()
                .RuleFor(b => b.Name, f => f.Company.CompanyName())
                .RuleFor(b => b.LogoUrl, f => f.Image.LoremPixelUrl())
                .RuleFor(b => b.Description, f => f.Lorem.Sentence())
                .Generate(count);

            return brands;
        }

        public static List<Review> GenerateReviews(int count, int productId)
        {
            var reviews = new Faker<Review>()
                .RuleFor(r => r.Comment, f => f.Lorem.Sentence())
                .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                .RuleFor(r => r.ProductId, productId)
                .Generate(count);

            return reviews;
        }


        public static List<Category> GenerateCategories(int count)
        {
            var categories = new Faker<Category>()
                .RuleFor(c => c.OrderBy, f => f.Random.Int(1, 100))
                .RuleFor(c => c.Name, f => f.Commerce.Department())
                .RuleFor(c => c.ImageUrl, f => f.Internet.Url())
                .Generate(count);

            //foreach (var c in categories)
            //{
            //    c.SubCategories = GenerateSubCategories(_random.Next(1, 5), c.Id);
            //}

            return categories;
        }

        public static List<SubCategory> GenerateSubCategories(int count, int categoryId)
        {
            var subCategories = new Faker<SubCategory>()
                .RuleFor(sc => sc.CategoryId, categoryId)
                .RuleFor(sc => sc.Name, f => f.Commerce.ProductName())

                .Generate(count);
            //foreach (var c in subCategories)
            //{
            //    c.Products = GenerateProducts(_random.Next(5, 20), c.Id, c.CategoryId);

            //}

            return subCategories;
        }

    }
}

