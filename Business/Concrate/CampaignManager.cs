using System;
using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Abtract;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Dto;
using Entity.Enum;

namespace Business.Concrate
{
    public class CampaignManager : ICampaignService
    {
        private readonly ICampaignGiftDal _campaignGiftDal;
        private readonly ICampaignProductGroupDal _campaignProductGroupDal;
        private readonly ICampaignSecondDiscountDal _campaignSecondDiscountDal;
        private readonly IBasketDal _basketDal;

        public CampaignManager(ICampaignGiftDal campaignGiftDal, ICampaignProductGroupDal campaignProductGrouptDal, ICampaignSecondDiscountDal campaignSecondDiscountDal, IBasketDal basketDal)
        {
            _campaignGiftDal = campaignGiftDal;
            _campaignProductGroupDal = campaignProductGrouptDal;
            _campaignSecondDiscountDal = campaignSecondDiscountDal;
            _basketDal = basketDal;
        }

        public IResult Add(CampaignAddDto campaignAddDto)
        {
            // campaignType parametresine göre ilgili kampanya nesnesini oluşturuyoruz.
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
                        ProductSecondId = (int)campaignAddDto.ProductSecondId
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
                        EndDate = campaignAddDto.EndDate
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
                        EndDate = campaignAddDto.EndDate
                    };
                    _campaignSecondDiscountDal.Add(campaignSecondDiscount);
                    break;
                default:
                    throw new ArgumentException("Invalid campaign type.");
            }
            return new SuccessResult(Messages.CampaignAdded);
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
        private bool checkProductGroup(BasketDetailDto basket, CampaignProductGroupDto campaignGift)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == campaignGift.ProductFirstId) ?? false;
            var isProductSecondExist = basket.BasketItems?.Any(b => b.ProductId == campaignGift.ProductSecondId) ?? false;
            if (isProductOneExist && isProductSecondExist && campaignGift.EndDate > DateTime.Now && campaignGift.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool checkSecondary(BasketDetailDto basket, CampaignSecondDiscountDto campaignSeconds)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == campaignSeconds.ProductFirstId) ?? false;
            var isProductSecond = basket.BasketItems?.Count(x => x.ProductId == campaignSeconds.ProductFirstId) >= 2;
            if (isProductOneExist && isProductSecond && campaignSeconds.EndDate > DateTime.Now && campaignSeconds.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public IDataResult<List<CampaignDto>> GetAllForBasket(int basketId)
        {
            var basket = _basketDal.GetBasketWithBasketId(basketId);
            var giftCampaigns = _campaignGiftDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var campaignGroups = _campaignProductGroupDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
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
                            CampaignType = CampaignTypes.GiftCampaign
                        });
                    }
                }
                foreach (var item in campaignGroups)
                {
                    if (checkProductGroup(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.ProductFirstName} - {item.ProductSecondName} Alışverişine İndirimli Ürün Kampanyası",
                            CampaignType = CampaignTypes.ProductGroupCampaign,
                        });
                    }
                }
                foreach (var item in campaignSeconds)
                {
                    if (checkSecondary(basket, item))
                    {
                        campaignList.Add(new CampaignDto()
                        {
                            Id = item.Id,
                            Name = $"{item.ProductFirstName} Alışverişine İkinci Üründe %{item.ProductSecondDiscount} İndirim Kampanyası",
                            CampaignType = CampaignTypes.SecondDiscountCampaign
                        });
                    }
                }
            }
            return new SuccessDataResult<List<CampaignDto>>(campaignList);
        }
    }
}

