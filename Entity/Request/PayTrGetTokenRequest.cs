using System;
namespace Entity.Result
{
    public class PayTrGetTokenRequest
    {
        public string merchant_id { get; set; }
        public string user_ip { get; set; }
        public string merchant_oid { get; set; } // orderId
        public string email { get; set; }
        public decimal payment_amount { get; set; }
        public string currency { get; set; }
        public string user_basket { get; set; }
        public int no_installment { get; set; } // taksit seçeneği, 1 ise taksit yok
        public int max_installment { get; set; } // 0 ise verilebilecek en fazla taksit
        public string paytr_token { get; set; } // bizden gidecek token
        public string user_name { get; set; }
        public string user_address { get; set; }
        public string user_phone { get; set; }
        public string merchant_ok_url { get; set; }
        public string merchant_fail_url { get; set; }
        public int test_mode { get; set; } // 1 ise test
        public int debug_on { get; set; } // hata döndürülmesi için
        public int? timeout_limit { get; set; } // işlemin tamamlanması gereken süre, gönderilmezse 30 dakika olarak sayılır
        public string lang { get; set; }
    }
}

