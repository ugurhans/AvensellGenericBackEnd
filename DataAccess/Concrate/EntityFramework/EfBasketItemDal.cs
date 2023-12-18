using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketItemDal : EfEntityRepositoryBase<BasketItem, AvenSellContext>, IBasketItemDal
    {
        public List<BasketItem> GetAllItems(int productId)
        {
            using AvenSellContext context = new AvenSellContext();
            var result = from bi in context.BasketItems where bi.ProductId == productId select bi;
            return result.ToList();
        }

        public void DeleteRange(int basketId)
        {
            using AvenSellContext context = new AvenSellContext();
            context.BasketItems.RemoveRange(context.BasketItems.Where(bi => bi.BasketId == basketId));
            context.SaveChanges();
        }

        public void DeleteAllBasketItemsWithBasketId(int basketId)
        {
            using AvenSellContext context = new AvenSellContext();
            context.BasketItems.RemoveRange(context.BasketItems.Where(bi => bi.BasketId == basketId));
            context.SaveChanges();
        }
    }
}
