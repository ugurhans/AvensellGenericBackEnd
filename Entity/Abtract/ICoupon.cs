using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Abtract
{
    public interface ICoupon
    {
        string Code { get; set; }
        decimal Discount { get; set; }
        string Name { get; set; }
    }

    public interface IProductCoupon : ICoupon   //(Ürün Bazlı Kupon):
    {
        string CombinedProduct { get; set; }
    }

    public interface ICategoryCoupon : ICoupon  // (Kategori Bazlı Kupon):
    {
        int CategoryId { get; set; } 
    }

    public interface ITimedCoupon : ICoupon  //(Zamanlı Kupon): özel günlük kupon gibi 
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }

    public interface ILimitedOfferCoupon : ICoupon  // (Limitli Teklif Kuponu):  gerek yok gibi buna 
    {
        decimal MaxLimit { get; set; }
    }


}
