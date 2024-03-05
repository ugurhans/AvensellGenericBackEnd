using System;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrate.EntityFramework;
using Entity.Abtract;
using Entity.Concrate;
using Entity.Concrate.paytr;
using Entity.Concrete;
using Entity.Dto;
using Entity.Enum;
using Entity.Request;

namespace Business.Concrate
{
    public class CampaignManager : ICampaignService
    {
        private readonly ICampaignGiftDal _campaignGiftDal;
        private readonly ICampaignProductGroupDal _campaignProductGroupDal;
        private readonly ICampaignSecondDiscountDal _campaignSecondDiscountDal;
        private readonly IBasketDal _basketDal;
        private readonly ICampaignCombinedDiscountDal _combinedDiscountCampaignDal;
        private readonly ICampaignProductPercentageDiscountDal _productPercentageDiscountCampaignDal;
        private readonly ICampaignGiftProductDal _giftProductCampaignDal;
        private readonly ICampaignSpecialDiscountDal _specialDiscountCampaignDal;
        private readonly ICampaignCategoryPercentageDiscountDal  _categoryPercentageDiscountCampaignDal;

        public CampaignManager(ICampaignGiftDal campaignGiftDal, ICampaignProductGroupDal campaignProductGrouptDal, ICampaignSecondDiscountDal campaignSecondDiscountDal, IBasketDal basketDal, ICampaignCategoryPercentageDiscountDal campaignCategoryPercentageDiscountDal, ICampaignSpecialDiscountDal campaignSpecialDiscountDal, ICampaignGiftProductDal  campaignGiftProductDal, ICampaignProductPercentageDiscountDal campaignProductPercentageDiscount, ICampaignCombinedDiscountDal campaignCombinedDiscountDal)
        {
            _campaignGiftDal = campaignGiftDal;
            _campaignProductGroupDal = campaignProductGrouptDal;
            _campaignSecondDiscountDal = campaignSecondDiscountDal;
            _basketDal = basketDal;
            _combinedDiscountCampaignDal = campaignCombinedDiscountDal;
            _productPercentageDiscountCampaignDal = campaignProductPercentageDiscount;
            _giftProductCampaignDal = campaignGiftProductDal;
            _specialDiscountCampaignDal = campaignSpecialDiscountDal;
            _categoryPercentageDiscountCampaignDal = campaignCategoryPercentageDiscountDal;

        }

        public IDataResult<CampaignDto> GetCampaignID(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Add(CampaignAddDto campaignAddDto)
        {
            //campaignType parametresine göre ilgili kampanya nesnesini oluşturuyoruz.
            var campaign = campaignAddDto.CampaignType;
            switch (campaignAddDto.CampaignType)
            {
                case CampaignTypes.GiftCampaign:
                    var campaignGift = new CampaignGift
                    {
                        ProductFirstId = (int)campaignAddDto.ProductFirstId,
                        ProductGift = campaignAddDto.ProductGift,
                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        ProductSecondId = (int)campaignAddDto.ProductSecondId,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                        

                    };
                    _campaignGiftDal.Add(campaignGift);
                    break;
                case CampaignTypes.ProductGroupCampaign:
                    var campaignProductGroup = new CampaignProductGroup
                    {
                        ProductFirstId = (int)campaignAddDto.ProductFirstId,
                        ProductFirstDiscount = campaignAddDto.ProductFirstDiscount,
                        ProductSecondId = (int)campaignAddDto.ProductSecondId,
                        ProductSecondDiscount = campaignAddDto.ProductSecondDiscount,
                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };
                    _campaignProductGroupDal.Add(campaignProductGroup);
                    break;
                case CampaignTypes.SecondDiscountCampaign:
                    var campaignSecondDiscount = new CampaignSecondDiscount
                    {
                        ProductFirstId = (int)campaignAddDto.ProductFirstId,
                        ProductSecondDiscount = campaignAddDto.ProductSecondDiscount,
                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };
                    _campaignSecondDiscountDal.Add(campaignSecondDiscount);
                    break;
                case CampaignTypes.ProductPercentageDiscountCampaign:
                    var ProductPercentageDiscount = new CampaignProductPercentageDiscount
                    {
                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        MinPurchaseAmount = campaignAddDto.MinPurchaseAmount,
                        CombinedProduct = campaignAddDto.CombinedProduct,
                        PercentageDiscountRate = campaignAddDto.PercentageDiscountRate,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };
                    _productPercentageDiscountCampaignDal.Add(ProductPercentageDiscount);
                    break;
                case CampaignTypes.CategoryPercentageDiscountCampaign:
                    var categoryPercentageDiscount = new CampaignCategoryPercentageDiscount
                    {
                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        MinPurchaseAmount = campaignAddDto.MinPurchaseAmount,
                        CategoryId = campaignAddDto.CategoryId,
                        PercentageDiscountRate = campaignAddDto.PercentageDiscountRate,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };
                    _categoryPercentageDiscountCampaignDal.Add(categoryPercentageDiscount);
                    break;
                case CampaignTypes.GiftProductCampaign:
                    var campaignGiftProduct = new CampaignGiftProduct
                    {

                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        GiftProductId = campaignAddDto.GiftProductId,
                        MinPurchaseAmount = campaignAddDto.MinPurchaseAmount,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,

                    };
                    _giftProductCampaignDal.Add(campaignGiftProduct);
                    break;
                case CampaignTypes.SpecialDiscountCampaign:
                    var campaignSpecialDiscount = new CampaignSpecialDiscount
                    {

                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        MinPurchaseAmount = campaignAddDto.MinPurchaseAmount,
                        ProductID = campaignAddDto.ProductId,
                        PromotionPeriodName = campaignAddDto.PromotionPeriodName,   // "Bugüne Özel" veya "Saatlik Teklifler" gibi
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };

                    _specialDiscountCampaignDal.Add(campaignSpecialDiscount);
                    break;
                case CampaignTypes.CombinedDiscountCampaign:
                    var campaignCombinedDiscount = new CampaignCombinedDiscount
                    {

                        IsActive = campaignAddDto.IsActive,
                        StartDate = campaignAddDto.StartDate,
                        EndDate = campaignAddDto.EndDate,
                        MaxDiscountAmount = campaignAddDto.MaxDiscountAmount,
                        CombinedProduct = campaignAddDto.CombinedProduct,
                        PercentageDiscountRate = campaignAddDto.PercentageDiscountRate,
                        CampaignDetail = campaignAddDto.CampaignDetail,
                        CampaignImageUrl = campaignAddDto.CampaignImageUrl,
                        CampaignName = campaignAddDto.CampaignName,
                    };
                    _combinedDiscountCampaignDal.Add(campaignCombinedDiscount);
                    break;

                default:
                    throw new ArgumentException("Invalid campaign type.");
            }
            return new SuccessResult(Messages.CampaignAdded);
        }

        public IResult Update(CampaignUpdateDto campaignUpdateDto)
        {
            var existingCampaign = GetById(campaignUpdateDto.Id, campaignUpdateDto.CampaignType);

            if (existingCampaign == null)
            {
                return new ErrorResult();
            }


            switch (campaignUpdateDto.CampaignType)
            {
                case CampaignTypes.GiftCampaign:
                    var campaignGift = (CampaignGift)existingCampaign;
                  
                    campaignGift.ProductFirstId = campaignUpdateDto.ProductIds[0];
                    campaignGift.ProductSecondId = campaignUpdateDto.ProductIds[1];
                    campaignGift.ProductGift = campaignUpdateDto.GiftProductId;
                    campaignGift.IsActive = campaignUpdateDto.IsActive;
                    campaignGift.StartDate = campaignUpdateDto.StartDate;
                    campaignGift.EndDate = campaignUpdateDto.EndDate;
                    campaignGift.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    campaignGift.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    campaignGift.CampaignName = campaignUpdateDto.CampaignName;
              
                    //
                    //campaignGift.Discount = campaignAddDto.Discount;
                   
                
     
                
                    _campaignGiftDal.Update(campaignGift);
                    break;
                case CampaignTypes.ProductGroupCampaign:
                    var campaignProductGroup = (CampaignProductGroup)existingCampaign;

                    campaignProductGroup.ProductFirstId = campaignUpdateDto.ProductIds[0];
                    campaignProductGroup.ProductSecondId = campaignUpdateDto.ProductIds[1];
                    campaignProductGroup.ProductSecondDiscount = campaignUpdateDto.ProductSecondDiscount;
                    campaignProductGroup.ProductFirstDiscount = campaignUpdateDto.ProductFirstDiscount;
                    campaignProductGroup.IsActive = campaignUpdateDto.IsActive;
                    campaignProductGroup.StartDate = campaignUpdateDto.StartDate;
                    campaignProductGroup.EndDate = campaignUpdateDto.EndDate;
                    campaignProductGroup.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    campaignProductGroup.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    campaignProductGroup.CampaignName = campaignUpdateDto.CampaignName;

        
                    //    ProductFirstDiscount = campaignAddDto.ProductFirstDiscount,
                    //    ProductSecondDiscount = campaignAddDto.ProductSecondDiscount,
             
                    _campaignProductGroupDal.Update(campaignProductGroup);
                    break;
                case CampaignTypes.SecondDiscountCampaign:
                    var campaignSecondDiscount = (CampaignSecondDiscount)existingCampaign;
                  
                    campaignSecondDiscount.ProductFirstId = campaignUpdateDto.ProductIds[0];
                    campaignSecondDiscount.ProductSecondDiscount = campaignUpdateDto.MaxDiscountAmount;
                    campaignSecondDiscount.IsActive = campaignUpdateDto.IsActive;
                    campaignSecondDiscount.StartDate = campaignUpdateDto.StartDate;
                    campaignSecondDiscount.EndDate = campaignUpdateDto.EndDate;
                    campaignSecondDiscount.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    campaignSecondDiscount.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    campaignSecondDiscount.CampaignName = campaignUpdateDto.CampaignName;
                    
           
                    //    ProductSecondDiscount = campaignAddDto.ProductSecondDiscount,
              
                    _campaignSecondDiscountDal.Update(campaignSecondDiscount);
                    break;
                case CampaignTypes.ProductPercentageDiscountCampaign:
                    var productPercentageDiscount = (CampaignProductPercentageDiscount)existingCampaign;

                    productPercentageDiscount.CombinedProduct = campaignUpdateDto.CombinedProduct;
                    productPercentageDiscount.MinPurchaseAmount = campaignUpdateDto.MinPurchaseAmount;
                    productPercentageDiscount.IsActive = campaignUpdateDto.IsActive;
                    productPercentageDiscount.StartDate = campaignUpdateDto.StartDate;
                    productPercentageDiscount.EndDate = campaignUpdateDto.EndDate;
                    productPercentageDiscount.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    productPercentageDiscount.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    productPercentageDiscount.CampaignName = campaignUpdateDto.CampaignName;
                    productPercentageDiscount.PercentageDiscountRate = campaignUpdateDto.PercentageDiscountRate;


                    _productPercentageDiscountCampaignDal.Update(productPercentageDiscount);
                    break;
                case CampaignTypes.CategoryPercentageDiscountCampaign:
                    var categoryPercentageDiscount = (CampaignCategoryPercentageDiscount)existingCampaign;

                    categoryPercentageDiscount.CategoryId = campaignUpdateDto.CategoryId;
                    categoryPercentageDiscount.MinPurchaseAmount = campaignUpdateDto.MinPurchaseAmount;
                    categoryPercentageDiscount.IsActive = campaignUpdateDto.IsActive;
                    categoryPercentageDiscount.StartDate = campaignUpdateDto.StartDate;
                    categoryPercentageDiscount.EndDate = campaignUpdateDto.EndDate;
                    categoryPercentageDiscount.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    categoryPercentageDiscount.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    categoryPercentageDiscount.CampaignName = campaignUpdateDto.CampaignName;
                    categoryPercentageDiscount.PercentageDiscountRate = campaignUpdateDto.PercentageDiscountRate;
   
         
              
          
                    _categoryPercentageDiscountCampaignDal.Update(categoryPercentageDiscount);
                    break;
                case CampaignTypes.GiftProductCampaign:
                    var giftProductCampaign = (CampaignGiftProduct)existingCampaign;

                    giftProductCampaign.GiftProductId = campaignUpdateDto.GiftProductId;
                    giftProductCampaign.IsActive = campaignUpdateDto.IsActive;
                    giftProductCampaign.StartDate = campaignUpdateDto.StartDate;
                    giftProductCampaign.EndDate = campaignUpdateDto.EndDate;  
                    giftProductCampaign.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    giftProductCampaign.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    giftProductCampaign.CampaignName = campaignUpdateDto.CampaignName;
                    giftProductCampaign.MinPurchaseAmount = campaignUpdateDto.MinPurchaseAmount;
                  
         
                    _giftProductCampaignDal.Update(giftProductCampaign);
                    break;
                case CampaignTypes.SpecialDiscountCampaign:
                    var specialDiscountCampaign = (CampaignSpecialDiscount)existingCampaign;

                    specialDiscountCampaign.PromotionPeriodName = campaignUpdateDto.PromotionPeriodName;
                    specialDiscountCampaign.IsActive = campaignUpdateDto.IsActive;
                    specialDiscountCampaign.StartDate = campaignUpdateDto.StartDate;
                    specialDiscountCampaign.EndDate = campaignUpdateDto.EndDate;
                    specialDiscountCampaign.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    specialDiscountCampaign.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    specialDiscountCampaign.CampaignName = campaignUpdateDto.CampaignName;
                    specialDiscountCampaign.ProductID = campaignUpdateDto.ProductId;   //
                    specialDiscountCampaign.MinPurchaseAmount  = campaignUpdateDto.MinPurchaseAmount;
            
                   
       
                    _specialDiscountCampaignDal.Update(specialDiscountCampaign);
                    break;
                case CampaignTypes.CombinedDiscountCampaign:
                    var combinedDiscountCampaign = (CampaignCombinedDiscount)existingCampaign;

                   
                    combinedDiscountCampaign.MaxDiscountAmount = campaignUpdateDto.MaxDiscountAmount;
                    combinedDiscountCampaign.IsActive = campaignUpdateDto.IsActive;
                    combinedDiscountCampaign.StartDate = campaignUpdateDto.StartDate;
                    combinedDiscountCampaign.EndDate = campaignUpdateDto.EndDate;
                    combinedDiscountCampaign.CampaignDetail = campaignUpdateDto.CampaignDetail;
                    combinedDiscountCampaign.CampaignImageUrl = campaignUpdateDto.CampaignImageUrl;
                    combinedDiscountCampaign.CampaignName = campaignUpdateDto.CampaignName;
                    combinedDiscountCampaign.PercentageDiscountRate = campaignUpdateDto.PercentageDiscountRate;

                    _combinedDiscountCampaignDal.Update(combinedDiscountCampaign);
                    break;

                default:
                    return new ErrorResult("Invalid campaign type.");
            }

            return new SuccessResult();
        }

        private ICampaign GetById(int campaignId, CampaignTypes? campaignType)
        {

            switch (campaignType)
            {
                case CampaignTypes.GiftCampaign:
                    return _campaignGiftDal.Get(b => b.Id == campaignId);
                case CampaignTypes.ProductGroupCampaign:
                    return _campaignProductGroupDal.Get(b => b.Id == campaignId);
                case CampaignTypes.SecondDiscountCampaign:
                    return _campaignSecondDiscountDal.Get(b => b.Id == campaignId);
                case CampaignTypes.ProductPercentageDiscountCampaign:
                    return _productPercentageDiscountCampaignDal.Get(b => b.Id == campaignId);
                case CampaignTypes.CategoryPercentageDiscountCampaign:
                    return _categoryPercentageDiscountCampaignDal.Get(b => b.Id == campaignId);
                case CampaignTypes.GiftProductCampaign:
                    return _giftProductCampaignDal.Get(b => b.Id == campaignId);
                case CampaignTypes.SpecialDiscountCampaign:
                    return _specialDiscountCampaignDal.Get(b => b.Id == campaignId);
                case CampaignTypes.CombinedDiscountCampaign:
                    return _combinedDiscountCampaignDal.Get(b => b.Id == campaignId);

                default:
                    return null;
            }
        }



        public IResult Delete(int campaignId, CampaignTypes campaignType)
        {
            // Kampanya türüne göre ilgili repository'yi seç ve Delete metodunu çağır
            switch (campaignType)
            {
                case CampaignTypes.GiftCampaign:
                    var giftCampaign = _campaignGiftDal.Get(b => b.Id == campaignId);
                    if (giftCampaign == null)
                        return new ErrorResult();

                    _campaignGiftDal.DeleteRange(giftCampaign.Id);
                    break;
                case CampaignTypes.ProductGroupCampaign:
                    var productGroupCampaign = _campaignProductGroupDal.Get(b => b.Id == campaignId);
                    if (productGroupCampaign == null)
                        return new ErrorResult();

                    _campaignProductGroupDal.DeleteRange(productGroupCampaign.Id);
                    break;
                case CampaignTypes.SecondDiscountCampaign:
                    var secondDiscountCampaign = _campaignSecondDiscountDal.Get(b => b.Id == campaignId);
                    if (secondDiscountCampaign == null)
                        return new ErrorResult();

                    _campaignSecondDiscountDal.DeleteRange(secondDiscountCampaign.Id);
                    break;
                case CampaignTypes.ProductPercentageDiscountCampaign:
                    var productPercentageDiscountCampaign = _productPercentageDiscountCampaignDal.Get(b => b.Id == campaignId);
                    if (productPercentageDiscountCampaign == null)
                        return new ErrorResult();

                    _productPercentageDiscountCampaignDal.DeleteRange(productPercentageDiscountCampaign.Id);
                    break;
                case CampaignTypes.CategoryPercentageDiscountCampaign:
                    var categoryPercentageDiscountCampaign = _categoryPercentageDiscountCampaignDal.Get(b => b.Id == campaignId);
                    if (categoryPercentageDiscountCampaign == null)
                        return new ErrorResult();

                    _categoryPercentageDiscountCampaignDal.DeleteRange(categoryPercentageDiscountCampaign.Id);
                    break;
                case CampaignTypes.GiftProductCampaign:
                    var giftProductCampaign = _giftProductCampaignDal.Get(b => b.Id == campaignId);
                    if (giftProductCampaign == null)
                        return new ErrorResult();

                    _giftProductCampaignDal.DeleteRange(giftProductCampaign.Id);
                    break;
                case CampaignTypes.SpecialDiscountCampaign:
                    var specialDiscountCampaign = _specialDiscountCampaignDal.Get(b => b.Id == campaignId);
                    if (specialDiscountCampaign == null)
                        return new ErrorResult();

                    _specialDiscountCampaignDal.DeleteRange(specialDiscountCampaign.Id);
                    break;
                case CampaignTypes.CombinedDiscountCampaign:
                    var combinedDiscountCampaign = _combinedDiscountCampaignDal.Get(b => b.Id == campaignId);
                    if (combinedDiscountCampaign == null)
                        return new ErrorResult();

                    _combinedDiscountCampaignDal.DeleteRange(combinedDiscountCampaign.Id);
                    break;

                default:
                    return new ErrorResult("Invalid campaign type.");
            }

            return new SuccessResult();
        }



        


        public IDataResult<List<CampaignDto>> GetAllDto()
        {
            var campaignDtos = new List<CampaignDto>();

            var giftCampaigns = _campaignGiftDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in giftCampaigns)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    Name = $"{campaign.ProductFirstName} Alışverişine - {campaign.ProductSecondName}  Hediye Ürün Kampanyası",
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.GiftCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir. Kampanya dahilinde {campaign.ProductFirstName} alışverişine sepete ekleyeceğiniz {campaign.ProductGiftName} hediye olarak satılacaktır. Her kullanıcı kampanyadan 1 (bir) kez faydalanabilecektir. Kampanya stoklarla sınırlıdır. Kadim Gross kampanyalarda değişiklik yapmak hakkını saklı tutar."

                };
                campaignDtos.Add(campaignDto);
            }

            var productGroupCampaigns = _campaignProductGroupDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in productGroupCampaigns)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    Name = $"{campaign.ProductFirstName} - {campaign.ProductSecondName} Alışverişine İndirimli Ürün Kampanyası",
                    CampaignType = CampaignTypes.ProductGroupCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir. Kampanya dahilinde {campaign.ProductFirstName} ürünü {campaign.ProductSecondName} ürünü ile birlikte alındığında toplam tutardan toplam {campaign.ProductFirstDiscount + campaign.ProductSecondDiscount}₺ indirim yapılacaktır. Kampanya stoklarla sınırlıdır. Kadim Gross kampanyalarda değişiklik yapmak hakkını saklı tutar."
                };
                campaignDtos.Add(campaignDto);
            }

            var secondDiscountCampaigns = _campaignSecondDiscountDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in secondDiscountCampaigns)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    Name = $"{campaign.ProductFirstName} Alışverişine İkinci Üründe %{campaign.ProductSecondDiscount} İndirim Kampanyası",
                    CampaignType = CampaignTypes.SecondDiscountCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir. Kampanya dahilinde {campaign.ProductFirstName} alışverişine sepete ekleyeceğiniz ikinci {campaign.ProductFirstName} üründe %{campaign.ProductSecondDiscount} indirim yapılacaktır. Her kullanıcı kampanyadan 1 (bir) kez faydalanabilecektir. Kampanya stoklarla sınırlıdır. Kadim Gross kampanyalarda değişiklik yapmak hakkını saklı tutar."
                };
                campaignDtos.Add(campaignDto);
            }

            var categoryPercentage = _categoryPercentageDiscountCampaignDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in categoryPercentage)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir."
                };
                campaignDtos.Add(campaignDto);
            }

            var specialDiscount = _specialDiscountCampaignDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in specialDiscount)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.GiftCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir."
                };
                campaignDtos.Add(campaignDto);
            }

            var productPercentageDiscount = _productPercentageDiscountCampaignDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in productPercentageDiscount)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.GiftCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir."
                };
                campaignDtos.Add(campaignDto);
            }

            var giftProductCampaign = _giftProductCampaignDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in giftProductCampaign)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.GiftCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir."
                };
                campaignDtos.Add(campaignDto);
            }



            var combinedDiscount = _combinedDiscountCampaignDal.GetAllDto(g => g.IsActive == true);
            foreach (var campaign in combinedDiscount)
            {
                var campaignDto = new CampaignDto
                {
                    Id = campaign.Id,
                    CampaignImageUrl = "",
                    EndDate = campaign.EndDate,
                    IsActive = campaign.IsActive,
                    StartDate = campaign.StartDate,
                    CampaignType = CampaignTypes.GiftCampaign,
                    CampaignDetail = $"Kampanya {campaign.StartDate?.ToString("dd.MM.yyyy")} - {campaign.EndDate?.ToString("dd.MM.yyyy")} tarihleri arasında yapılacak alışverişlerde geçerlidir."

                };
                campaignDtos.Add(campaignDto);
            }
            return new SuccessDataResult<List<CampaignDto>>(campaignDtos);
        }

        public IDataResult<T> Get<T>(CampaignTypes campaignType, int id) where T : class, IEntity, new()
        {
            IEntity campaign;

            switch (campaignType)
            {
                case CampaignTypes.GiftCampaign:
                    campaign = _campaignGiftDal.Get(x => x.Id == id);
                    break;
                case CampaignTypes.ProductGroupCampaign:
                    campaign = _campaignProductGroupDal.Get(x => x.Id == id);
                    break;
                case CampaignTypes.SecondDiscountCampaign:
                    campaign = _campaignSecondDiscountDal.Get(x => x.Id == id);
                    break;
                case CampaignTypes.ProductPercentageDiscountCampaign:
                    campaign = _productPercentageDiscountCampaignDal.Get(b => b.Id == id);
                    break;
                case CampaignTypes.CategoryPercentageDiscountCampaign:
                    campaign = _categoryPercentageDiscountCampaignDal.Get(b => b.Id == id);
                    break;
                case CampaignTypes.GiftProductCampaign:
                    campaign = _giftProductCampaignDal.Get(b => b.Id == id);
                    break;
                case CampaignTypes.SpecialDiscountCampaign:
                    campaign = _specialDiscountCampaignDal.Get(b => b.Id == id);
                    break;
                case CampaignTypes.CombinedDiscountCampaign:
                    campaign = _combinedDiscountCampaignDal.Get(b => b.Id == id);
                    break;
                default:
                    throw new ArgumentException("Invalid campaign type.");
            }

            var result = new T();
            result = (T)Convert.ChangeType(campaign, typeof(T));

            return new SuccessDataResult<T>(result);
        }

        private bool checkGift(BasketDetailDto basket, CampaignGiftDto campaignGift)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == campaignGift.ProductFirstId) ?? false;
            var isProductSecondExist = basket.BasketItems?.Any(b => b.ProductId == campaignGift.ProductSecondId) ?? false;
            var isProductGiftExist = basket.BasketItems?.Any(b => b.ProductId == campaignGift.ProductGiftId) ?? false;
            if (isProductGiftExist && isProductOneExist && isProductSecondExist && campaignGift.EndDate > DateTime.Now && campaignGift.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        private bool CampaignProductPercentage(BasketDetailDto basket, CampaignProductPercentageDiscount campaignGift)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == Convert.ToInt32(campaignGift.CombinedProduct)) ?? false;
            if (isProductOneExist &&campaignGift.EndDate > DateTime.Now && campaignGift.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool checkCombinedDiscount(BasketDetailDto basket, CampaignCombinedDiscount campaignCombined)
        {
            // Kampanya koşullarını kontrol et
            var isProductExist = basket.BasketItems?.Any(b => b.ProductId == Convert.ToInt32(campaignCombined.CombinedProduct)) ?? false;
            if (isProductExist && campaignCombined.EndDate > DateTime.Now && campaignCombined.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool checkCategoryPercentageDiscount(BasketDetailDto basket, CampaignCategoryPercentageDiscount campaignCategory)
        {
            // Kampanya koşullarını kontrol et
            var isCategoryExist = basket.BasketItems?.Any(b => b.CategoryId == campaignCategory.CategoryId) ?? false;
            if (isCategoryExist && campaignCategory.EndDate > DateTime.Now && campaignCategory.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool checkSpecialDiscount(BasketDetailDto basket, CampaignSpecialDiscount campaignSpecial)
        {
            // Kampanya koşullarını kontrol et
            var isProductExist = basket.BasketItems?.Any(b => b.ProductId == campaignSpecial.ProductID) ?? false;
            if (isProductExist && campaignSpecial.EndDate > DateTime.Now && campaignSpecial.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool checkCampaignGiftProduct(BasketDetailDto basket, CampaignGiftProduct campaignGiftProduct)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == campaignGiftProduct.GiftProductId) ?? false;
          //  var isProductSecond = basket.BasketItems?.Count(x => x.ProductId == campaignSeconds.ProductFirstId) >= 2;
            if (isProductOneExist && campaignGiftProduct.EndDate > DateTime.Now && campaignGiftProduct.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

       


        public IDataResult<List<CampaignDto>> GetAllForBasket(int basketId)
        {
            var basket = _basketDal.GetBasketWithBasketId(basketId);
            var giftCampaigns = _campaignGiftDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var productPercentageDiscount = _productPercentageDiscountCampaignDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignCombined = _combinedDiscountCampaignDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignCategory = _categoryPercentageDiscountCampaignDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignSpecial = _specialDiscountCampaignDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignGiftProduct = _giftProductCampaignDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);


           // var campaignGroups = _campaignProductGroupDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignSeconds = _campaignSecondDiscountDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);


            var campaignList = new List<CampaignDto>();
            if (basket != null)
            {
                foreach (var item in giftCampaigns)
                {
                    if (checkGift(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.ProductFirstName} Alışverişine - {item.ProductSecondName}  Hediye Ürün Kampanyası",
                            CampaignType = CampaignTypes.GiftCampaign,
                            IsActive = item.IsActive,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                           

                        });
                    }
                }
                foreach (var item in productPercentageDiscount)
                {
                    if (CampaignProductPercentage(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.CombinedProduct} ID li ürünlerde %- {item.PercentageDiscountRate}  İndirim  Kampanyası",
                            CampaignType = CampaignTypes.ProductGroupCampaign,
                            IsActive = item.IsActive,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            CampaignDetail=item.CampaignDetail,
                            CampaignImageUrl=item.CampaignImageUrl,
                        });
                    }
                }
                foreach (var item in campaignCombined)
                {
                    if (checkCombinedDiscount(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.CombinedProduct}  ID li ürünlerde % - {item.PercentageDiscountRate}  İndirim Kampanyası",
                            CampaignType = CampaignTypes.CombinedDiscountCampaign,
                            IsActive = item.IsActive,
                            StartDate = item.StartDate,
                            CampaignDetail = item.CampaignDetail,
                            CampaignImageUrl=item.CampaignImageUrl,
                            EndDate = item.EndDate,
                            
                        });
                    }
                }

                // Category Percentage Discount Campaigns
                foreach (var item in campaignCategory)
                {
                    if (checkCategoryPercentageDiscount(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.CategoryId} Kategori ıdsinde %{item.PercentageDiscountRate} İndirim Kampanyası",
                            CampaignType = CampaignTypes.CategoryPercentageDiscountCampaign
                            , IsActive = item.IsActive,
                            StartDate = item.StartDate,
                            CampaignDetail = item.CampaignDetail,
                            CampaignImageUrl=item.CampaignImageUrl,
                            EndDate = item.EndDate,

                        });
                    }
                }

                // Special Discount Campaigns
                foreach (var item in campaignSpecial)
                {
                    if (checkSpecialDiscount(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.CampaignName} Özel İndirim Kampanyası",
                            CampaignType = CampaignTypes.SpecialDiscountCampaign,
                            IsActive = item.IsActive,
                            StartDate = item.StartDate,
                            EndDate= item.EndDate,
                            CampaignDetail= item.CampaignDetail,
                            CampaignImageUrl=item.CampaignImageUrl, 
                        });
                    }
                }
                foreach (var item in campaignGiftProduct)
                {
                    if (checkCampaignGiftProduct(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"minimum {item.MinPurchaseAmount} tl siparişlerde  {item.GiftProductId} ıd li ürün hediye",
                            CampaignType = CampaignTypes.SecondDiscountCampaign,
                                IsActive = item.IsActive,
                                StartDate = item.StartDate,
                                EndDate= item.EndDate,
                                CampaignDetail= item.CampaignDetail,
                                CampaignImageUrl=item.CampaignImageUrl,
                                
                        });
                    }
                }
             

            }
            return new SuccessDataResult<List<CampaignDto>>(campaignList);
        }

        public IDataResult<List<ICampaign>> GetAll()
        {
            var allCampaigns = new List<ICampaign>();
            //aç sonradan null ayarı koy 
            //allCampaigns.AddRange(_campaignProductGroupDal.GetAll());
          //  allCampaigns.AddRange(_campaignSecondDiscountDal.GetAll());
            allCampaigns.AddRange(_combinedDiscountCampaignDal.GetAll());
            allCampaigns.AddRange(_campaignGiftDal.GetAll());
            allCampaigns.AddRange(_categoryPercentageDiscountCampaignDal.GetAll());
            allCampaigns.AddRange(_specialDiscountCampaignDal.GetAll());
            allCampaigns.AddRange(_giftProductCampaignDal.GetAll());
            allCampaigns.AddRange(_productPercentageDiscountCampaignDal.GetAll());

            return new SuccessDataResult<List<ICampaign>>(allCampaigns);
        }
    }
}

