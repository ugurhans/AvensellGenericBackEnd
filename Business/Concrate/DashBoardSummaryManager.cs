using System;
using Business.Abstract;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Concrate
{
    public class DashBoardSummaryManager : IDashboardSummaryService
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public DashBoardSummaryManager(IOrderService orderService, IUserService userService, IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        public IDataResult<DashBoardSummaryDto> GetSummary()
        {
            var orderCount = _orderService.GetAllCount()?.Data;
            var userCount = _userService.GetAllCount()?.Data;
            var productCount = _productService.GetAllCount()?.Data;
            var summaryDto = new DashBoardSummaryDto()
            {
                OrderCount = orderCount ?? 0,
                ProductCount = productCount ?? 0,
                UserCount = userCount ?? 0,
                AvenCoinCount = 0
            };
            return new SuccessDataResult<DashBoardSummaryDto>(summaryDto);
        }
    }
}

