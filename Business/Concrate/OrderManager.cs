using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System.Text;
using Entity.Concrete;
using Entity.Concrate;
using Entity.Dto;
using Entity.Enum;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Entity.Concrate.paytr;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
       
        IOrderDal _orderDal;
        IBasketService _basketService;
        IOrderItemService _orderItemService;
        private IBasketItemService _basketItemService;
        IProductService _productService;
        IBasketItemDal _basketItemDal;
        //IShopDal _shopDal;
        IMarketSettingDal _marketSettingDal;
        ICampaignCategoryPercentageDiscountItemDal _CampaignCategoryPercentageDiscountItemDal;
        private readonly ICityDal _cityDal;
        private readonly IDistrictDal _districtDal;
        private readonly INeighborhoodDal _neighborhoodDal;
        private readonly IPayTrOrderService _payTrOrderService;
        private readonly IPaytrLogDal _paytrLogDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressService _addressService;
        private readonly IBasketDal _basketDal;
        private readonly IAddressDal _addressDal;
        private readonly IOrderContactInfoDal _orderContactInfoDal;
        private readonly IUserDal _userDal;
        private readonly IBasketBoxesService _basketBoxesService;
        private readonly ICouponDal _couponDal;
        private readonly IOrderRepeatDal _orderRepeatDal;
        private readonly IBasketDal basketDal;
        private readonly IUserService _userService;



        public OrderManager(IOrderDal orderDal, IBasketService basketService, IOrderItemService orderItemService,
               IBasketItemService basketItemService, IProductService productService, IBasketItemDal basketItemDal, IUserService userService, IPayTrOrderService payTrOrderService, IPaytrLogDal paytrLogDal, IHttpContextAccessor httpContextAccessor, IAddressService addressService, IOrderContactInfoDal orderContactInfoDal, IUserDal userDal, IBasketBoxesService basketBoxesService, IMarketSettingDal marketSettingDal, ICampaignCategoryPercentageDiscountItemDal campaignCategoryPercentageDiscountItemDal, ICityDal cityDal, IDistrictDal districtDal, INeighborhoodDal neighborhoodDal)

        {
            _orderDal = orderDal;
            _basketService = basketService;
            _orderItemService = orderItemService;
            _basketItemService = basketItemService;
            _productService = productService;
            _basketItemDal = basketItemDal;
            _payTrOrderService = payTrOrderService;
            _paytrLogDal = paytrLogDal;
            _httpContextAccessor = httpContextAccessor;
            _addressService = addressService;
            _orderContactInfoDal = orderContactInfoDal;
            _userDal = userDal;
            _basketBoxesService = basketBoxesService;
            _marketSettingDal = marketSettingDal;
            _CampaignCategoryPercentageDiscountItemDal = campaignCategoryPercentageDiscountItemDal;
            _cityDal = cityDal;
            _districtDal = districtDal;
            _neighborhoodDal = neighborhoodDal;
        }

        public IDataResult<List<OrderDto>> GetAllDto(int userId, OrderStates state)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetOrderDetail(userId, state));
        }

        public IDataResult<List<OrderDto>> GetByOrderId(int orderıd)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetOrderDetails(orderıd));
        }

  
       public IDataResult<List<OrderBasicDto>> GetOrderBasicByUserId(int UserId)
        {
            return new SuccessDataResult<List<OrderBasicDto>>(_orderDal.GetOrderDetailBasic(UserId));
        }



        public IResult RepeatOrder(int orderId)
        {
            var marketSetting = _marketSettingDal.Get(x => x.DeliveryFee != null);
            decimal deliveryFee = marketSetting?.DeliveryFee ?? 0; // Null ise 0 olarak kabul et
            var order = _orderDal.Get(o => o.Id == orderId);
            order.DeliveryFee = Convert.ToInt32(marketSetting?.DeliveryFee ?? deliveryFee);
            order.OrderItems = _orderItemService.GetAllWithOrderId(order.Id).Data;
            if (order.OrderItems != null)
            {
                var basket = _basketService.GetDetailByUserId(order.UserId).Data;
                var items = _basketItemService.GetAll(basket.BasketId).Data;
                foreach (var item in items)
                {
                    _basketItemService.DeleteAllItem(item.Id, item.BasketId);
                }
                var errorProducts = new List<string>();

                foreach (var item in order.OrderItems)
                {
                    var product = _productService.GetById((int)item.ProductId).Data;
                    if (product.UnitsInStock > item.ProductCount)
                    {
                        _basketItemDal.Add(new BasketItem
                        {
                            BasketId = basket.BasketId,
                            ProductId = product.Id,
                            ProductCount = item.ProductCount
                        });
                    }
                    if (!(product.UnitsInStock > item.ProductCount))
                    {
                        errorProducts.Add(item.ProductName);
                    }
                }
                if (errorProducts.Count <= 0)
                {
                    return new SuccessResult("Başarıyla sepete eklendi.");
                }
                else if (errorProducts.Count == 0)
                {
                    var message = "";

                    foreach (var errors in errorProducts)
                    {
                        message += errors + " ,";
                    }
                    return new SuccessResult("Stokta bulunmayan " + message + "haricindeki ürünler başarıyla sepete eklendi.");

                }
                else if (errorProducts.Count > 0)
                {
                    var message = "";

                    foreach (var errors in errorProducts)
                    {
                        message += errors + " ,";
                    }
                    return new SuccessResult("Stokta bulunmayan " + message + "haricindeki ürünler başarıyla sepete eklendi.");
                }
            }

            return new ErrorResult("Eski sipariş tekrarlanamıyor.");
        }

        public IResult Delete(int orderId)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order != null) order.State = OrderStates.DELETED;
            _orderDal.Update(order);
            //var orderItems = _orderItemService.GetAllWithOrderId(orderId);
            //if (orderItems.Success && orderItems.Data != null)
            //{
            //    foreach (var item in orderItems.Data)
            //    {
            //        _orderItemService.Delete(item.Id);
            //    }
            //}
            return new SuccessResult("Sipariş Başarıyla Silindi");
        }


        public IDataResult<List<OrderDto>> GetAllByState(OrderStates state, DateTime? dateStart, DateTime? dateEnd)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetOrderDetailByState(state, dateStart, dateEnd));
        }

        public IResult CancelOrder(int orderId)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
            {
                return new ErrorResult("Sipariş zaten iptal edildi veya bulunamadı ");
            }

            order.State = OrderStates.CANCELED;
            _orderDal.Update(order);
            return new SuccessResult("Sipariş İptali Tamamlandı.");
        }

        public IResult UpdateWithState(int orderId, OrderStates state)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
            {
                return new ErrorResult("Sipariş zaten iptal edildi veya bulunamadı ");
            }

            order.State = state;
            _orderDal.Update(order);
            return new SuccessResult("Sipariş İptali Tamamlandı.");
        }

        public IDataResult<int> GetBadgeWithState(OrderStates stateId)
        {
            return new SuccessDataResult<int>(_orderDal.GetBadgeWithState(o => o.State == stateId));
        }

        public async Task<IResult> AddPayTr(OrderCreateRequestDto order)
        {
            var user = _userDal.Get(user => user.Id == order.UserId);
            if (user == null)
                return new ErrorResult("User Not Found");
            var basket = _basketService.GetDetailByUserId(user.Id).Data;
            if(basket == null)
                return new ErrorResult("Basket Not Found");
            var marketSetting = _marketSettingDal.GetAll(x=>x.DeliveryFee != null).FirstOrDefault();
            if(marketSetting == null)
                return new ErrorResult("Market Settings Not Found");   
            
            var priceLast = _basketBoxesService.GetBasketPrice(basket.UserId.Value);
            if (basket.BasketItems.Count < 1)
                return new ErrorResult("Sipariş oluşturmak için sepetinizde ürün bulunmalı.");
            
            var newOrder = new Order()
            {
                UserId = order.UserId,
                BasketId=basket.BasketId,
                TotalOrderDiscount = basket.TotalBasketDiscount,
                TotalOrderPrice = basket.TotalBasketPrice,
                TotalOrderPaidPrice = basket.TotalBasketPaidPrice + (marketSetting.DeliveryFee),
                OrderDate = DateTime.Now,
                State = OrderStates.UNAPPROVED,
                ConfirmDate = null, 
                CompletedDate = null,
                PaymentType = order.PaymentType,
                IsCampaignApplied = basket.IsCampaignApplied,
                CampaignType = basket.CampaignType,
                CampaignId = basket.CampaignId,
                CampaignDiscount = basket.CampaignDiscount,
                AddressId = order.AddressId,
                DeliveryFee = (marketSetting?.DeliveryFee)
            };
         
            if (basket.IsCampaignApplied == true && basket.CampaignId != null)
            {
                IDataResult<BasketDetailDto> campaignResult = null;

                switch (basket.CampaignType)
                {
                    case CampaignTypes.GiftCampaign:
                        campaignResult = _basketService.ApplyGiftCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductGroupCampaign:
                        campaignResult = _basketService.ApplyProductGroupCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.SecondDiscountCampaign:
                        campaignResult = _basketService.ApplySecondDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.SpecialDiscountCampaign:
                        campaignResult = _basketService.ApplySpecialDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.GiftProductCampaign:
                        campaignResult = _basketService.ApplyGiftProductCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CombinedDiscountCampaign:
                        campaignResult = _basketService.ApplyCombinedDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.CategoryPercentageDiscountCampaign:
                        campaignResult = _basketService.ApplyCategoryPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    case CampaignTypes.ProductPercentageDiscountCampaign:
                        campaignResult = _basketService.ApplyProductPercentageDiscountCampaign((int)basket.CampaignId, basket.BasketId);
                        break;
                    default:
                        campaignResult = new ErrorDataResult<BasketDetailDto>("Kampanya Uygulanırken Sorun Yaşandı.");
                        break;
                }

                if (campaignResult.Success)
                {
                    var totalBasketPaidPrice = basket.TotalBasketPaidPrice;
                    basket = campaignResult.Data;
                    newOrder.IsCampaignApplied = true;
                    newOrder.CampaignDiscount = totalBasketPaidPrice - basket.TotalBasketPaidPrice;
                    newOrder.CampaignId = basket.CampaignId;
                    newOrder.CampaignType = basket.CampaignType;
                }
            }
            newOrder.OrderDate = DateTime.Now;
            newOrder.State = OrderStates.UNAPPROVED;
            newOrder.TotalOrderDiscount = basket.TotalBasketDiscount;
            newOrder.TotalOrderPrice = basket.TotalBasketPrice;
            newOrder.TotalOrderPaidPrice = basket.TotalBasketPaidPrice + (marketSetting?.DeliveryFee);
            newOrder.ConfirmDate = null;
            
          var address = _addressService.GetSelectedAddress(order.AddressId).Data;
            _orderDal.Add(newOrder);

            if (address != null)
            {
            
                var newContact = new OrderContactInfo()
                {
                    City =_cityDal.Get(x=>x.Id == address.CityId).Name,
                    Header = address.Header,
                    Desc = address.Desc,
                    IsActive = address.IsActive,
                    District = _districtDal.Get(x=>x.Id == address.DistrictId).Name,
                    Latitude = address.Latitude,
                    Longitude = address.Longitude,
                    PostalCode = address.PostalCode,
                    Type = address.Type,
                    BuildingNo = address.BuildingNo,
                    ApartmentNo = address.ApartmentNo,
                    DateCreated = DateTime.Now,
                    Floor = address.Floor,
                    Neighborhood = _neighborhoodDal.Get(x=>x.Id == address.NeighborhoodId).Name,
                    Phone = address.Phone,
                    DateModified = null,
                    AddressesId = address.Id,
                    UserId = address.UserId,
                };

                _orderContactInfoDal.Add(newContact);
                newOrder.OrderContactId = newContact.Id;
                _orderDal.Update(newOrder);
          
            }
         
            foreach (var item in basket.BasketItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = item.ProductId,
                    BasketItemId = item.Id,
                    ProductName = item.Name,
                    TotalPrice = item.TotalPrice,
                    TotalDiscount = item.TotalDiscount,
                    TotalPaidPrice = item.TotalPaidPrice,
                    ProductCount = item.ProductCount,
                    ProductPrice = item.UnitPrice,
                    ProductDiscountPrice = item.UnitDiscount,
                    ProductPaidPrice = item.UnitPaidPrice,
                };

                var product = _productService.GetById(item.ProductId).Data;

                if (product.UnitsInStock < orderItem.ProductCount)
                {
                    _orderDal.Delete(newOrder.Id);
                    _basketItemService.DeleteAllItem(item.Id, item.BasketId);
                    return new ErrorResult(product.Name + " isimli ürününden elimizde yeterince mevcut değil. Sepetinizden kaldırıldı.");
                }

                _orderItemService.Add(orderItem);
                product.UnitsInStock -= item.ProductCount ?? 0;
                _productService.Update(product);
            }
            _basketService.DeleteBasket(basket.BasketId);
            if (order.PaymentType == PaymentTypes.CashOnDelivery)
            {
                return new SuccessResult("Siparişiniz başarıyla alındı.");
            }
            else if (order.PaymentType == PaymentTypes.OnlinePayment || order.PaymentType == PaymentTypes.CreditCardOnDelivery)

            {
                var payTrPaymentInfo = new PayTrPaymentInfo()
                {
                    Email = user.Email,
                    PaymentAmount = (decimal)priceLast * 100,
                    MerchantOid = newOrder.Id.ToString(),
                    UserName = user.FirstName + " " + user.LastName,
                    UserAddress = address.BuildingNo + " " + address.ApartmentNo + " " + address.Floor + " " +  _districtDal.Get(x=>x.Id == address.NeighborhoodId).Name+ " " +_districtDal.Get(x=>x.Id == address.DistrictId).Name + " " + _districtDal.Get(x=>x.Id == address.CityId).Name,
                    UserPhone = "+" + user.PhoneNumber,
                    MerchantOkUrl = "http://admin.titiztürkiye.com/pages/paymentSuccess.html",
                    MerchantFailUrl = "http://admin.titiztürkiye.com/pages/paymentReject.html",
                    UserIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() ?? "2.56.152.68",
                    Currency = "TL",
                    NoInstallment = "yes"
                };

                var newBasketItems = new List<PayTrBasketItem>
        {
            new PayTrBasketItem()
            {
               //UnitPrice = (decimal)order.TotalOrderPaidPrice
                 UnitPrice= priceLast
            }
        };
                var result = _payTrOrderService.GetPaytrFrameLink(payTrPaymentInfo, newBasketItems);
                return new SuccessResult("Order Added and paytr is: " + result);
            }

            return new ErrorResult("Ödeme tipi geçersiz.");
        }

        public IResult OrderComplate(PaytrWebHookDto paytrWebHookDto)
        {
            var order = _orderDal.Get(x => x.Id.ToString() == paytrWebHookDto.merchant_oid);
            if (order == null)
            {
                var newLogErr = new PaytrLog()
                {
                    ContentMessage = paytrWebHookDto.status,
                    OrderId = order.Id,
                    RequestDate = DateTime.Now,
                    UserId = order.UserId,
                    Success = false,
                    ErrorType = ErrorTypes.PayTr_Error,

                };
                _paytrLogDal.Add(newLogErr);
                return new ErrorResult();
            }
            if (paytrWebHookDto.status == "failed")
            {
                var errLog = new PaytrLog()
                {
                    ContentMessage = paytrWebHookDto.failed_reason_code + " : " + paytrWebHookDto.status,
                    OrderId = order.Id,
                    RequestDate = DateTime.Now,
                    UserId = order.UserId,
                    Success = true,
                };
                _paytrLogDal.Add(errLog);
                return new ErrorResult();
            }
            order.State = OrderStates.APPROVED;
            order.PaymentApprovedDate = DateTime.Now; 
            var newLog = new PaytrLog()
            {
                ContentMessage = paytrWebHookDto.status,
                OrderId = order.Id,
                RequestDate = DateTime.Now,
                UserId = order.UserId,
                Success = true,
            };
            _paytrLogDal.Add(newLog);
            _orderDal.Update(order);
            return new SuccessResult();
        }

        public IResult CompleteOrder(int orderId)
        {
            var order = _orderDal.Get(o => o.Id == orderId);
            if (order == null)
            {
                return new ErrorResult("Sipariş zaten onaylandı veya bulunamadı ");
            }

            order.State = OrderStates.COMPLETED;
            _orderDal.Update(order);
            return new SuccessResult("Sipariş Tamamlandı.");
        }

        public dynamic SendRequestFromToken(NameValueCollection requestData)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", requestData);
                string ResultAuthTicket = Encoding.UTF8.GetString(result);
                dynamic json = JValue.Parse(ResultAuthTicket);

                if (json.status == "success")
                {
                    var frameSrc = "https://www.paytr.com/odeme/guvenli/" + json.token;
                    return new SuccessDataResult<string>(frameSrc);
                }
                else
                {
                    return new ErrorResult("PAYTR IFRAME failed. reason:" + json.reason + "");
                }
            }
        }

       

        public IDataResult<int> GetAllCount()
        {
            return new SuccessDataResult<int>(_orderDal.GetAll().Count);
        }

        public IDataResult<List<OrderDto>> GetOrdersWithCount(int orderCount)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetAllDto().Take(orderCount).ToList());
        }

        public IDataResult<OrderPartDto> GetAllWithParts()
        {
            var UnApprovedOrders = _orderDal.GetAllDtoSimple(x => x.State == OrderStates.UNAPPROVED);
            UnApprovedOrders.AddRange(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.WAITINGCARDPAYMENT));

            var WaitingOrders = _orderDal.GetAllDtoSimple(x => x.State == OrderStates.APPROVED);
            WaitingOrders.AddRange(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.PREPARING));
            WaitingOrders.AddRange(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.ONROAD));
            WaitingOrders.AddRange(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.DELIVERED));

            var CompletedOrders = _orderDal.GetAllDtoSimple(x => x.State == OrderStates.COMPLETED);

            var CanceledOrders = _orderDal.GetAllDtoSimple(x => x.State == OrderStates.CANCELED);
            CanceledOrders.AddRange(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.DELETED));

            return new SuccessDataResult<OrderPartDto>(new OrderPartDto()
            {
                WaitingOrders = WaitingOrders,
                UnApproved = UnApprovedOrders,
                CompletedOrders = CompletedOrders,
                CanceledOrders = CanceledOrders
            });
        }

        public IDataResult<OrderDto> GetById(int orderId)
        {
            return new SuccessDataResult<OrderDto>(_orderDal.GetDto(x => x.OrderId == orderId));
        }

        public IDataResult<List<OrderSimpleDto>> GetLastOrdersSimple()
        {
            return new SuccessDataResult<List<OrderSimpleDto>>(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.WAITINGCARDPAYMENT || x.State == OrderStates.UNAPPROVED).OrderByDescending(x=>x.OrderDate).Take(8).ToList());
        }

        public IDataResult<GraphPieDto> GetTopCategories(int count)
        {
            return new SuccessDataResult<GraphPieDto>(_orderDal.GetTopCategories(count));
        }

        public IDataResult<GraphPieDto> GetProductForLowSelling(int count)
        {
            return new SuccessDataResult<GraphPieDto>(_orderDal.GetProductForLowSelling(count));
        }

        public IDataResult<RevenueAndProfitDto> GetCostForMarket()
        {
            return new SuccessDataResult<RevenueAndProfitDto>(_orderDal.GetCostForMarket());
        }

        public  IResult Update(OrderUpdateDto order)
        {
            //  var existingOrder = _orderDal.Get(o => o.Id == order.OrderId);
            var existingOrder = _orderDal.GetOrderDetails(order.OrderId);
            // Eğer Order varsa güncelle, yoksa hata ver
            Console.WriteLine(existingOrder);
            if (existingOrder != null)
            {
                var selectedAddressResult = _addressService.GetSelectedAddress(order.AddressId).Data;
                var newOrder = new Order()
                {
                    Id = order.OrderId,
                    UserId = order.UserId,
                    BasketId = order.BasketId,
                    TotalOrderDiscount = order.TotalOrderDiscount,
                    TotalOrderPrice = order.TotalOrderPrice,
                    TotalOrderPaidPrice = order.TotalOrderPaidPrice,
                    OrderDate = order.OrderDate,
                    State = order.State,
                    ConfirmDate = order.ConfirmDate,
                    CompletedDate = order.CompletedDate,
                    AddressId = order.AddressId,

                };


                order.OrderDate = DateTime.Now;
                order.State = OrderStates.UNAPPROVED;
                order.ConfirmDate = null;


                var address = _addressService.GetSelectedAddress(order.AddressId).Data;
                _orderDal.Update(newOrder);

                if (address != null)
                {
                    var newContact = new OrderContactInfo()
                    {
                        City =_cityDal.Get(x=>x.Id == address.CityId).Name,
                        Header = address.Header,
                        Desc = address.Desc,
                        IsActive = address.IsActive,
                        District = _districtDal.Get(x=>x.Id == address.DistrictId).Name,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude,
                        PostalCode = address.PostalCode,
                        Type = address.Type,
                        BuildingNo = address.BuildingNo,
                        ApartmentNo = address.ApartmentNo,
                        DateCreated = DateTime.Now,
                        Floor = address.Floor,
                        Neighborhood = _neighborhoodDal.Get(x=>x.Id == address.NeighborhoodId).Name,
                        Phone = address.Phone,
                        DateModified = null,
                        AddressesId = address.Id,
                        UserId = address.UserId,
                    };

                   // _orderContactInfoDal.Update(newContact);
                   // order.OrderContactId = newContact.Id;
                    _orderDal.Update(newOrder);

                }
            }
            else 
            {
                return new ErrorResult("güncellenemedi.");
            }
            return new SuccessResult();
           
        }

        public IResult OrderComplateForPaytr(PaytrWebHookDto paytrWebHookDto)
        {
            var order = _orderDal.Get(x => x.Id.ToString() == paytrWebHookDto.merchant_oid);
            if (order == null)
            {
                var newLogErr = new PaytrLog()
                {
                    ContentMessage = paytrWebHookDto.status,
                    OrderId = order.Id,
                    RequestDate = DateTime.Now,
                    UserId = order.UserId,
                    Success = false,
                    ErrorType = ErrorTypes.PayTr_Error,

                };
                _paytrLogDal.Add(newLogErr);
                return new ErrorResult();
            }
            if (paytrWebHookDto.status == "failed")
            {
                var errLog = new PaytrLog()
                {
                    ContentMessage = paytrWebHookDto.failed_reason_code + " : " + paytrWebHookDto.status,
                    OrderId = order.Id,
                    RequestDate = DateTime.Now,
                    UserId = order.UserId,
                    Success = true,
                };
                _paytrLogDal.Add(errLog);
                return new ErrorResult();
            }
            order.State = OrderStates.APPROVED;
            order.PaymentApprovedDate = DateTime.Now;
            var newLog = new PaytrLog()
            {
                ContentMessage = paytrWebHookDto.status,
                OrderId = order.Id,
                RequestDate = DateTime.Now,
                UserId = order.UserId,
                Success = true,
            };
            _paytrLogDal.Add(newLog);
            _orderDal.Update(order);
            return new SuccessResult();
        }
    }
}
