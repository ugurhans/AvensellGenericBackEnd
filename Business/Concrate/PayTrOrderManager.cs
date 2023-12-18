using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrate.paytr;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class PayTrOrderManager: IPayTrOrderService
    {
        
        private readonly IPaytrLogDal _paytrLogDal;

        public PayTrOrderManager(IPaytrLogDal paytrLogDal)
        {
            _paytrLogDal = paytrLogDal;
        }

        public string GetPaytrFrameLink(PayTrPaymentInfo payTrPaymentInfo, List<PayTrBasketItem> payTrBasketItems)
        {
            string merchant_id = "414427";
            string merchant_key = "UFFZYTSq9kc8Z7k4";
            string merchant_salt = "EJzpw7k6jw2TXJ82";
            var body = CreatePaymentBody(payTrPaymentInfo, payTrBasketItems, merchant_id, merchant_salt, merchant_key);
            if (body != null)
            {
                var result = MakePayment(body);

                // JSON yanıtını başarı durumuna göre kontrol et
                if (result.status == "success")
                {
                    var newLogErr = new PaytrLog()
                    {
                        ContentMessage = "https://www.paytr.com/odeme/guvenli/" + result.token,
                        OrderId = Convert.ToInt32(payTrPaymentInfo.MerchantOid),
                        RequestDate = DateTime.Now,
                        UserId = Convert.ToInt32(payTrPaymentInfo.UserId),
                        Success = true
                    };
                    _paytrLogDal.Add(newLogErr);
                    return "https://www.paytr.com/odeme/guvenli/" + result.token;
                }
                else
                {
                    // Hata durumunu ele alabilirsiniz
                    var newLogErr = new PaytrLog()
                    {
                        ContentMessage = "PAYTR IFRAME failed. reason:" + result.reason,
                        OrderId = Convert.ToInt32(payTrPaymentInfo.MerchantOid),
                        RequestDate = DateTime.Now,
                        UserId = Convert.ToInt32(payTrPaymentInfo.UserId),
                        Success = false,
                        ErrorType = ErrorTypes.PayTr_Error,

                    };
                    _paytrLogDal.Add(newLogErr);
                    return "PAYTR IFRAME failed. reason:" + result.reason;
                }
            }

            // Hata durumunu ele alabilirsiniz
            var newLog = new PaytrLog()
            {
                ContentMessage = "PAYTR IFRAME failed. Reason: Payment data could not be created.",
                OrderId = Convert.ToInt32(payTrPaymentInfo.MerchantOid),
                RequestDate = DateTime.Now,
                UserId = Convert.ToInt32(payTrPaymentInfo.UserId),
                Success = false,
                ErrorType = ErrorTypes.PayTr_Error,

            };
            _paytrLogDal.Add(newLog);
            return "PAYTR IFRAME failed. Reason: Payment data could not be created.";
        }

        public dynamic MakePayment(NameValueCollection data)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", data);
                string ResultAuthTicket = Encoding.UTF8.GetString(result);
                dynamic json = JValue.Parse(ResultAuthTicket);

                return json;
            }
        }
        public NameValueCollection CreatePaymentBody(PayTrPaymentInfo payTrPaymentInfo, List<PayTrBasketItem> payTrBasketItems, string merchant_id, string merchant_salt, string merchant_key)
        {
            // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
            //
            // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.

            //
            // Müşterinizin sitenizde kayıtlı veya form vasıtasıyla aldığınız eposta adresi
            string emailstr = payTrPaymentInfo.Email;
            //
            // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
            int payment_amountstr = ((int)payTrPaymentInfo.PaymentAmount);
            //
            // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.
            string merchant_oid = payTrPaymentInfo.MerchantOid;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi
            string user_namestr = payTrPaymentInfo.UserName;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi
            string user_addressstr = payTrPaymentInfo.UserAddress;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
            string user_phonestr = payTrPaymentInfo.UserPhone;
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

            //
            // ÖRNEK user_basket oluşturma - Ürün adedine göre object'leri çoğaltabilirsiniz

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

            NameValueCollection data = new NameValueCollection();
            data["merchant_id"] = merchant_id;
            data["user_ip"] = "213.14.146.127";
            data["merchant_oid"] = merchant_oid;
            data["email"] = emailstr;
            data["payment_amount"] = payment_amountstr.ToString();
            data["debug_on"] = debug_on;
            data["test_mode"] = test_mode;
            data["no_installment"] = no_installment;
            data["max_installment"] = max_installment;
            data["user_name"] = user_namestr;
            data["user_address"] = user_addressstr;
            data["user_phone"] = user_phonestr;
            data["merchant_ok_url"] = "http://admin.titiztürkiye.com/pages/paymentSuccess.html";
            data["merchant_fail_url"] = "http://admin.titiztürkiye.com/pages/paymentReject.html";
            data["timeout_limit"] = timeout_limit;
            data["currency"] = currency;
            data["lang"] = lang;


            string user_basket_json = JsonSerializer.Serialize(payTrBasketItems);

            // JSON dizesini Base64'e dönüştürme
            byte[] bytes = Encoding.UTF8.GetBytes(user_basket_json);
            string user_basketstr = Convert.ToBase64String(bytes);

            data["user_basket"] = user_basketstr;

            string Birlestir = string.Concat(merchant_id,
                "213.14.146.127", merchant_oid, emailstr, payment_amountstr.ToString(), user_basketstr, no_installment, max_installment, currency, test_mode, merchant_salt);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            data["paytr_token"] = Convert.ToBase64String(b);

            return data;
        }

    
}
}
