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
               IBasketItemService basketItemService, IProductService productService, IBasketItemDal basketItemDal, IUserService userService, IPayTrOrderService payTrOrderService, IPaytrLogDal paytrLogDal, IHttpContextAccessor httpContextAccessor, IAddressService addressService, IOrderContactInfoDal orderContactInfoDal, IUserDal userDal, IBasketBoxesService basketBoxesService, IMarketSettingDal marketSettingDal, ICampaignCategoryPercentageDiscountItemDal campaignCategoryPercentageDiscountItemDal)

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
        }

        public IDataResult<List<OrderDto>> GetAllDto(int userId, OrderStates state)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetOrderDetail(userId, state));
        }

        public IDataResult<List<OrderDto>> GetByOrderId(int orderıd)
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetOrderDetails(orderıd));
        }

  
       public IDataResult<List<OrderBasicDto>> GetByOrderIdBasic(int UserId)
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
            var basket = _basketService.GetDetailByBasketId(order.BasketId).Data;
            var priceLast = _basketBoxesService.GetBasketPrice(basket.UserId.Value);
            var selectedAddressResult = _addressService.GetSelectedAddress(order.AddressId).Data;
            var marketSetting = _marketSettingDal.Get(x => x.Id == order.MarketId && x.DeliveryFee != null);
            decimal deliveryFee = marketSetting?.DeliveryFee ?? 0;
            var newOrder = new Order()
            {
                UserId = order.UserId,
                BasketId=order.BasketId,
                TotalOrderDiscount = order.TotalOrderDiscount,
                TotalOrderPrice = order.TotalOrderPrice,
                TotalOrderPaidPrice = order.TotalOrderPaidPrice+ (marketSetting?.DeliveryFee ?? deliveryFee),
                OrderDate = order.OrderDate,
                State = OrderStates.UNAPPROVED,
                ConfirmDate = order.ConfirmDate, 
                CompletedDate = order.CompletedDate,
                PaymentType = order.PaymentType,
                IsCampaignApplied = order.IsCampaignApplied,
                CampaignType = order.CampaignType,
                CampaignId = order.CampaignId,
                CampaignDiscount = order.CampaignDiscount,
                AddressId = order.AddressId,
                DeliveryFee = (marketSetting?.DeliveryFee ?? deliveryFee)

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
                    order.IsCampaignApplied = true;
                    order.CampaignDiscount = totalBasketPaidPrice - basket.TotalBasketPaidPrice;
                    order.CampaignId = basket.CampaignId;
                    order.CampaignType = basket.CampaignType;
                }
            }

           
            order.OrderDate = DateTime.Now;
            order.State = OrderStates.UNAPPROVED;
            order.TotalOrderDiscount = basket.TotalBasketDiscount;
            order.TotalOrderPrice = basket.TotalBasketPrice;
            order.TotalOrderPaidPrice = basket.TotalBasketPaidPrice + (marketSetting?.DeliveryFee ?? deliveryFee);
            order.ConfirmDate = null;


            var address = _addressService.GetSelectedAddress(order.AddressId).Data;
            _orderDal.Add(newOrder);

            if (address != null)
            {
                var newContact = new OrderContactInfo()
                {
                    City = address.City,
                    Header = address.Header,
                    Desc = address.Desc,
                    IsActive = address.IsActive,
                    Country = address.Country,
                    Latitude = address.Latitude,
                    Longitude = address.Longitude,
                    PostalCode = address.PostalCode,
                    Type = address.Type,
                    BuildingNo = address.BuildingNo,
                    ApartmentNo = address.ApartmentNo,
                    DateCreated = DateTime.Now,
                    Floor = address.Floor,
                    Neighborhood = address.Neighborhood,
                    Phone = address.Phone,
                    DateModified = null,
                    AddressesId = address.Id,
                    UserId = address.UserId,
                };

                _orderContactInfoDal.Add(newContact);
                order.OrderContactId = newContact.Id;
                _orderDal.Update(newOrder);
          
            }
            if (basket.BasketItems.Count < 1)
                return new ErrorResult("Sipariş oluşturmak için sepetinizde ürün bulunmalı.");

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
              
                var user = _userDal.GetProfileDto(order.UserId);
                var payTrPaymentInfo = new PayTrPaymentInfo()
                {
                    Email = user.Email,
                    PaymentAmount = (decimal)priceLast * 100,
                    MerchantOid = newOrder.Id.ToString(),
                    UserName = user.FirstName + " " + user.LastName,
                    UserAddress = address.BuildingNo + " " + address.ApartmentNo + " " + address.Floor + " " + address.Neighborhood + " " + address.Country + " " + address.City,
                    UserPhone = "+" + user.Phone,
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

        public NameValueCollection GetPaytrObject(object basket)
        {
            var requestData = new NameValueCollection();
            // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
            //
            // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.
            string merchant_id = "XXXXXX";
            string merchant_key = "YYYYYYYYYYYYYY";
            string merchant_salt = "ZZZZZZZZZZZZZZ";
            //
            // Müşterinizin sitenizde kayıtlı veya form vasıtasıyla aldığınız eposta adresi
            string emailstr = "ZZZZZZZZZZZZZZ";
            //
            // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
            int payment_amountstr = 1;
            //
            // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.
            string merchant_oid = "";
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi
            string user_namestr = "";
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi
            string user_addressstr = "";
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
            string user_phonestr = "";
            //
            // Başarılı ödeme sonrası müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi onaylayacağınız sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi onaylayacağız sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_ok_url = "http://www.siteniz.com/basarili";
            //
            // Ödeme sürecinde beklenmedik bir hata oluşması durumunda müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi iptal edeceğiniz sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi iptal edeceğiniz sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_fail_url = "http://www.siteniz.com/basarisiz";
            //        
            // !!! Eğer bu örnek kodu sunucuda değil local makinanızda çalıştırıyorsanız
            // buraya dış ip adresinizi (https://www.whatismyip.com/) yazmalısınız. Aksi halde geçersiz paytr_token hatası alırsınız.
            string user_ip = "192.16.14.250";

            //
            // ÖRNEK user_basket oluşturma - Ürün adedine göre object'leri çoğaltabilirsiniz
            object[][] user_basket = {
            new object[] {"Örnek ürün 1", "18.00", 1}, // 1. ürün (Ürün Ad - Birim Fiyat - Adet)
            new object[] {"Örnek ürün 2", "33.25", 2}, // 2. ürün (Ürün Ad - Birim Fiyat - Adet)
            new object[] {"Örnek ürün 3", "45.42", 1}, // 3. ürün (Ürün Ad - Birim Fiyat - Adet)
            };
            /* ############################################################################################ */

            // İşlem zaman aşımı süresi - dakika cinsinden
            string timeout_limit = "30";
            //
            // Hata mesajlarının ekrana basılması için entegrasyon ve test sürecinde 1 olarak bırakın. Daha sonra 0 yapabilirsiniz.
            string debug_on = "1";
            //
            // Mağaza canlı modda iken test işlem yapmak için 1 olarak gönderilebilir.
            string test_mode = "1";
            //
            // Taksit yapılmasını istemiyorsanız, sadece tek çekim sunacaksanız 1 yapın
            string no_installment = "0";
            //
            // Sayfada görüntülenecek taksit adedini sınırlamak istiyorsanız uygun şekilde değiştirin.
            // Sıfır (0) gönderilmesi durumunda yürürlükteki en fazla izin verilen taksit geçerli olur.
            string max_installment = "0";
            //
            // Para birimi olarak TL, EUR, USD gönderilebilir. USD ve EUR kullanmak için kurumsal@paytr.com 
            // üzerinden bilgi almanız gerekmektedir. Boş gönderilirse TL geçerli olur.
            string currency = "TL";
            //
            // Türkçe için tr veya İngilizce için en gönderilebilir. Boş gönderilirse tr geçerli olur.
            string lang = "";

            // Gönderilecek veriler oluşturuluyor
            NameValueCollection data = new NameValueCollection();
            data["merchant_id"] = merchant_id;
            data["user_ip"] = user_ip;
            data["merchant_oid"] = merchant_oid;
            data["email"] = emailstr;
            data["payment_amount"] = payment_amountstr.ToString();
            //
            // Sepet içerği oluşturma fonksiyonu, değiştirilmeden kullanılabilir.
            string user_basket_json = JsonConvert.SerializeObject(basket, Formatting.Indented);
            string user_basketstr = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json));
            data["user_basket"] = user_basketstr;
            //
            // Token oluşturma fonksiyonu, değiştirilmeden kullanılmalıdır.
            string Birlestir = string.Concat(merchant_id, user_ip, merchant_oid, emailstr, payment_amountstr.ToString(), user_basketstr, no_installment, max_installment, currency, test_mode, merchant_salt);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            data["paytr_token"] = Convert.ToBase64String(b);
            //
            data["debug_on"] = debug_on;
            data["test_mode"] = test_mode;
            data["no_installment"] = no_installment;
            data["max_installment"] = max_installment;
            data["user_name"] = user_namestr;
            data["user_address"] = user_addressstr;
            data["user_phone"] = user_phonestr;
            data["merchant_ok_url"] = merchant_ok_url;
            data["merchant_fail_url"] = merchant_fail_url;
            data["timeout_limit"] = timeout_limit;
            data["currency"] = currency;
            data["lang"] = lang;

            return requestData;
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
            return new SuccessDataResult<List<OrderSimpleDto>>(_orderDal.GetAllDtoSimple(x => x.State == OrderStates.WAITINGCARDPAYMENT || x.State == OrderStates.UNAPPROVED).Take(8).ToList());
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
                        City = address.City,
                        Header = address.Header,
                        Desc = address.Desc,
                        IsActive = address.IsActive,
                        Country = address.Country,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude,
                        PostalCode = address.PostalCode,
                        Type = address.Type,
                        BuildingNo = address.BuildingNo,
                        ApartmentNo = address.ApartmentNo,
                        DateCreated = DateTime.Now,
                        Floor = address.Floor,
                        Neighborhood = address.Neighborhood,
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

    }
}
