﻿using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.Enum;
using Core.Entities.Concrete;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, AvenSellContext>, IOrderDal
    {
        public List<OrderDto> GetOrderDetail(int userId, OrderStates state)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from o in context.Orders
                             where o.UserId == userId && o.State == state
                             join a in context.Addresses
                                 on o.AddressId equals a.Id
                             select new OrderDto()
                             {
                                 OrderId = o.Id,
                                 UserId = o.UserId,
                                 AddressId = o.AddressId,
                                 Address = a,
                                 State = o.State,
                                 CallRing = o.CallRing,
                                 DeliveryType = ((CallRings)o.CallRing).ToString(),
                                 PaymentType = ((PaymentTypes)o.PaymentType).ToString(),
                                 CompletedDate = o.CompletedDate,
                                 ConfirmDate = o.ConfirmDate,
                                 OrdersItem = (from oi in context.OrderItems
                                               where oi.OrderId == o.Id
                                               select new OrderItemDto
                                               {
                                                   Id = oi.Id,
                                                   OrderId = oi.OrderId,
                                                   ProductId = oi.ProductId,
                                                   BasketItemId = oi.BasketItemId,
                                                   TotalPrice = oi.TotalPrice,
                                                   TotalDiscount = oi.TotalDiscount,
                                                   TotalPaidPrice = oi.TotalPaidPrice,
                                                   ProductCount = oi.ProductCount,
                                                   ProductName = oi.ProductName,
                                                   ProductPrice = oi.ProductPrice,
                                                   ProductDiscountPrice = oi.ProductDiscountPrice,
                                                   ProductPaidPrice = oi.ProductPaidPrice,
                                                   Image = null
                                               }
                                     ).ToList(),
                                 TotalOrderPrice = o.TotalOrderPrice,
                                 TotalOrderDiscount = o.TotalOrderDiscount,
                                 OrderDate = o.OrderDate,
                                 TotalOrderPaidPrice = o.TotalOrderPaidPrice,
                                 DeliveryFee = o.DeliveryFee
                             };

                return result.ToList();
            }
        }

        public List<OrderDto> GetOrderDetailByState(OrderStates state, DateTime? dateStart, DateTime? dateEnd)
        {

            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from o in context.Orders
                             where o.State == state
                             join a in context.Addresses
                                 on o.AddressId equals a.Id
                             select new OrderDto()
                             {
                                 OrderId = o.Id,
                                 UserId = o.UserId,
                                 AddressId = o.AddressId,
                                 Address = a,
                                 State = o.State,
                                 CallRing = o.CallRing,
                                 DeliveryType = ((CallRings)o.CallRing).ToString(),
                                 PaymentType = ((PaymentTypes)o.PaymentType).ToString(),
                                 CompletedDate = o.CompletedDate,
                                 ConfirmDate = o.ConfirmDate,
                                 OrdersItem = (from oi in context.OrderItems
                                               where oi.OrderId == o.Id
                                               select new OrderItemDto
                                               {
                                                   Id = oi.Id,
                                                   OrderId = oi.OrderId,
                                                   ProductId = oi.ProductId,
                                                   BasketItemId = oi.BasketItemId,
                                                   TotalPrice = oi.TotalPrice,
                                                   TotalDiscount = oi.TotalDiscount,
                                                   TotalPaidPrice = oi.TotalPaidPrice,
                                                   ProductCount = oi.ProductCount,
                                                   ProductName = oi.ProductName,
                                                   ProductPrice = oi.ProductPrice,
                                                   ProductDiscountPrice = oi.ProductDiscountPrice,
                                                   ProductPaidPrice = oi.ProductPaidPrice,
                                                   Image = null
                                               }
                                     ).ToList(),
                                 TotalOrderPrice = o.TotalOrderPrice,
                                 TotalOrderDiscount = o.TotalOrderDiscount,
                                 OrderDate = o.OrderDate,
                                 TotalOrderPaidPrice = o.TotalOrderPaidPrice,
                                 DeliveryFee = o.DeliveryFee
                             };
                if (dateEnd != null && dateStart != null)
                {
                    return result.Where(o => o.OrderDate < dateEnd && o.OrderDate > dateStart).ToList();

                }
                return result.ToList();
            }
        }

        public int GetBadgeWithState(Expression<Func<Order, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                return filter == null
                ? context.Set<Order>().ToList().Count
                : context.Set<Order>().Where(filter).ToList().Count;

            }
        }

        public List<OrderDto> GetAllDto(Expression<Func<OrderDto, bool>> filter)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from o in context.Orders
                             join a in context.Addresses
                                 on o.AddressId equals a.Id
                             select new OrderDto()
                             {
                                 OrderId = o.Id,
                                 UserId = o.UserId,
                                 AddressId = o.AddressId,
                                 Address = a,
                                 State = o.State,
                                 CallRing = o.CallRing,
                                 DeliveryType = ((CallRings)o.CallRing).ToString(),
                                 PaymentType = ((PaymentTypes)o.PaymentType).ToString(),
                                 CompletedDate = o.CompletedDate,
                                 ConfirmDate = o.ConfirmDate,
                                 OrdersItem = (from oi in context.OrderItems
                                               where oi.OrderId == o.Id
                                               select new OrderItemDto
                                               {
                                                   Id = oi.Id,
                                                   OrderId = oi.OrderId,
                                                   ProductId = oi.ProductId,
                                                   BasketItemId = oi.BasketItemId,
                                                   TotalPrice = oi.TotalPrice,
                                                   TotalDiscount = oi.TotalDiscount,
                                                   TotalPaidPrice = oi.TotalPaidPrice,
                                                   ProductCount = oi.ProductCount,
                                                   ProductName = oi.ProductName,
                                                   ProductPrice = oi.ProductPrice,
                                                   ProductDiscountPrice = oi.ProductDiscountPrice,
                                                   ProductPaidPrice = oi.ProductPaidPrice,
                                                   Image = null
                                               }
                                     ).ToList(),
                                 TotalOrderPrice = o.TotalOrderPrice,
                                 TotalOrderDiscount = o.TotalOrderDiscount,
                                 OrderDate = o.OrderDate,
                                 TotalOrderPaidPrice = o.TotalOrderPaidPrice,
                                 DeliveryFee = o.DeliveryFee
                             };

                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        public List<OrderSimpleDto> GetAllDtoSimple(Expression<Func<OrderSimpleDto, bool>> filter = null)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from o in context.Orders
                             join u in context.Users on o.UserId equals u.Id
                             select new OrderSimpleDto()
                             {
                                 Id = o.Id,
                                 State = o.State,
                                 OrdersItem = (from oi in context.OrderItems
                                               where oi.OrderId == o.Id
                                               select new OrderItemDto
                                               {
                                                   Id = oi.Id,
                                                   OrderId = oi.OrderId,
                                                   ProductId = oi.ProductId,
                                                   BasketItemId = oi.BasketItemId,
                                                   TotalPrice = oi.TotalPrice,
                                                   TotalDiscount = oi.TotalDiscount,
                                                   TotalPaidPrice = oi.TotalPaidPrice,
                                                   ProductCount = oi.ProductCount,
                                                   ProductName = oi.ProductName,
                                                   ProductPrice = oi.ProductPrice,
                                                   ProductDiscountPrice = oi.ProductDiscountPrice,
                                                   ProductPaidPrice = oi.ProductPaidPrice,
                                                   Image = null
                                               }
                                     ).ToList(),
                                 OrderDate = o.OrderDate,
                                 Price = o.TotalOrderPaidPrice,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName
                             };

                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        public OrderDto GetDto(Expression<Func<OrderDto, bool>> filter = null)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from o in context.Orders
                             join a in context.Addresses
                                 on o.AddressId equals a.Id
                             select new OrderDto()
                             {
                                 OrderId = o.Id,
                                 UserId = o.UserId,
                                 AddressId = o.AddressId,
                                 Address = a,
                                 State = o.State,
                                 CallRing = o.CallRing,
                                 DeliveryType = ((CallRings)o.CallRing).ToString(),
                                 PaymentType = ((PaymentTypes)o.PaymentType).ToString(),
                                 CompletedDate = o.CompletedDate,
                                 ConfirmDate = o.ConfirmDate,
                                 OrdersItem = (from oi in context.OrderItems
                                               where oi.OrderId == o.Id
                                               select new OrderItemDto
                                               {
                                                   Id = oi.Id,
                                                   OrderId = oi.OrderId,
                                                   ProductId = oi.ProductId,
                                                   BasketItemId = oi.BasketItemId,
                                                   TotalPrice = oi.TotalPrice,
                                                   TotalDiscount = oi.TotalDiscount,
                                                   TotalPaidPrice = oi.TotalPaidPrice,
                                                   ProductCount = oi.ProductCount,
                                                   ProductName = oi.ProductName,
                                                   ProductPrice = oi.ProductPrice,
                                                   ProductDiscountPrice = oi.ProductDiscountPrice,
                                                   ProductPaidPrice = oi.ProductPaidPrice,
                                                   Image = null
                                               }
                                     ).ToList(),
                                 TotalOrderPrice = o.TotalOrderPrice,
                                 TotalOrderDiscount = o.TotalOrderDiscount,
                                 OrderDate = o.OrderDate,
                                 TotalOrderPaidPrice = o.TotalOrderPaidPrice,
                                 DeliveryFee = o.DeliveryFee
                             };

                return filter == null
                    ? result.SingleOrDefault()
                    : result.Where(filter).SingleOrDefault();
            }
        }

        public GraphPieDto GetTopCategories(int count)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var topCategories = context.OrderItems
                  .Join(context.Products, oi => oi.ProductId, p => p.Id, (oi, p) => new { oi, p })
                  .Join(context.Categories, oip => oip.p.CategoryId, c => c.Id, (oip, c) => new { oip, c })
                  .GroupBy(oc => oc.c.Name)
                  .Select(g => new
                  {
                      CategoryName = g.Key,
                      OrderCount = g.Sum(oc => oc.oip.oi.ProductCount)
                  })
                  .OrderByDescending(c => c.OrderCount)
                  .Take(count)
                  .ToList();

                var graphData = new GraphPieDto
                {
                    Labels = topCategories.Select(c => c.CategoryName).ToList(),
                    Data = topCategories.Select(c => c.OrderCount).ToList()
                };
                return graphData;
            }
        }

        public GraphPieDto GetProductForLowSelling(int count)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var productQuantities = context.OrderItems
                    .GroupBy(oi => oi.ProductId) // Ürün ID'sine göre grupla
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        TotalQuantity = g.Sum(oi => oi.ProductCount) // Ürünlerin toplam satış miktarını hesapla
                    })
                    .OrderBy(g => g.TotalQuantity) // Toplam miktarına göre sırala
                    .Take(count)
                    .ToList();

                var labels = new List<string>();
                var data = new List<int?>();

                foreach (var item in productQuantities)
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == item.ProductId);
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

        public RevenueAndProfitDto GetCostForMarket()
        {
            DateTime startDate = DateTime.Now.AddMonths(-12).Date;
            DateTime endDate = DateTime.Now.Date;

            using (AvenSellContext context = new AvenSellContext())
            {
                var costData = context.Orders
                    .Where(order => order.OrderDate >= startDate && order.OrderDate <= endDate)
                    .GroupBy(order => order.OrderDate.Value.Month)
                    .Select(group => new
                    {
                        Month = group.Key,
                        Revenue = group.Sum(order => order.TotalOrderPaidPrice),
                        Profit = group.Sum(order => order.TotalOrderPrice - order.TotalOrderDiscount)
                    })
                    .OrderBy(item => item.Month)
                    .ToList();

                RevenueAndProfitDto result = new RevenueAndProfitDto
                {
                    Revenue = new List<int>(),
                    Profit = new List<int>()
                };

                for (int i = 1; i <= 12; i++)
                {
                    var monthData = costData.FirstOrDefault(item => item.Month == i);
                    int revenue = monthData != null ? Convert.ToInt32(monthData.Revenue) : 0;
                    int profit = monthData != null ? Convert.ToInt32(monthData.Profit) : 0;

                    result.Revenue.Add(revenue);
                    result.Profit.Add(profit);
                }

                return result;
            }
        }
    }
}


