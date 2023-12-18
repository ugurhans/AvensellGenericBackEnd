using Core;
using Entity.Dto;
using Entity.Dtos;
using Entity.DTOs;
using Entity.Enum;

public class BasketDetailDto : IDto
{
    public int BasketId { get; set; }
    public int? UserId { get; set; }
    public decimal? TotalBasketDiscount { get; set; }
    public decimal? TotalBasketPrice { get; set; }
    public decimal? TotalBasketPaidPrice { get; set; }
    public List<BasketItemDto>? BasketItems { get; set; } = new List<BasketItemDto>();



    public bool? IsCampaignApplied { get; set; }
    public CampaignTypes? CampaignType { get; set; }
    public int? CampaignId { get; set; }
    public decimal? CampaignDiscount { get; set; }



    public bool? IsCouponApplied { get; set; }
    public int? CouponId { get; set; }
    public decimal? CouponDiscount { get; set; }
}