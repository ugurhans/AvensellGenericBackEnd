using System;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IChartsService
    {
        DataResult<GraphPieDto> GetCategoryForOrderChart();
        DataResult<RevenueAndProfitDto> GetCostForMarket();
        DataResult<GraphPieDto> GetProductForClick();
        DataResult<GraphPieDto> GetProductForLowSelling();
        DataResult<GraphPieDto> GetProductForWaitingInBasket();
    }
}

