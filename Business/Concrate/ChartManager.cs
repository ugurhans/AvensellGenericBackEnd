using System;
using Business.Abstract;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Concrate
{
    public class ChartManager : IChartsService
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ChartManager(IProductService productService, IBasketService basketService, IOrderService orderService)
        {
            _productService = productService;
            _basketService = basketService;
            _orderService = orderService;
        }

        public DataResult<GraphPieDto> GetCategoryForOrderChart()
        {
            return new SuccessDataResult<GraphPieDto>(_orderService.GetTopCategories(5)?.Data);
        }

        public DataResult<RevenueAndProfitDto> GetCostForMarket()
        {
            return new SuccessDataResult<RevenueAndProfitDto>(_orderService.GetCostForMarket()?.Data);
        }

        public DataResult<GraphPieDto> GetProductForClick()
        {
            return new SuccessDataResult<GraphPieDto>(_orderService.GetProductForLowSelling(5)?.Data);
        }

        public DataResult<GraphPieDto> GetProductForLowSelling()
        {
            return new SuccessDataResult<GraphPieDto>(_orderService.GetProductForLowSelling(5)?.Data);
        }

        public DataResult<GraphPieDto> GetProductForWaitingInBasket()
        {
            return new SuccessDataResult<GraphPieDto>(_basketService.GetTopProductInWaitinBasket(5)?.Data);
        }
    }
}

