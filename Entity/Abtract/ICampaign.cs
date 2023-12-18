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
    }

    public interface IGiftCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        int ProductSecondId { get; set; }
        int? ProductGift { get; set; }
    }

    public interface IProductGroupCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        int ProductSecondId { get; set; }
        decimal? ProductFirstDiscount { get; set; }
        decimal? ProductSecondDiscount { get; set; }
    }

    public interface ISecondDiscountCampaign : ICampaign
    {
        int ProductFirstId { get; set; }
        decimal? ProductSecondDiscount { get; set; }
    }
}

