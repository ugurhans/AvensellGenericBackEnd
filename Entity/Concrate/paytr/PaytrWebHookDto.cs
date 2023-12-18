using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrate.paytr
{
    public class PaytrWebHookDto:IDto
    {
        public string merchant_oid { get; set; }
        public string status { get; set; }
        public int total_amount { get; set; }
        public string hash { get; set; }
        public string failed_reason_code { get; set; }
        public string failed_reason_msg { get; set; }
        public bool test_mode { get; set; }
        public string payment_type { get; set; }
        public string currency { get; set; }
        public int payment_amount { get; set; }
    }
}
