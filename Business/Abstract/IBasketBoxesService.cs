using System;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IBasketBoxesService
    {
        public IDataResult<BasketCheckBoxTypeDto> GetBoxes();
        public IResult UpdateBoxes(BasketCheckBoxTypeDto basketCheckBoxes);
        public decimal GetBasketPrice(int userId);
    }
}

