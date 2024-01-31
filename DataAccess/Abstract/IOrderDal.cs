using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using Entity.Concrete;
using Entity.Enum;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        public List<OrderDto> GetOrderDetail(int userId, OrderStates state);
        public List<OrderDto> GetOrderDetails(int orderId);
        public List<OrderBasicDto> GetOrderDetailBasic(int orderId);
        public List<OrderDto> GetAllDto(Expression<Func<OrderDto, bool>> filter = null);
        public OrderDto GetDto(Expression<Func<OrderDto, bool>> filter = null);
        public List<OrderSimpleDto> GetAllDtoSimple(Expression<Func<OrderSimpleDto, bool>> filter = null);

        List<OrderDto> GetOrderDetailByState(OrderStates state, DateTime? dateStart, DateTime? dateEnd);
        int GetBadgeWithState(Expression<Func<Order, bool>> filter);
        GraphPieDto GetTopCategories(int number);
        GraphPieDto GetProductForLowSelling(int count);
        RevenueAndProfitDto GetCostForMarket();
    }
}

