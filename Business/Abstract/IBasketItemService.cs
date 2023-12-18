using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.Dto;
using Entity.DTOs;
using Entity.Request;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Abstract
{
    public interface IBasketItemService
    {
        IDataResult<List<BasketItem>> GetAll(int basketId);
        IDataResult<List<BasketItem>> GetAllItems(int productId);
        CountResult Add(BasketAddItemDto basketItem);
        CountResult Delete(int productId, int basketId);
        CountResult DeleteWithId(int id);
        IResult DeleteAllItem(int productId, int basketId);
        IResult DeleteAllBasket(int basketId);
    }
}



