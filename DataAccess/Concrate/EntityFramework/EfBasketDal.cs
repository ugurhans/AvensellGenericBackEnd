using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Entity.Dto;
using Entity.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, AvenSellContext>, IBasketDal
    {
        public BasketDetailDto GetBasketWithBasketId(int basketId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = context.Baskets
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .Where(b => b.Id == basketId)
                .Select(b => new BasketDetailDto
                {
                    BasketId = b.Id,
                    BasketItems = b.BasketItems.Select(bi => new BasketItemDto
                    {
                        Id = bi.Id,
                        BasketId = bi.BasketId,
                        ProductId = bi.ProductId,
                        Name = bi.Product.Name,
                        UnitPrice = bi.Product.UnitPrice,
                        UnitDiscount = bi.Product.UnitPrice - bi.Product.Discount,
                        UnitPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount)),
                        ProductCount = bi.ProductCount,
                        TotalPrice = (decimal)(bi.Product.UnitPrice * bi.ProductCount),
                        TotalPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount),
                        TotalDiscount = (decimal)(bi.Product.Discount * bi.ProductCount),
                        CategoryId = bi.Product.CategoryId,
                        CategoryName = (from c in context.Categories where c.Id == bi.Product.CategoryId select c).FirstOrDefault().Name,
                        SubCategoryId = bi.Product.SubCategoryId,
                        SubCategoryName = (from sc in context.SubCategories where sc.Id == bi.Product.SubCategoryId select sc).FirstOrDefault().Name,
                        BrandId = bi.Product.BrandId,
                        BrandName = bi.Product.Brand.Name,
                        UnitType = bi.Product.UnitType,
                        UnitQuantity = bi.Product.UnitQuantity,
                        UnitCount = bi.Product.UnitCount,
                        ImageUrl = bi.Product.ImageUrl
                    }).ToList(),
                    TotalBasketPrice = b.BasketItems.Sum(bi => bi.Product.UnitPrice * bi.ProductCount),
                    TotalBasketDiscount = b.BasketItems.Sum(bi => bi.Product.Discount * bi.ProductCount),
                    TotalBasketPaidPrice = b.BasketItems.Sum(bi => (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount)),
                    CampaignId = b.CampaignId,
                    CampaignType = b.CampaignType,
                    IsCampaignApplied = b.IsCampaignApplied,
                    CampaignDiscount = b.CampaignDiscount,
                    CouponId = b.CouponId,
                    CouponDiscount = b.CouponDiscount,
                    IsCouponApplied = b.IsCouponApplied,
                    UserId = b.UserId
                })
                .FirstOrDefault();

                return result;
            }
        }

        public BasketDetailDto GetBasketWithUserId(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = context.Baskets
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .Where(b => b.UserId == userId)
                .Select(b => new BasketDetailDto
                {
                    BasketId = b.Id,
                    BasketItems = b.BasketItems.Select(bi => new BasketItemDto
                    {
                        Id = bi.Id,
                        BasketId = bi.BasketId,
                        ProductId = bi.ProductId,
                        Name = bi.Product.Name,
                        UnitPrice = bi.Product.UnitPrice,
                        UnitDiscount = bi.Product.UnitPrice - bi.Product.Discount,
                        UnitPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount)),
                        ProductCount = bi.ProductCount,
                        TotalPrice = (decimal)(bi.Product.UnitPrice * bi.ProductCount),
                        TotalPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount),
                        TotalDiscount = (decimal)(bi.Product.Discount * bi.ProductCount),
                        CategoryId = bi.Product.CategoryId,
                        CategoryName = (from c in context.Categories where c.Id == bi.Product.CategoryId select c).FirstOrDefault().Name,
                        SubCategoryId = bi.Product.SubCategoryId,
                        SubCategoryName = (from sc in context.SubCategories where sc.Id == bi.Product.SubCategoryId select sc).FirstOrDefault().Name,
                        BrandId = bi.Product.BrandId,
                        BrandName = bi.Product.Brand.Name,
                        UnitType = bi.Product.UnitType,
                        UnitQuantity = bi.Product.UnitQuantity,
                        UnitCount = bi.Product.UnitCount,
                        ImageUrl = bi.Product.ImageUrl
                    }).ToList(),
                    TotalBasketPrice = b.BasketItems.Sum(bi => bi.Product.UnitPrice * bi.ProductCount),
                    TotalBasketDiscount = b.BasketItems.Sum(bi => bi.Product.Discount * bi.ProductCount),
                    TotalBasketPaidPrice = b.BasketItems.Sum(bi => (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount)),
                    CampaignId = b.CampaignId,
                    CampaignType = b.CampaignType,
                    IsCampaignApplied = b.IsCampaignApplied,
                    CampaignDiscount = b.CampaignDiscount,
                    CouponId = b.CouponId,
                    CouponDiscount = b.CouponDiscount,
                    IsCouponApplied = b.IsCouponApplied,
                    UserId = b.UserId
                })
                .FirstOrDefault();

                return result;
            }
        }

        public BasketSimpleDto GetSimpleByBasketId(int basketId)
        {
            using var context = new AvenSellContext();

            var result = context.Baskets
                 .Include(b => b.BasketItems)
                     .ThenInclude(bi => bi.Product)
                 .Where(b => b.UserId == basketId)
                 .Select(b => new BasketSimpleDto
                 {
                     BasketId = b.Id,
                     BasketItems = b.BasketItems.Select(bi => new BasketProductSimpleDto
                     {
                         Id = bi.Id,
                         ProductId = bi.ProductId,
                         Name = bi.Product.Name,
                         UnitPrice = bi.Product.UnitPrice,
                         UnitDiscount = bi.Product.UnitPrice - bi.Product.Discount,
                         UnitPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount)),
                         ProductCount = bi.ProductCount ?? 0,
                         CategoryId = bi.Product.CategoryId,
                         CategoryName = (from c in context.Categories where c.Id == bi.Product.CategoryId select c).FirstOrDefault().Name,
                         SubCategoryId = bi.Product.SubCategoryId,
                         SubCategoryName = (from sc in context.SubCategories where sc.Id == bi.Product.SubCategoryId select sc).FirstOrDefault().Name,
                         BrandName = bi.Product.Brand.Name,
                         UnitType = bi.Product.UnitType,
                         UnitQuantity = bi.Product.UnitQuantity,
                         UnitCount = bi.Product.UnitCount,
                         ImageUrl = bi.Product.ImageUrl
                     }).ToList(),
                     TotalBasketPrice = b.BasketItems.Sum(bi => bi.Product.UnitPrice * bi.ProductCount),
                     TotalBasketDiscount = b.BasketItems.Sum(bi => bi.Product.Discount * bi.ProductCount),
                     TotalBasketPaidPrice = b.BasketItems.Sum(bi => (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount)),
                     CampaignId = b.CampaignId,
                     CampaignType = b.CampaignType,
                     IsCampaignApplied = b.IsCampaignApplied,
                     CampaignDiscount = b.CampaignDiscount,
                     CouponId = b.CouponId,
                     CouponDiscount = b.CouponDiscount,
                     IsCouponApplied = b.IsCouponApplied,
                     UserId = b.UserId
                 })
                 .FirstOrDefault();

            return result;
        }

        public BasketSimpleDto GetSimpleByUserId(int userId)
        {
            using var context = new AvenSellContext();

            var result = context.Baskets
                 .Include(b => b.BasketItems)
                     .ThenInclude(bi => bi.Product)
                 .Where(b => b.UserId == userId)
                 .Select(b => new BasketSimpleDto
                 {
                     BasketId = b.Id,
                     BasketItems = b.BasketItems.Select(bi => new BasketProductSimpleDto
                     {
                         Id = bi.Id,
                         ProductId = bi.ProductId,
                         Name = bi.Product.Name,
                         UnitPrice = bi.Product.UnitPrice,
                         UnitDiscount = bi.Product.UnitPrice - bi.Product.Discount,
                         UnitPaidPrice = (decimal)((bi.Product.UnitPrice - bi.Product.Discount)),
                         ProductCount = bi.ProductCount ?? 0,
                         CategoryId = bi.Product.CategoryId,
                         CategoryName = (from c in context.Categories where c.Id == bi.Product.CategoryId select c).FirstOrDefault().Name,
                         SubCategoryId = bi.Product.SubCategoryId,
                         SubCategoryName = (from sc in context.SubCategories where sc.Id == bi.Product.SubCategoryId select sc).FirstOrDefault().Name,
                         BrandName = bi.Product.Brand.Name,
                         UnitType = bi.Product.UnitType,
                         UnitQuantity = bi.Product.UnitQuantity,
                         UnitCount = bi.Product.UnitCount,
                         ImageUrl = bi.Product.ImageUrl
                     }).ToList(),
                     TotalBasketPrice = b.BasketItems.Sum(bi => bi.Product.UnitPrice * bi.ProductCount),
                     TotalBasketDiscount = b.BasketItems.Sum(bi => bi.Product.Discount * bi.ProductCount),
                     TotalBasketPaidPrice = b.BasketItems.Sum(bi => (decimal)((bi.Product.UnitPrice - bi.Product.Discount) * bi.ProductCount))+0,
                     CampaignId = b.CampaignId,
                     CampaignType = b.CampaignType,
                     IsCampaignApplied = b.IsCampaignApplied,
                     CampaignDiscount = b.CampaignDiscount,
                     CouponId = b.CouponId,
                     CouponDiscount = b.CouponDiscount,
                     IsCouponApplied = b.IsCouponApplied,
                     UserId = b.UserId,
                     DeliveryFee=0
                     

                 })
                 .FirstOrDefault();

            return result;
        }

        public GraphPieDto GetTopProductInWaitinBasket(int count)
        {
            using var _context = new AvenSellContext();

            var productQuantities = _context.BasketItems
                .GroupBy(bi => bi.ProductId) // Ürün ID'sine göre grupla
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(bi => bi.ProductCount.GetValueOrDefault()) // Ürünlerin toplam miktarını hesapla
                })
                .OrderByDescending(g => g.TotalQuantity) // Toplam miktarına göre sırala
                .Take(count)
                .ToList();

            var labels = new List<string>();
            var data = new List<int?>();

            foreach (var item in productQuantities)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    labels.Add(product.Name);
                    data.Add(item.TotalQuantity);
                }
            }

            var chartData = new GraphPieDto
            {
                Labels = labels,
                Data = data
            };

            return chartData;
        }
    }
}
