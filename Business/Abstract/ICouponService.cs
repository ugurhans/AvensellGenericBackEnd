using Core.Entities;
using Core.Utilities.Results;
using Entity.Abtract;
using Entity.Concrate;
using Entity.Dto;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICouponService
    {
        public IDataResult<List<ICoupon>> GetAll();
        public IResult Add(CouponAddDto coupon);
        public IDataResult<T> Get<T>(CouponTypes couponTypes, int id) where T : class, IEntity, new();
        public IDataResult<List<Coupon>> GetAllForBasket(int basketId);
        public IDataResult<Coupon> GetCouponId(int id);
        public IResult Update(CouponUpdateDto coupon);
        public IResult Delete(int couponıd, CouponTypes couponTypes);
    }

}
