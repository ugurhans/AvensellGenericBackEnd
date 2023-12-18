using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IOrderItemService
    {
        IResult Add(OrderItem orderItem);
        IDataResult<List<OrderItem>> GetAllWithOrderId(int orderId);
        IResult Delete(int id);
    }
}
