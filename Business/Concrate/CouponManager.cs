using Bogus.DataSets;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrate.EntityFramework;
using DataAccess.Concrete.EntityFramework;
using Entity.Abtract;
using Entity.Concrate;
using Entity.Concrate.paytr;
using Entity.Dto;
using Entity.Enum;
using Entity.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Business.Concrate
{
    public class CouponManager : ICouponService
    {
        private readonly IProductCouponDal _productCouponDal;
        private readonly ICategoryCouponDal _categorycoupondal;
        private readonly ITimedCouponDal _timedCouponDal;
        private readonly IBasketDal _basketDal;

        public CouponManager(IProductCouponDal productCouponDal, ICategoryCouponDal categoryCouponDal, ITimedCouponDal timedCouponDal, IBasketDal basketDal)
        {
            _productCouponDal = productCouponDal;
            _categorycoupondal = categoryCouponDal;
            _timedCouponDal = timedCouponDal;
            _basketDal = basketDal;
        }
        public IResult Add(CouponAddDto coupon)
        {

            var coupoon = coupon.couponTypes;
            switch (coupon.couponTypes)
            {
                case CouponTypes.CouponProduct:
                    var productcoupon = new CouponProduct
                    {

                        Code = coupon.Code,
                        Discount = coupon.Discount,
                        Name = coupon.CouponName,
                        CombinedProduct = coupon.CombinedProduct,
                        IsActive = coupon.IsActive,
                        StartDate = coupon.StartDate,
                        CouponImageUrl = coupon.CouponImageUrl,
                        EndDate = coupon.EndDate,
                        MinBasketCost = coupon.MinBasketCost,


                    };
                    _productCouponDal.Add(productcoupon);
                    break;
                case CouponTypes.CouponCategory:
                    var couponcategory = new CouponCategory
                    {
                        CategoryId = coupon.CategoryId,
                        Code = coupon.Code,
                        Discount = coupon.Discount,
                        Name = coupon.CouponName,
                        CouponImageUrl = coupon.CouponImageUrl,
                        MinBasketCost = coupon.MinBasketCost,
                        EndDate = coupon.EndDate,
                        IsActive = coupon.IsActive,
                        StartDate = coupon.StartDate,

                    };
                    _categorycoupondal.Add(couponcategory);
                    break;
                case CouponTypes.CouponTimed:
                    var Timedcoupon = new CouponTimed
                    {
                        IsActive = coupon.IsActive,
                        CategoryId = coupon.CategoryId,
                        Code = coupon.Code,
                        Discount = coupon.Discount,
                        CouponImageUrl = coupon.CouponImageUrl,
                        EndTime = coupon.EndDate,
                        MinBasketCost = coupon.MinBasketCost,
                        Name = coupon.CouponName,
                        CombinedProduct = coupon.CombinedProduct,
                        StartTime = coupon.StartDate,

                    };
                    _timedCouponDal.Add(Timedcoupon);
                    break;
                default:
                    throw new ArgumentException("Invalid coupon type.");
            }
            return new SuccessResult(Messages.CampaignAdded);
        }

        public IResult Delete(int couponıd, CouponTypes couponTypes)
        {
            switch (couponTypes)
            {
                case CouponTypes.CouponProduct:
                    var couponproduct = _productCouponDal.Get(b => b.Id == couponıd);
                    if (couponproduct == null)
                        return new ErrorResult();

                    _productCouponDal.DeleteRange(couponproduct.Id);
                    break;
                case CouponTypes.CouponCategory:
                    var couponCategory = _categorycoupondal.Get(b => b.Id == couponıd);
                    if (couponCategory == null)
                        return new ErrorResult();

                    _categorycoupondal.DeleteRange(couponCategory.Id);
                    break;
                case CouponTypes.CouponTimed:
                    var timedcoupon = _timedCouponDal.Get(b => b.Id == couponıd);
                    if (timedcoupon == null)
                        return new ErrorResult();

                    _timedCouponDal.DeleteRange(timedcoupon.Id);
                    break;
                default:
                    return new ErrorResult("Invalid campaign type.");
            }

            return new SuccessResult();
        }

        public IDataResult<List<ICoupon>> GetAll()
        {
            var allCoupons = new List<ICoupon>();
            //aç sonradan null ayarı koy 
            //allCampaigns.AddRange(_campaignProductGroupDal.GetAll());
            //  allCampaigns.AddRange(_campaignSecondDiscountDal.GetAll());
            allCoupons.AddRange(_productCouponDal.GetAll());
            allCoupons.AddRange(_categorycoupondal.GetAll());
            allCoupons.AddRange(_timedCouponDal.GetAll());

            return new SuccessDataResult<List<ICoupon>>(allCoupons);
        }


        private bool CouponCategory(BasketDetailDto basket, CouponCategory couponCategory)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.CategoryId == couponCategory.CategoryId) ?? false;
            if (  isProductOneExist  && couponCategory.EndDate > DateTime.Now && couponCategory.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        private bool CouponProduct(BasketDetailDto basket, CouponProduct couponProduct)
        {
            var isProductOneExist = basket.BasketItems?.Any(b => b.ProductId == Convert.ToInt32(couponProduct.CombinedProduct)) ?? false;
            if (isProductOneExist && couponProduct.EndDate > DateTime.Now && couponProduct.StartDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private bool CouponTimed(BasketDetailDto basket, CouponTimed couponTimed)
        {
            // Kampanya koşullarını kontrol et
            var isProductExist = basket.BasketItems?.Any(b => b.ProductId == Convert.ToInt32(couponTimed.CombinedProduct) && b.CategoryId == couponTimed.CategoryId) ?? false;
            if (isProductExist && couponTimed.EndTime > DateTime.Now && couponTimed.StartTime < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public IDataResult<List<Coupon>> GetAllForBasket(int basketId)
        {
            var basket = _basketDal.GetBasketWithBasketId(basketId);
            var categorycoupon = _categorycoupondal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var productCoupon = _productCouponDal.GetAllDto(g => g.IsActive == true && g.StartDate < DateTime.Now && g.EndDate > DateTime.Now);
            var timedCoupon = _timedCouponDal.GetAllDto(g => g.IsActive == true && g.StartTime < DateTime.Now && g.EndTime > DateTime.Now);
            var couponList = new List<Coupon>();
            if (basket != null)
            {
                foreach (var item in categorycoupon)
                {
                    if (CouponCategory(basket, item))
                    {
                        couponList.Add(new Coupon()
                        {
                            Id = item.Id,
                            CouponName = $"{item.Code} koduna - {item.CategoryId}  kategori ıd li ürünlerde indirim ",
                            couponTypes = CouponTypes.CouponCategory,
                            CouponCode = item.Code,
                            CouponImageUrl=item.CouponImageUrl,
                            EndDate = item.EndDate,
                            StartDate = item.StartDate,
                            IsActive = item.IsActive,
                            MinBasketCost = item.MinBasketCost,
                            DiscountAmount  = item.Discount,
                            
                           
                        });
                    }
                }
                foreach (var item in productCoupon)
                {
                    if (CouponProduct(basket, item))
                    {
                        couponList.Add(new Coupon()
                        {
                            Id = item.Id,
                            CouponName = $"{item.Code} koduna - {item.CombinedProduct}  ıd li ürünlerde indirim ",
                            couponTypes = CouponTypes.CouponProduct,
                            CouponCode = item.Code,
                            EndDate = item.EndDate,
                            StartDate = item.StartDate,
                            IsActive = item.IsActive,
                            MinBasketCost = item.MinBasketCost,
                            DiscountAmount = item.Discount
                        });
                    }
                }
                foreach (var item in timedCoupon)
                {
                    if (CouponTimed(basket, item))
                    {
                        couponList.Add(new Coupon()
                        {
                            Id = item.Id,
                            CouponName = $"{item.Code} koduna {item.EndTime} tarihe kadar  indirim ",
                            couponTypes = CouponTypes.CouponTimed,
                            CouponCode = item.Code,
                            CouponImageUrl = item.CouponImageUrl,
                            EndDate = item.EndTime,
                            StartDate = item.StartTime,
                            IsActive = item.IsActive,
                            MinBasketCost = item.MinBasketCost,
                            DiscountAmount = item.Discount
                        });
                    }
                }
            }
            return new SuccessDataResult<List<Coupon>>(couponList);
        }

        public IDataResult<Coupon> GetCouponId(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(CouponUpdateDto coupon)
        {
            var existingCoupon = GetById(coupon.Id, coupon.couponTypes);

            if (existingCoupon == null)
            {
                return new ErrorResult();
            }


            switch (coupon.couponTypes)
            {
                case CouponTypes.CouponProduct:
                    var couponProduct = (CouponProduct)existingCoupon;

                    couponProduct.Id = coupon.Id;
                    couponProduct.Code = coupon.CouponCode;
                    couponProduct.Discount = coupon.DiscountAmount;
                    couponProduct.Name = coupon.CouponName;
                    couponProduct.CombinedProduct = coupon.CombinedProduct;
                    couponProduct.IsActive = coupon.IsActive;
                    couponProduct.StartDate = coupon.StartDate;
                    couponProduct.CouponImageUrl = coupon.CouponImageUrl;
                    couponProduct.EndDate = coupon.EndDate;
                    couponProduct.MinBasketCost = coupon.MinBasketCost;

                    _productCouponDal.Update(couponProduct);
                    break;

                case CouponTypes.CouponCategory:
                    var couponCategory = (CouponCategory)existingCoupon;

                    couponCategory.Id = coupon.Id;
                    couponCategory.Code = coupon.CouponCode;
                    couponCategory.Discount = coupon.DiscountAmount;
                    couponCategory.Name = coupon.CouponName;
                    couponCategory.CategoryId = coupon.CategoryId;
                    couponCategory.IsActive = coupon.IsActive;
                    couponCategory.StartDate = coupon.StartDate;
                    couponCategory.CouponImageUrl = coupon.CouponImageUrl;
                    couponCategory.EndDate = coupon.EndDate;
                    couponCategory.MinBasketCost = coupon.MinBasketCost;

                    _categorycoupondal.Update(couponCategory);
                    break;

                case CouponTypes.CouponTimed:
                    var Coupontimed = (CouponTimed)existingCoupon;

                    Coupontimed.Id = coupon.Id;
                    Coupontimed.Code = coupon.CouponCode;
                    Coupontimed.Discount = coupon.DiscountAmount;
                    Coupontimed.Name = coupon.CouponName;
                    Coupontimed.IsActive = coupon.IsActive;
                    Coupontimed.StartTime = coupon.StartDate;
                    Coupontimed.CouponImageUrl = coupon.CouponImageUrl;
                    Coupontimed.EndTime = coupon.EndDate;
                    Coupontimed.MinBasketCost = coupon.MinBasketCost;
                    Coupontimed.CategoryId = coupon.CategoryId;
                    Coupontimed.CombinedProduct = coupon.CombinedProduct;


                    _timedCouponDal.Update(Coupontimed);
                    break;
                default:
                    return new ErrorResult("Invalid coupon type.");
            }

            return new SuccessResult();
        }

        IDataResult<T> ICouponService.Get<T>(CouponTypes couponTypes, int id)
        {
            throw new NotImplementedException();
        }

        private ICoupon GetById(int coupondıd, CouponTypes? coupontype)
        {

            switch (coupontype)
            {
                case CouponTypes.CouponProduct:
                    return _productCouponDal.Get(b => b.Id == coupondıd);
                case CouponTypes.CouponCategory:
                    return _categorycoupondal.Get(b => b.Id == coupondıd);
                case CouponTypes.CouponTimed:
                    return _timedCouponDal.Get(b => b.Id == coupondıd);


                default:
                    return null;
            }
        }
    }
}
