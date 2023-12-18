using System;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IBasketBoxesService
    {
        IDataResult<BasketCheckBoxTypeDto>? GetBoxes();
        IResult UpdateBoxes(BasketCheckBoxTypeDto basketCheckBoxes);
        decimal GetBasketPrice(int userId);//
    }
}

