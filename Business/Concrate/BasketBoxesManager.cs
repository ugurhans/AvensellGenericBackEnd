using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dto;

namespace Business.Concrate
{
    public class BasketBoxesManager : IBasketBoxesService
    {
        private readonly IDeliveryDal _deliveryDal;
        private readonly IEmptyDeliveryDal _emptyDeliveryDal;
        private readonly IOnlinePaymentDal _onlinePaymentDal;
        private readonly IPaymentTypeDal _paymentTypeDal;
        private readonly IMarketVariablesDal _marketVariablesDal;
        private readonly IBasketDal _basketDal;
        private readonly IAddressDal _addressDal;
        private readonly IMarketSettingDal _marketSettingDal;


        public BasketBoxesManager(IDeliveryDal deliveryDal, IEmptyDeliveryDal emptyDeliveryDal, IOnlinePaymentDal onlinePaymentDal, IPaymentTypeDal paymentTypeDal, IMarketVariablesDal marketVariablesDal, IBasketDal basketDal, IAddressDal addressDal, IMarketSettingDal marketSettingDal)
        {
            _deliveryDal = deliveryDal;
            _emptyDeliveryDal = emptyDeliveryDal;
            _onlinePaymentDal = onlinePaymentDal;
            _paymentTypeDal = paymentTypeDal;
            _marketVariablesDal = marketVariablesDal;
            _basketDal = basketDal;
            _addressDal = addressDal;
            _marketSettingDal = marketSettingDal;

        }


        public IDataResult<BasketCheckBoxTypeDto> GetBoxes()
        {
            var delivery = _deliveryDal.GetAll();
            var emptyDelivery = _emptyDeliveryDal.GetAll().FirstOrDefault();
            var onlinePayment = _onlinePaymentDal.GetAll().FirstOrDefault();
            var paymentType = _paymentTypeDal.GetAll();
            var marketVariable = _marketVariablesDal.GetAll().FirstOrDefault();
            var basketCheckBoxTypeDto = new BasketCheckBoxTypeDto()
            {
                Delivery = delivery,
                EmptyDelivery = emptyDelivery,
                OnlinePayment = onlinePayment,
                Payment = paymentType,
                MarketVariables = marketVariable
            };
            return new SuccessDataResult<BasketCheckBoxTypeDto>(basketCheckBoxTypeDto);
        }

        public IResult UpdateBoxes(BasketCheckBoxTypeDto basketCheckBoxes)
        {
            _onlinePaymentDal.Update(basketCheckBoxes.OnlinePayment);
            _emptyDeliveryDal.Update(basketCheckBoxes.EmptyDelivery);
            _marketVariablesDal.Update(basketCheckBoxes.MarketVariables);
            foreach (var item in basketCheckBoxes.Payment)
            {
                _paymentTypeDal.Update(item);
            }
            foreach (var item in basketCheckBoxes.Delivery)
            {
                _deliveryDal.Update(item);
            }
            return new SuccessResult(Messages.Updated);
        }

        public decimal GetBasketPrice(int userId)
        {
            var marketsetting = _marketSettingDal.Get(x => x.DeliveryFee != null);
            decimal deliveryFee = marketsetting?.DeliveryFee ?? 0; // Null ise 0 olarak kabul et

            var basket = _basketDal.GetSimpleByUserId(userId);
            basket.DeliveryFee = Convert.ToInt32(marketsetting?.DeliveryFee ?? deliveryFee);

            if (basket == null)
            {
                var newBasket = new Basket()
                {
                    UserId = userId,
                };
                _basketDal.Add(newBasket);
                return 0;
            }
            decimal totalBasketPrice = (basket.TotalBasketPrice + basket.DeliveryFee) ?? 0;
            return totalBasketPrice;
        }
    }
}

