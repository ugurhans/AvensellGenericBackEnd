using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entity.Concrete;

namespace DataAccess.Abstract
{
    public interface IBasketItemDal : IEntityRepository<BasketItem>
    {
        List<BasketItem> GetAllItems(int productId);
        void DeleteRange(int basketId);
        void DeleteAllBasketItemsWithBasketId(int basketId);
    }
}
