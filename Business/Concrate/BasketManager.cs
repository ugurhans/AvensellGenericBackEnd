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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Entity.Concrate.paytr;
using Entity.Abtract;
using static Azure.Core.HttpHeader;
using System.Runtime.CompilerServices;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;
        IBasketItemService _basketItemService;
        private IBasketItemDal _basketItemDal;
        private readonly IOrderDal   _orderdal;
        private readonly ICampaignService _campaignService;
        private readonly ICouponService _CopuonService;
        private readonly IProductDal _productDal;
        private readonly ICouponDal _couponDal;
        private readonly IUserCouponDal _userCouponDal;
        private readonly ITimedCouponDal _TimedCouponDal;
        private readonly IProductCouponDal _productCouponDal;
        private readonly ICategoryCouponDal _categoryCouponDal;
        private readonly IShopDal _shopDal;

        public BasketManager(IOrderDal orderDal, IBasketDal basketDal, IBasketItemService basketItemService, IBasketItemDal basketItemDal, ICampaignService campaignService, IProductDal productDal, ICouponDal couponDal, IUserCouponDal userCouponDal, ICouponService copuonService, ITimedCouponDal timedCouponDal, IProductCouponDal productCouponDal, ICategoryCouponDal categoryCouponDal, IShopDal shopDal)
        {
            _basketDal = basketDal;
            _basketItemService = basketItemService;
            _basketItemDal = basketItemDal;
            _campaignService = campaignService;
            _productDal = productDal;
            _couponDal = couponDal;
            _userCouponDal = userCouponDal;
            _CopuonService = copuonService;
            _TimedCouponDal = timedCouponDal;
            _productCouponDal = productCouponDal;
            _categoryCouponDal = categoryCouponDal;
            _orderdal = orderDal;
            _shopDal = shopDal;
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
                    case CampaignTypes.SpecialDiscountCampaign:
                        result = ApplySpecialDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.GiftProductCampaign:
                        result = ApplyGiftProductCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CombinedDiscountCampaign:
                        result = ApplyCombinedDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CategoryPercentageDiscountCampaign:
                        result = ApplyCategoryPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductPercentageDiscountCampaign:
                        result = ApplyProductPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
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
            var shop = _shopDal.Get(x => x.DeliveryFee != null);
            decimal deliveryFee = shop?.DeliveryFee ?? 0; // Null ise 0 olarak kabul et
            basket.DeliveryFee = Convert.ToInt32(shop?.DeliveryFee ?? deliveryFee);
            basket.TotalBasketPaidPrice = basket.TotalBasketPaidPrice + basket.DeliveryFee;
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
                    case CampaignTypes.SpecialDiscountCampaign:
                        result = ApplySpecialDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.GiftProductCampaign:
                        result = ApplyGiftProductCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CombinedDiscountCampaign:
                        result = ApplyCombinedDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CategoryPercentageDiscountCampaign:
                        result = ApplyCategoryPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductPercentageDiscountCampaign:
                        result = ApplyProductPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
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
                    basket.DeliveryFee = 0;
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
                    couponTypes = result.Data.couponTypes,
                    TotalBasketDiscount = result.Data.TotalBasketDiscount,
                    TotalBasketPaidPrice = result.Data.TotalBasketPaidPrice,
                    TotalBasketPrice = result.Data.TotalBasketPrice + Convert.ToInt32(shop?.DeliveryFee ?? deliveryFee),
                    DeliveryFee = Convert.ToInt32(shop?.DeliveryFee ?? deliveryFee)
            });
            }
            // _orderService.GetByOrderId(userId);
          
            return new SuccessDataResult<BasketSimpleDto>(basket);
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

        public IDataResult<BasketDetailDto> GetDetailByBasketId(int basketId)
        {
            return new SuccessDataResult<BasketDetailDto>(_basketDal.GetBasketWithBasketId(basketId));
        }

        public IDataResult<GraphPieDto> GetTopProductInWaitinBasket(int count)
        {
            return new SuccessDataResult<GraphPieDto>(_basketDal.GetTopProductInWaitinBasket(count));
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


        private bool IsCategoryCampanyValid(CampaignCategoryPercentageDiscount campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now)
            {

                return false;
            }
            if (BasketDetailDto.BasketItems != null && BasketDetailDto.BasketItems.Any())
            {
                foreach (var basketItem in BasketDetailDto.BasketItems)
                {
                    // Eğer herhangi bir elemanın CategoryId özelliği kampanyanın CategoryId'sine eşitse true döndür
                    if (basketItem.CategoryId == campaign.CategoryId)
                    {
                        return true;
                    }
                }
            }

            var minamount = campaign.MinPurchaseAmount;
            var basketamount = BasketDetailDto.TotalBasketPrice;

            if (basketamount < minamount)
            {
                //decimal indirimTutari = (decimal)(basketamount * (campaign.PercentageDiscountRate / 100));

                //// Sepet tutarından indirim miktarını çıkar
                //basketamount -= indirimTutari;
                return false;
            }

            return true;
        }

        private bool IsCombinanedCampanyValid(CampaignCombinedDiscount campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now || campaign.MaxDiscountAmount < BasketDetailDto.TotalBasketPrice)
            {

                return false;
            }

            var combinedProductIds = campaign.CombinedProduct.Split(',').Select(int.Parse).ToList();

            if (BasketDetailDto.BasketItems != null && BasketDetailDto.BasketItems.Any())
            {
                foreach (var basketItem in BasketDetailDto.BasketItems)
                {
                    // Eğer sepet ürünü kampanyaya dahil edilen ürünlerden biri ise
                    if (combinedProductIds.Contains(basketItem.ProductId))
                    {
                        return true;
                    }
                }
            }

            // Yukarıdaki döngüden hiçbir eşleşme bulunamadıysa kampanya geçerli değil.
            return false;


        }
        private bool IsProductCampanyValid(CampaignProductPercentageDiscount campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now || campaign.MinPurchaseAmount < BasketDetailDto.TotalBasketPrice)
            {

                return false;
            }

            var combinedProductIds = campaign.CombinedProduct.Split(',').Select(int.Parse).ToList();

            if (BasketDetailDto.BasketItems != null && BasketDetailDto.BasketItems.Any())
            {
                foreach (var basketItem in BasketDetailDto.BasketItems)
                {
                    // Eğer sepet ürünü kampanyaya dahil edilen ürünlerden biri değilse
                    if (!combinedProductIds.Contains(basketItem.ProductId))
                    {
                        return false;
                    }
                }

                // Sepette belirtilen ürünlerden başka bir ürün bulunmuyorsa
                return true;
            }

            // Sepette hiç ürün yoksa
            return false;




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
        private bool IsSpecialDiscountCampaignValid(CampaignSpecialDiscount campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now || campaign.MinPurchaseAmount > BasketDetailDto.TotalBasketPrice)
            {
                return false;
            }


            bool giftProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.ProductID);


            int giftProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.ProductID).Sum(x => x.ProductCount) ?? 0;

            if (giftProductQuantity > 3)
            {
                return false;
            }

            return true;
        }

        private bool IsGıftCampaignValid(CampaignGiftProduct campaign, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (campaign.IsActive == false || campaign.IsActive == null || campaign.StartDate > DateTime.Now || campaign.EndDate < DateTime.Now || campaign.MinPurchaseAmount > BasketDetailDto.TotalBasketPrice)
            {
                return false;
            }


            bool giftProductExists = BasketDetailDto.BasketItems.Any(x => x.ProductId == campaign.GiftProductId);


            int giftProductQuantity = BasketDetailDto.BasketItems.Where(x => x.ProductId == campaign.GiftProductId).Sum(x => x.ProductCount) ?? 0;

            if (giftProductQuantity > 3)
            {
                return false;
            }

            return true;
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




       



        public IDataResult<BasketDetailDto> ApplyCategoryPercentageDiscountCampaign(int campaignId, int basketId)
        {

            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignCategoryPercentageDiscount>(CampaignTypes.CategoryPercentageDiscountCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isCategoryValid = IsCategoryCampanyValid(campaign, BasketDetailDto);
                    if (isCategoryValid)
                    {
                        var minamount = campaign.MinPurchaseAmount;
                        var basketamount = BasketDetailDto.TotalBasketPrice;
                        decimal indirimTutari = 0;

                        if (basketamount > minamount)
                        {
                            indirimTutari = ((decimal)((basketamount * campaign.PercentageDiscountRate) / 100));



                            // Sepet tutarından indirim miktarını çıkar
                            basketamount -= indirimTutari;

                        }

                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign;
                        basket.CampaignId = campaignId;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = basketamount,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }

        public IDataResult<BasketDetailDto> ApplyCombinedDiscountCampaign(int campaignId, int basketId)
        {
            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignCombinedDiscount>(CampaignTypes.CombinedDiscountCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isCategoryValid = IsCombinanedCampanyValid(campaign, BasketDetailDto);
                    if (isCategoryValid)
                    {
                        var basketamount = BasketDetailDto.TotalBasketPrice;
                        decimal indirimTutari = 0;


                        indirimTutari = ((decimal)((basketamount * campaign.PercentageDiscountRate) / 100));



                        // Sepet tutarından indirim miktarını çıkar
                        basketamount = basketamount - indirimTutari;



                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign;
                        basket.CampaignId = campaignId;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = BasketDetailDto.TotalBasketPrice,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }

        public IDataResult<BasketDetailDto> ApplyProductPercentageDiscountCampaign(int campaignId, int basketId)
        {

            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignProductPercentageDiscount>(CampaignTypes.ProductPercentageDiscountCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isCategoryValid = IsProductCampanyValid(campaign, BasketDetailDto);
                    if (isCategoryValid)
                    {
                        var minamount = campaign.MinPurchaseAmount;
                        var basketamount = BasketDetailDto.TotalBasketPrice;
                        decimal indirimTutari = 0;

                        if (minamount > basketamount)
                        {
                            indirimTutari = ((decimal)((basketamount * campaign.PercentageDiscountRate) / 100));



                            // Sepet tutarından indirim miktarını çıkar
                            basketamount = basketamount - indirimTutari;

                        }

                        var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                        basket.IsCampaignApplied = true;
                        basket.CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign;
                        basket.CampaignId = campaignId;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = basketamount,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = BasketDetailDto.BasketItems,
                            CampaignId = campaign.Id,
                            CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("Kampanya ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("Kampanya Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }

        public IDataResult<BasketDetailDto> ApplySpecialDiscountCampaign(int campaignId, int basketId)
        {

            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignSpecialDiscount>(CampaignTypes.SpecialDiscountCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isSpecialDiscountValid = IsSpecialDiscountCampaignValid(campaign, BasketDetailDto);
                    if (isSpecialDiscountValid)
                    {
                        var giftedProduct = _productDal.Get(x => x.Id == campaign.ProductID);
                        decimal deneme = 0;
                        if (giftedProduct != null)
                        {
                            foreach (var item in BasketDetailDto.BasketItems)
                            {
                                if (item.ProductId == giftedProduct.Id)
                                {
                                    // Hediye edilen ürünün fiyatını düşür
                                    item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                                     deneme = item.UnitPaidPrice;
                                    // Sepetin toplam ödenen fiyatını düşür
                                    BasketDetailDto.TotalBasketPaidPrice = BasketDetailDto.TotalBasketPaidPrice - item.UnitPaidPrice;
                                    var basket = _basketDal.Get(x => x.Id == BasketDetailDto.BasketId);
                                    basket.IsCampaignApplied = true;
                                    basket.CampaignType = CampaignTypes.SecondDiscountCampaign;
                                    basket.CampaignId = campaignId;

                                    _basketDal.Update(basket);
                                }
                            }
                        }




                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = BasketDetailDto.BasketId,
                            TotalBasketDiscount = deneme,
                            TotalBasketPrice = BasketDetailDto.TotalBasketPrice,
                            TotalBasketPaidPrice = BasketDetailDto.TotalBasketPaidPrice,
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


        public IDataResult<BasketDetailDto> ApplyGiftProductCampaign(int campaignId, int basketId) //üsttede var onuda kullanabiliriz.
        {

            var BasketDetailDto = _basketDal.GetBasketWithBasketId(basketId);
            var campaign = _campaignService.Get<CampaignGiftProduct>(CampaignTypes.GiftProductCampaign, campaignId).Data;
            if (BasketDetailDto != null)
            {
                if (campaign != null)
                {
                    var isGiftValid = IsGıftCampaignValid(campaign, BasketDetailDto);
                    if (isGiftValid)
                    {
                        var giftProduct = _productDal.Get(x => x.Id == campaign.GiftProductId);
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


                        basket.TotalBasketPaidPrice = basket.TotalBasketPaidPrice - coupon.DiscountAmount;
                        basket.TotalBasketDiscount = basket.TotalBasketDiscount + coupon.DiscountAmount;

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

        public IDataResult<BasketDetailDto> ApplyProductCoupon(string couponcode, int basketId)
        {
            var basketdetaildto = _basketDal.GetBasketWithBasketId(basketId);
            var coupon = _productCouponDal.Get(x => x.IsActive == true && x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.Code == couponcode);
            if (coupon != null)
            {
                var validProducts = IsProductcouponValid(coupon, basketdetaildto);
                var userCoupon = _userCouponDal.Get(x => x.BasketId == basketdetaildto.BasketId && x.CouponId == coupon.Id);
                if (userCoupon == null)
                {

                   
                    
                    if (validProducts)
                    {
                        var procuctcoupon = _productCouponDal.Get(x => x.Id == coupon.Id);
                        foreach (var item in basketdetaildto.BasketItems)
                        {
                            if (item.Id == procuctcoupon.Id)
                            {
                                item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                            }
                        }

                        var minamount = coupon.MinBasketCost;
                        var basketamount = basketdetaildto.TotalBasketPrice;
                        decimal indirimTutari = coupon.Discount;

                        if (basketamount > minamount)
                        {
                            basketamount = ((decimal)(basketdetaildto.TotalBasketPrice - coupon.Discount));



                        

                        }

                        var basket = _basketDal.Get(x => x.Id == basketdetaildto.BasketId);
                        basket.IsCouponApplied = true;
                        basket.coupontype = CouponTypes.CouponProduct;
                        basket.CouponCode = couponcode;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = basketdetaildto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = basketamount,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = basketdetaildto.BasketItems,
                            CouponId = coupon.Id,
                            couponTypes = CouponTypes.CouponProduct,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("kupon ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("kupon Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
         
        }

        private bool IsProductcouponValid(CouponProduct coupon, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (coupon.IsActive == false || coupon.IsActive == null || coupon.StartDate > DateTime.Now || coupon.EndDate < DateTime.Now || coupon.MinBasketCost > BasketDetailDto.TotalBasketPrice)
            {

                return false;
            }

            var combinedProduct = coupon.CombinedProduct.Split(',').Select(int.Parse).ToList();

            if (BasketDetailDto.BasketItems != null && BasketDetailDto.BasketItems.Any())
            {
                foreach (var basketItem in BasketDetailDto.BasketItems)
                {
                    // Eğer sepet ürünü kampanyaya dahil edilen ürünlerden biri değilse
                    if (!combinedProduct.Contains(basketItem.ProductId))
                    {
                        return false;
                    }
                }

                // Sepette belirtilen ürünlerden başka bir ürün bulunmuyorsa
                return true;
            }

            // Sepette hiç ürün yoksa
            return false;

        }

        private bool IsCategorycouponValid(CouponCategory coupon, BasketDetailDto basketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (coupon.IsActive == false || coupon.IsActive == null || coupon.StartDate > DateTime.Now || coupon.EndDate < DateTime.Now || coupon.MinBasketCost > basketDetailDto.TotalBasketPrice)
            {
                return false;
            }

            var targetCategoryId = coupon.CategoryId;

            if (basketDetailDto.BasketItems != null && basketDetailDto.BasketItems.Any())
            {
                // Sepetteki ürünlerden en az biri, belirtilen kategoriye aitse true döndür
                if (basketDetailDto.BasketItems.Any(item => item.CategoryId == targetCategoryId))
                {
                    return true;
                }
            }

            // Sepette hiç ürün yoksa veya belirtilen kategoriye ait ürün bulunmuyorsa false döndür
            return false;
        }


        public IDataResult<BasketDetailDto> ApplyCategoryCoupon(string couponcode, int basketId)
        {
            var basketdetaildto = _basketDal.GetBasketWithBasketId(basketId);
            var coupon = _categoryCouponDal.Get(x => x.IsActive == true && x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.Code == couponcode);
            if (coupon != null)
            {
                var validProducts = IsCategorycouponValid(coupon, basketdetaildto);
                var userCoupon = _userCouponDal.Get(x => x.BasketId == basketdetaildto.BasketId && x.CouponId == coupon.Id);
                if (userCoupon == null)
                {



                    if (validProducts)
                    {
                        var procuctcoupon = _categoryCouponDal.Get(x => x.Id == coupon.Id);
                        foreach (var item in basketdetaildto.BasketItems)
                        {
                            if (item.Id == procuctcoupon.Id)
                            {
                                item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                            }
                        }

                        var minamount = coupon.MinBasketCost;
                        var basketamount = basketdetaildto.TotalBasketPrice;
                        decimal indirimTutari = coupon.Discount;

                        if (basketamount > minamount)
                        {
                            basketamount = ((decimal)(basketdetaildto.TotalBasketPrice - coupon.Discount));





                        }

                        var basket = _basketDal.Get(x => x.Id == basketdetaildto.BasketId);
                        basket.IsCouponApplied = true;
                        basket.coupontype = CouponTypes.CouponProduct;
                        basket.CouponCode = couponcode;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = basketdetaildto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = basketamount,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = basketdetaildto.BasketItems,
                            CouponId = coupon.Id,
                            couponTypes = CouponTypes.CouponProduct,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("kupon ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("kupon Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }

        private bool IsTimedcouponValid(CouponTimed coupon, BasketDetailDto BasketDetailDto)
        {
            // Kampanya tarihlerinin geçerliliğini kontrol et
            if (coupon.IsActive == false || coupon.IsActive == null || coupon.StartTime > DateTime.Now || coupon.EndTime < DateTime.Now || coupon.MinBasketCost > BasketDetailDto.TotalBasketPrice)
            {

                return false;
            }
            return true;

        }

        public IDataResult<BasketDetailDto> ApplyTimedCoupon(string couponcode, int basketId)
        {

            var basketdetaildto = _basketDal.GetBasketWithBasketId(basketId);
            var coupon = _TimedCouponDal.Get(x => x.IsActive == true && x.EndTime > DateTime.Now && x.StartTime < DateTime.Now && x.Code == couponcode);
            if (coupon != null)
            {
                var validProducts = IsTimedcouponValid(coupon, basketdetaildto);
                var userCoupon = _userCouponDal.Get(x => x.BasketId == basketdetaildto.BasketId && x.CouponId == coupon.Id);
                if (userCoupon == null)
                {



                    if (validProducts)
                    {
                        var procuctcoupon = _productCouponDal.Get(x => x.Id == coupon.Id);
                        foreach (var item in basketdetaildto.BasketItems)
                        {
                            if (item.Id == procuctcoupon.Id)
                            {
                                item.TotalPaidPrice = item.TotalPaidPrice - item.UnitPaidPrice;
                            }
                        }

                        var minamount = coupon.MinBasketCost;
                        var basketamount = basketdetaildto.TotalBasketPrice;
                        decimal indirimTutari = coupon.Discount;

                        if (basketamount > minamount)
                        {

                            basketamount = ((decimal)(basketdetaildto.TotalBasketPrice - coupon.Discount));

                        }

                        var basket = _basketDal.Get(x => x.Id == basketdetaildto.BasketId);
                        basket.IsCouponApplied = true;
                        basket.coupontype = CouponTypes.CouponProduct;
                        basket.CouponCode = couponcode;

                        _basketDal.Update(basket);


                        return new SuccessDataResult<BasketDetailDto>(new BasketDetailDto()
                        {
                            BasketId = basketdetaildto.BasketId,
                            TotalBasketDiscount = indirimTutari,
                            TotalBasketPrice = basketamount,
                            TotalBasketPaidPrice = basketamount,
                            BasketItems = basketdetaildto.BasketItems,
                            CouponId = coupon.Id,
                            couponTypes = CouponTypes.CouponProduct,
                            IsCampaignApplied = true
                        });
                    }
                    return new ErrorDataResult<BasketDetailDto>("kupon ile Sepet Eşleşmiyor.");
                }
                return new ErrorDataResult<BasketDetailDto>("kupon Bilgisi Bulunamadı.");
            }
            return new ErrorDataResult<BasketDetailDto>("Sepet Bilgisi Bulunamadı.");
        }
    }
}
