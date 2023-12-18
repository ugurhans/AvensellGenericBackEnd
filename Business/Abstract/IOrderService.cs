using Core.Utilities.Results;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.Concrete;
using Entity.Concrate;
using Entity.Dto;
using Entity.Enum;
using Entity.Concrate.paytr;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<OrderDto>> GetAllDto(int userId, OrderStates state);
        IDataResult<int> GetAllCount();
        IResult RepeatOrder(int orderId);
        IResult Delete(int orderId);
        IDataResult<List<OrderDto>> GetAllByState(OrderStates state, DateTime? dateStart, DateTime? dateEnd);
        IResult CompleteOrder(int orderId);
        IResult CancelOrder(int orderId);
        IResult UpdateWithState(int orderId, OrderStates state);
        IDataResult<int> GetBadgeWithState(OrderStates stateId); 
        IDataResult<List<OrderDto>> GetOrdersWithCount(int orderCount);
        IDataResult<OrderPartDto> GetAllWithParts();
        IDataResult<OrderDto> GetById(int orderId);
        IDataResult<List<OrderSimpleDto>> GetLastOrdersSimple();
        IDataResult<GraphPieDto> GetTopCategories(int count);
        IDataResult<GraphPieDto> GetProductForLowSelling(int v);
        IDataResult<RevenueAndProfitDto> GetCostForMarket();
        IResult OrderComplate(PaytrWebHookDto paytrWebHookDto);//
        Task<IResult> AddPayTr(OrderCreateRequestDto order);
    }
}

