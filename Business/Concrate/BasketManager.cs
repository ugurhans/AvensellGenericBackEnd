using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Entity.Concrete;
using Entity.Concrate;
using Entity.Enum;
using Org.BouncyCastle.Bcpg;
using Entity.Dto;
using Core.Entities.Concrete;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;
        IBasketItemService _basketItemService;
        private IBasketItemDal _basketItemDal;
        private readonly ICampaignService _campaignService;
        private readonly IProductDal _productDal;
        private readonly ICouponDal _couponDal;
        private readonly IUserCouponDal _userCouponDal;

        public BasketManager(IBasketDal basketDal, IBasketItemService basketItemService, IBasketItemDal basketItemDal, ICampaignService campaignService, IProductDal productDal, ICouponDal couponDal, IUserCouponDal userCouponDal)
        {
            _basketDal = basketDal;
            _basketItemService = basketItemService;
            _basketItemDal = basketItemDal;
            _campaignService = campaignService;
            _productDal = productDal;
            _couponDal = couponDal;
            _userCouponDal = userCouponDal;
        }

        public IDataResult<BasketDetailDto> GetDetailByUserId(int userId)
        {
            var basket = _basketDal.GetBasketWithUserId(userId);
            if (basket.IsCampaignApplied == true && basket.CampaignId != null && basket.CampaignId > 0 && basket.CampaignType != null && basket.CampaignDiscount > 0)
            {
                IDataResult<BasketDetailDto> result = null;
                switch (basket.CampaignType)
                {
                    case CampaignTypes.GiftCampaign:
                        result = ApplyGiftCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductGroupCampaign:
                        result = ApplyProductGroupCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.SecondDiscountCampaign:
                        result = ApplySecondDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    default:
                        result = new ErrorDataResult<BasketDetailDto>("Kampanya Uygulanırken Sorun Yaşandı.");
                        break;
                }
                return new SuccessDataResult<BasketDetailDto>(result.Data);
            }
            return new SuccessDataResult<BasketDetailDto>(basket);
        }

        public IDataResult<Basket> GetBasket(int basketId)
        {
            return new SuccessDataResult<Basket>(_basketDal.Get(b => b.Id == basketId));
        }

        public IDataResult<BasketSimpleDto> GetSimpleByUserId(int userId)
        {
            var basket = _basketDal.GetSimpleByUserId(userId);
            if (basket.IsCampaignApplied == true && basket.CampaignId != null && basket.CampaignId > 0 && basket.CampaignType != null && basket.CampaignDiscount > 0)
            {
                IDataResult<BasketDetailDto> result = null;
                switch (basket.CampaignType)
                {
                    case CampaignTypes.GiftCampaign:
                        result = ApplyGiftCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductGroupCampaign:
                        result = ApplyProductGroupCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.SecondDiscountCampaign:
                        result = ApplySecondDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    default:
                        result = new ErrorDataResult<BasketDetailDto>("Kampanya Uygulanırken Sorun Yaşandı.");
                        break;
                }
                if (result.Data == null)
                {
                    var dbBasket = _basketDal.Get(x => x.Id == basket.BasketId);
                    dbBasket.IsCampaignApplied = null;
                    dbBasket.CampaignDiscount = null;
                    dbBasket.CampaignDiscount = null;
                    dbBasket.CampaignId = null;
                    basket.IsCampaignApplied = null;
                    basket.CampaignDiscount = null;
                    basket.CampaignDiscount = null;
                    basket.CampaignId = null;
                    _basketDal.Update(dbBasket);
                    return new SuccessDataResult<BasketSimpleDto>(basket);
                }
                return new SuccessDataResult<BasketSimpleDto>(new BasketSimpleDto()
                {
                    BasketId = result.Data.BasketId,
                    CampaignId = result.Data.CampaignId,
                    CampaignType = result.Data.CampaignType,
                    BasketItems = basket.BasketItems,
                    CampaignDiscount = result.Data.CampaignDiscount,
                    IsCampaignApplied = result.Data.IsCampaignApplied,
                    TotalBasketDiscount = result.Data.TotalBasketDiscount,
                    TotalBasketPaidPrice = result.Data.TotalBasketPaidPrice,
                    TotalBasketPrice = result.Data.TotalBasketPrice
                });
            }
            return new SuccessDataResult<BasketSimpleDto>(basket);
        }


        public IDataResult<BasketDetailDto> ApplyCoupon(string couponCode, int basketId)
        {
            var basket = _basketDal.GetBasketWithBasketId(basketId);
            var coupon = _couponDal.Get(x => x.IsActive == true && x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.CouponCode == couponCode);
            if (coupon != null)
            {
                var userCoupon = _userCouponDal.Get(x => x.BasketId == basket.BasketId && x.CouponId == coupon.Id);
                if (userCoupon == null)
                {
                    if (basket.TotalBasketPaidPrice >= coupon.MinBasketCost)
                    {
                        var dbBAsket = _basketDal.Get(x => x.Id == basket.BasketId);

                        dbBAsket.CampaignDiscount = null;
                        dbBAsket.CampaignId = null;
                        dbBAsket.CampaignType = null;
                        dbBAsket.IsCampaignApplied = null;
                        _basketDal.Update(dbBAsket);

                        basket = _basketDal.GetBasketWithBasketId(basket.BasketId);


                        basket.TotalBasketPaidPrice = basket.TotalBasketPaidPrice - coupon.Discount;
                        basket.TotalBasketDiscount = basket.TotalBasketDiscount + coupon.Discount;

                        _userCouponDal.Add(new UserCoupon()
                        {
                            BasketId = basket.BasketId,
                            CouponId = coupon.Id,
                            UsageDate = DateTime.Now,
                            UserId = basket.UserId ?? 1,

                        });
                        return new SuccessDataResult<BasketDetailDto>
                           (basket, "Kupon Uygulandı");

                    }
                    else
                    {
                        return new ErrorDataResult<BasketDetailDto>
                            (basket, "Kuponu Uygulamak için sepetinize " + (coupon.MinBasketCost - basket.TotalBasketPaidPrice) + " ₺ değerinde ürün ekleyin.");
                    }
                }
                else
                {
                    return new ErrorDataResult<BasketDetailDto>
                            (basket, "Kupon Zaten Kullanıldı.");

                }

            }
            else
            {
                return new ErrorDataResult<BasketDetailDto>(basket, "Kupon Bulunamadı.");
            }

        }
        public IResult Add(Basket basket)
        {
            var isExist = checkExist(basket.UserId);
            if (!isExist)
            {
                _basketDal.Add(basket);
                return new SuccessResult(Messages.Added);
            }

            return new ErrorResult("Sepet zaten oluşturulmuş.");
        }

        public IResult DeleteBasket(int basketId)
        {
            var items = _basketItemService.GetAll(basketId);

            foreach (var item in items.Data)
            {
                _basketItemDal.Delete(item.Id);
            }
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<int> GetBadgeCount(int userId)
        {
            var basketId = _basketDal.Get(b => b.UserId == userId).Id;

            var basketItem = _basketItemService.GetAll(basketId);
            if (basketItem.Success && basketItem.Data != null)
            {
                var count = basketItem.Data.Count;
                return new SuccessDataResult<int>(count);
            }
            return new SuccessDataResult<int>(0);

        }

        private bool checkExist(int userId)
        {
            var result = _basketDal.Get(b => b.UserId == userId);
            if (result != null)
            {
                return true;
            }

            return false;
        }

        public IDataResult<BasketDetailDto> ApplyGiftCampaign(int campaignId, int basketId)
        {
            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignGift>(CampaignTypes.GiftCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isGiftValid = IsGiftCampaignValid(campaign, BasketDetailDto);
                    if (isGiftValid)
                    {
                        var giftProduct = _productDal.Get(x => x.Id == campaign.ProductGift);
                        foreach (var item in BasketDetailDto.BasketItems)
                        {
                            if (item.ProductId == giftProduct.Id)
                            {
                                item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                                break;
                            }
                        }

                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.GiftCampaign;
                        basket.CampaignId = campaignId;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = BasketDetailDto.TotalBasketDiscount + (giftProduct.UnitPrice - giftProduct.Discount),
                            TotalBasketPrice = BasketDetailDto.TotalBasketPrice,
                            TotalBasketPaidPrice = BasketDetailDto.TotalBasketPaidPrice - (giftProduct.UnitPrice - giftProduct.Discount),
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.GiftCampaign,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");

        }

        public IDataResult<BasketDetailDto> ApplyProductGroupCampaign(int campaignId, int basketId)
        {
            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignProductGroup>(CampaignTypes.ProductGroupCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    //if (BasketDetailDto.IsCampaignApplied == true && BasketDetailDto.CampaignId == campaign.Id)
                    //{
                    //    return new ErrorDataResult<BasketDetailDto>("Kampanya Zaten Uygulandı");
                    //}
                    var isGroupValid = IsGroupCampaignValid(campaign, BasketDetailDto);
                    if (isGroupValid)
                    {
                        var productFirst = _productDal.Get(x => x.Id == campaign.ProductFirstId);
                        var productSecond = _productDal.Get(x => x.Id == campaign.ProductSecondId);

                        foreach (var item in BasketDetailDto.BasketItems)
                        {
                            if (productFirst.Id == item.ProductId)
                            {
                                item.TotalPaidPrice = (decimal)(item.TotalPaidPrice - campaign.ProductFirstDiscount);
                                item.TotalPrice = (decimal)(item.TotalPrice - campaign.ProductFirstDiscount);
                                item.UnitPaidPrice = (decimal)(item.UnitPaidPrice - campaign.ProductFirstDiscount);
                                item.UnitPrice = (decimal)(item.UnitPrice - campaign.ProductFirstDiscount);
                                break;
                            }
                        }


                        foreach (var item in BasketDetailDto.BasketItems)
                        {
                            if (productSecond.Id == item.ProductId)
                            {
                                item.TotalPaidPrice = (decimal)(item.TotalPaidPrice - campaign.ProductSecondDiscount);
                                item.TotalPrice = (decimal)(item.TotalPrice - campaign.ProductSecondDiscount);
                                item.UnitPaidPrice = (decimal)(item.UnitPaidPrice - campaign.ProductSecondDiscount);
                                item.UnitPrice = (decimal)(item.UnitPrice - campaign.ProductSecondDiscount);
                                break;
                            }
                        }


                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.ProductGroupCampaign;
                        basket.CampaignId = campaignId;
                        basket.CampaignDiscount = campaign.ProductSecondDiscount + campaign.ProductFirstDiscount;
                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = BasketDetailDto.TotalBasketDiscount + basket.CampaignDiscount,
                            TotalBasketPrice = BasketDetailDto.TotalBasketPrice,
                            TotalBasketPaidPrice = BasketDetailDto.TotalBasketPaidPrice - basket.CampaignDiscount,
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.GiftCampaign,
                            IsCampaignApplied = true,
                            CampaignDiscount = basket.CampaignDiscount,

                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");

        }


        private bool IsGiftCampaignValid(CampaignGift campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now)
            {
                return false;
            }

            // Sepetteki ürünleri kampanya kapsamındaki ürünlerle karşılaştır
            bool firstProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductFirstId);
            bool secondProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductSecondId);
            bool giftProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductGift);

            if (!firstProductExists || !secondProductExists || !giftProductExists)
            {
                return false;
            }

            // Sepetteki ürünlerin kampanya şartlarını karşıladığını kontrol et
            int firstProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductFirstId).Sum(x => x.ProductCount) ?? 0;
            int secondProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductSecondId).Sum(x => x.ProductCount) ?? 0;
            int giftProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductGift).Sum(x => x.ProductCount) ?? 0;

            if (firstProductQuantity < 1 || secondProductQuantity < 1 || giftProductQuantity < 1)
            {
                return false;
            }

            //bunu bi test etmek lazım
            if (giftProductQuantity != Math.Min(firstProductQuantity, secondProductQuantity))
            {
                return false;
            }

            return true;
        }

        private bool IsGroupCampaignValid(CampaignProductGroup campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now)
            {
                return false;
            }

            // Sepetteki ürünleri kampanya kapsamındaki ürünlerle karşılaştır
            bool firstProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductFirstId);
            bool secondProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductSecondId);

            if (!firstProductExists || !secondProductExists)
            {
                return false;
            }

            // Sepetteki ürünlerin kampanya şartlarını karşıladığını kontrol et
            int firstProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductFirstId).Sum(x => x.ProductCount) ?? 0;
            int secondProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductSecondId).Sum(x => x.ProductCount) ?? 0;

            if (firstProductQuantity < 1 || secondProductQuantity < 1)
            {
                return false;
            }
            return true;
        }

        public IResult ClearbasketCampaign(int basketId)
        {
            var basket = _basketDal.Get(x => x.Id == basketId);
            if (basket == null)
            {
                return new ErrorResult("Sepet Bilgisi Bulunamadı");
            }
            basket.IsCampaignApplied = null;
            basket.CampaignDiscount = null;
            basket.CampaignType = null;
            basket.CampaignId = null;
            _basketDal.Update(basket);
            return new SuccessResult("Kampanya Kaldırıldı");
        }

        public IDataResult<BasketDetailDto> ApplySecondDiscountCampaign(int campaignId, int basketId)
        {
            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignGift>(CampaignTypes.SecondDiscountCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isGiftValid = IsGiftCampaignValid(campaign, BasketDetailDto);
                    if (isGiftValid)
                    {
                        var giftProduct = _productDal.Get(x => x.Id == campaign.ProductGift);
                        foreach (var item in BasketDetailDto.BasketItems)
                        {
                            if (item.Id == giftProduct.Id)
                            {
                                item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                            }
                        }

                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.SecondDiscountCampaign;
                        basket.CampaignId = campaignId;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = BasketDetailDto.TotalBasketDiscount + (giftProduct.UnitPrice - giftProduct.Discount),
                            TotalBasketPrice = BasketDetailDto.TotalBasketPrice,
                            TotalBasketPaidPrice = BasketDetailDto.TotalBasketPaidPrice - (giftProduct.UnitPrice - giftProduct.Discount),
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.GiftCampaign,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }

        public IDataResult<BasketDetailDto> GetDetailByBasketId(int basketId)
        {
            return new SuccessDataResult<BasketDetailDto>(_basketDal.GetBasketWithBasketId(basketId));
        }

        public IDataResult<GraphPieDto> GetTopProductInWaitinBasket(int count)
        {
            return new SuccessDataResult<GraphPieDto>(_basketDal.GetTopProductInWaitinBasket(count));
        }
    }
}
