using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Concrete;

namespace Business.Concrete
{
    public class OrderItemManager : IOrderItemService
    {
        private IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        public IResult Add(OrderItem orderItem)
        {
            _orderItemDal.Add(orderItem);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<List<OrderItem>> GetAllWithOrderId(int orderId)
        {
            return new SuccessDataResult<List<OrderItem>>(_orderItemDal.GetAll(oi => oi.OrderId == orderId));
        }

        public IResult Delete(int id)
        {
            _orderItemDal.Delete(id);
            return new SuccessResult("Başarıyla Silindi");
        }
    }
}