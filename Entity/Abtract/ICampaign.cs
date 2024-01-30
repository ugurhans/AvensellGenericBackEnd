using System;
namespace Entity.Abtract
{
    public interface ICampaign
    {
        // ortak özellikler
        int Id { get; set; }
        bool? IsActive { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        public string? CampaignName { get; set; }
        public string? CampaignDetail { get; set; }
        public string? CampaignImageUrl { get; set; }



    }
    //Hediye Kampanyası
    public interface IGiftCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        int ProductSecondId { get; set; }
        int? ProductGift { get; set; }
       
    }
    //Ürün Grubu Kampanyası
    public interface IProductGroupCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        int ProductSecondId { get; set; }
        decimal? ProductFirstDiscount { get; set; }
        decimal? ProductSecondDiscount { get; set; }
    }
    //İkinci Ürün İndirim Kampanyası
    public interface ISecondDiscountCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        decimal? ProductSecondDiscount { get; set; }
    }


    // Kategori bazlı yüzdelik indirim kampanyası
    public interface ICategoryPercentageDiscountCampaign : ICampaign//  ilgili kategorideki ürünlere belli oranda indirim var 
    {
        //public int Id { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
    }

    // Ürün bazlı yüzdelik indirim kampanyası
    public interface IProductPercentageDiscountCampaign : ICampaign// seçili ürünler onlara indirim uygulanıyor ama belli bir rakam geçilmei sepette 
    {
        public int Id { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
    }

    // Özel indirim kampanyası
    public interface ISpecialDiscountCampaign : ICampaign //
    {
        public int Id { get; set; }
        public string PromotionPeriodName { get; set; } // "Bugüne Özel" veya "Saatlik Teklifler" gibi
    }

    // Hediye ürün kampanyası
    public interface IGiftProductCampaign : ICampaign//
    {
        public int Id { get; set; }
        public int? GiftProductId { get; set; }
    }

    // Kombine indirim kampanyası  seçili ürünler var onalrın arasından alınan ürünlere indirim uyguluyoruz ama max bir limit var mesela 1000 tl
    public interface ICombinedDiscountCampaign : ICampaign//
    {
        public int Id { get; set; }
        //public List<int> CombinedProductIds { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
    }
}

