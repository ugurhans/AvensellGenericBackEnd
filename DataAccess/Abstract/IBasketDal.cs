using Core.DataAccess;
using Entity.Concrete;
using Entity.Dto;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IBasketDal : IEntityRepository<Basket>
    {
        public BasketDetailDto GetBasketWithUserId(int userId);
        public BasketDetailDto GetBasketWithBasketId(int basketId);
        public BasketSimpleDto GetSimpleByUserId(int userId);
        public BasketSimpleDto GetSimpleByBasketId(int basketId);
        GraphPieDto GetTopProductInWaitinBasket(int count);

        

        //void DeleteRange(int userId);
    }
}
