using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate.paytr
{
    public class PayTrPaymentInfo
    {
        public string Email { get; set; }
        public decimal PaymentAmount { get; set; }
        public string MerchantOid { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string MerchantOkUrl { get; set; }
        public string MerchantFailUrl { get; set; }
        public string UserIp { get; set; }
        public string? UserId { get; set; }
        public string TimeoutLimit { get; set; }
        public string DebugOn { get; set; }
        public string TestMode { get; set; }
        public string NoInstallment { get; set; }
        public string MaxInstallment { get; set; }
        public string Currency { get; set; }
        public string Lang { get; set; }
    }
}
