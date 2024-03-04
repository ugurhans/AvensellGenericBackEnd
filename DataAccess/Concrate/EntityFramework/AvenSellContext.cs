using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.Concrate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore.SqlServer;
using Entity.Concrete;
using static Azure.Core.HttpHeader;
using Entity.Dto;
using Entity.Concrate.paytr;
using DataAccess.Concrate.EntityFramework;
using Entity.Enum;

namespace DataAccess.Concrete.EntityFramework
{
    public class AvenSellContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<MarketVariable> MarketVariables { get; set; }

        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<EmptyDelivery> EmptyDelivery { get; set; }
        public DbSet<OnlinePayment> OnlinePayment { get; set; }
        public DbSet<Delivery> Delivery { get; set; }

        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }
        public DbSet<CouponProduct> couponProducts { get; set; }
        public DbSet<CouponCategory> couponCategories { get; set; }
        public DbSet<CouponTimed>  couponTimeds { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CampaignGift> CampaignGifts { get; set; }
        public DbSet<CampaignProductGroup> CampaignProductGroups { get; set; }
        public DbSet<CampaignSecondDiscount> CampaignSecondDiscounts { get; set; }
        public DbSet<CampaignCategoryPercentageDiscount> CampaignCategoryPercentageDiscounts { get; set; }
        public DbSet<CampaignCombinedDiscount> CampaignCombinedDiscounts { get; set; }
        public DbSet<CampaignProductPercentageDiscount> CampaignProductPercentageDiscounts { get; set; }
        public DbSet<CampaignSpecialDiscount> CampaignSpecialDiscounts { get; set; }
        public DbSet<CampaignGiftProduct> CampaignGiftProducts { get; set; }



        public DbSet<FAQs> FAQs { get; set; }
        public DbSet<LiveChatFAQs> LiveChatFAQs { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }


        public DbSet<OrderContactInfo> OrderContactInfos { get; set; }
        public DbSet<PaytrLog> PaytrLogs { get; set; }
        public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }
        public DbSet<Admin>Admins { get; set; }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Thema> Themas { get; set; }

        public DbSet<MarketSetting> MarketSettings { get; set; }

        public DbSet<MarketSettingItem> marketSettingItems { get; set; }


    }
}
