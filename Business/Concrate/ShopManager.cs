using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class ShopManager : IShopService
    {

        private readonly IShopDal _shopdal;

        public ShopManager(IShopDal shopDal)
        {
            _shopdal = shopDal;
        }


        public IResult Add(Shop shop)
        {
            _shopdal.Add(shop);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id)
        {
            _shopdal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<Shop> GetById(int id)
        {
            return new SuccessDataResult<Shop>(_shopdal.Get(b => b.Id == id));
        }

        public IDataResult<List<Shop>> GetAll()
        {
            return new SuccessDataResult<List<Shop>>(_shopdal.GetAll());
        }

        public IResult Update(Shop shop)
        {
           
            _shopdal.Update(shop);
            return new SuccessResult(Messages.Updated);

        }


    }
}

