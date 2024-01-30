using Core.Utilities.Results;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.Concrete;
using Entity.Dto;
using Entity.Abtract;

namespace Business.Abstract
{
    public interface IBasketService
    {
        IDataResult<BasketDetailDto> GetDetailByUserId(int userId);
        IDataResult<BasketSimpleDto> GetSimpleByUserId(int userId);

        IDataResult<BasketDetailDto> GetDetailByBasketId(int basketId);


        IDataResult<BasketDetailDto> ApplyGiftCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplyProductGroupCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplySecondDiscountCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplyCombinedDiscountCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplyGiftProductCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplySpecialDiscountCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplyProductPercentageDiscountCampaign(int campaignId, int basketId);
        IDataResult<BasketDetailDto> ApplyCategoryPercentageDiscountCampaign(int campaignId, int basketId);



        IDataResult<BasketDetailDto> ApplyProductCoupon(string couponcode, int basketId);
        IDataResult<BasketDetailDto> ApplyCategoryCoupon(string couponcode, int basketId);
        IDataResult<BasketDetailDto> ApplyTimedCoupon(string couponcode, int basketId);



        IDataResult<Basket> GetBasket(int basketId);
        IResult Add(Basket basket);
        IResult DeleteBasket(int basketId);

        IDataResult<int> GetBadgeCount(int userId);
        IResult ClearbasketCampaign(int basketId);
        IDataResult<GraphPieDto> GetTopProductInWaitinBasket(int count);
    }
}
