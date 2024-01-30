
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrate;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrate;
using DataAccess.Concrate.EntityFramework;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<SliderManager>().As<ISliderService>().SingleInstance();
            builder.RegisterType<EfSliderDal>().As<ISliderDal>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<BasketManager>().As<IBasketService>().SingleInstance();
            builder.RegisterType<EfBasketDal>().As<IBasketDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimsDal>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<EfFileStorageDal>().As<IFileStorageDal>().SingleInstance();
            builder.RegisterType<FileStorageManager>().As<IFileStorageService>().SingleInstance();

            builder.RegisterType<UserFavoriteManager>().As<IUserFavoriteService>().SingleInstance();
            builder.RegisterType<EfUserFavoriteDal>().As<IUserFavoriteDal>().SingleInstance();

            builder.RegisterType<EfCouponDal>().As<ICouponDal>().SingleInstance();
            builder.RegisterType<EfUserCouponDal>().As<IUserCouponDal>().SingleInstance();

            builder.RegisterType<EfCouponCategoryDal>().As<ICategoryCouponDal>().SingleInstance(); 
            builder.RegisterType<EfCouponProductDal>().As<IProductCouponDal>().SingleInstance();
            builder.RegisterType<EfCouponTimedDal>().As<ITimedCouponDal>().SingleInstance();
            builder.RegisterType<CouponManager>().As<ICouponService>().SingleInstance();


            builder.RegisterType<SliderManager>().As<ISliderService>().SingleInstance();
            builder.RegisterType<EfSliderDal>().As<ISliderDal>().SingleInstance();

            builder.RegisterType<EfBasketItemDal>().As<IBasketItemDal>().SingleInstance();
            builder.RegisterType<BasketItemManager>().As<IBasketItemService>().SingleInstance();

            builder.RegisterType<AddressManager>().As<IAddressService>().SingleInstance();
            builder.RegisterType<EfAddressDal>().As<IAddressDal>().SingleInstance();

            builder.RegisterType<EfSubCategoryDal>().As<ISubCategoryDal>().SingleInstance();
            builder.RegisterType<SubCategoryManager>().As<ISubCategoryService>().SingleInstance();

            builder.RegisterType<MailManager>().As<IMailService>().SingleInstance();

            builder.RegisterType<CampaignManager>().As<ICampaignService>().SingleInstance();
            builder.RegisterType<EfCampaignGiftDal>().As<ICampaignGiftDal>().SingleInstance();
            builder.RegisterType<EfCampaignProductGroupDal>().As<ICampaignProductGroupDal>().SingleInstance();
            builder.RegisterType<EfCampaignSecondDiscountDal>().As<ICampaignSecondDiscountDal>().SingleInstance();
            builder.RegisterType<EfCampaignSpecialDiscountDal>().As<ICampaignSpecialDiscountDal>().SingleInstance();
            builder.RegisterType<EfCampaignProductPercentageDiscountDal>().As<ICampaignProductPercentageDiscountDal>().SingleInstance();
            builder.RegisterType<EfCampaignGiftProductDal>().As<ICampaignGiftProductDal>().SingleInstance();
            builder.RegisterType<EfCampaignCategoryPercentageDiscountDal>().As<ICampaignCategoryPercentageDiscountDal>().SingleInstance();
            builder.RegisterType<EfCampaignCombinedDiscountDal>().As<ICampaignCombinedDiscountDal>().SingleInstance();
            

            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();

            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

            builder.RegisterType<OrderItemManager>().As<IOrderItemService>().SingleInstance();
            builder.RegisterType<EfOrderItemDal>().As<IOrderItemDal>().SingleInstance();

            builder.RegisterType<ReviewManager>().As<IReviewService>().SingleInstance();
            builder.RegisterType<EfReviewDal>().As<IReviewDal>().SingleInstance();



            builder.RegisterType<BasketBoxesManager>().As<IBasketBoxesService>().SingleInstance();

            builder.RegisterType<EfDeliveryDal>().As<IDeliveryDal>().SingleInstance();
            builder.RegisterType<EfEmptyDeliveryDal>().As<IEmptyDeliveryDal>().SingleInstance();
            builder.RegisterType<EfOnlinePaymentDal>().As<IOnlinePaymentDal>().SingleInstance();
            builder.RegisterType<EfPaymentTypeDal>().As<IPaymentTypeDal>().SingleInstance();

            builder.RegisterType<EfLiveChatFAQsDal>().As<ILiveChatFAQsDal>().SingleInstance();
            builder.RegisterType<LiveChatFAQsManager>().As<ILiveChatFAQsService>().SingleInstance();


            builder.RegisterType<ShopStuffManager>().As<IShopStuffService>().SingleInstance();
            builder.RegisterType<EfMarketVariablesDal>().As<IMarketVariablesDal>().SingleInstance();
            builder.RegisterType<EfFAQsDal>().As<IFAQsDal>().SingleInstance();
            builder.RegisterType<EfUserPermissionDal>().As<IUserPermissionDal>().SingleInstance();

            builder.RegisterType<ChartManager>().As<IChartsService>().SingleInstance();

            builder.RegisterType<PayTrOrderManager>().As<IPayTrOrderService>().SingleInstance();
            builder.RegisterType<EfPaytrLogDal>().As<IPaytrLogDal>().SingleInstance();

            builder.RegisterType<EfOrderContactInfoDal>().As<IOrderContactInfoDal>().SingleInstance();


            builder.RegisterType<ResetPasswordCodeManager>().As<IResetPasswordCodeService>().SingleInstance();
            builder.RegisterType<EfResetPasswordCodeDal>().As<IResetPasswordCodeDal>().SingleInstance();

            builder.RegisterType<AdminMAnager>().As<IAdminService>().SingleInstance();
            builder.RegisterType<EfAdminDal>().As<IAdminDal>().SingleInstance();

            builder.RegisterType<ShopManager>().As<IShopService>().SingleInstance();
            builder.RegisterType<EfShopDal>().As<IShopDal>().SingleInstance();

            builder.RegisterType<ThemaManager>().As<IThemaService>().SingleInstance();
            builder.RegisterType<EfThemaDal>().As<IThemaDal>().SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
