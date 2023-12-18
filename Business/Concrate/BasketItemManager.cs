using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Dto;
using Entity.DTOs;
using Entity.Request;

namespace Business.Concrete
{
    public class BasketItemManager : IBasketItemService
    {
        private IBasketItemDal _basketItemDal;
        private IProductService _productService;
        public BasketItemManager(IBasketItemDal basketItemDal, IProductService productService)
        {
            _basketItemDal = basketItemDal;
            _productService = productService;
        }

        public IDataResult<List<BasketItem>> GetAll(int basketId)
        {
            return new SuccessDataResult<List<BasketItem>>(_basketItemDal.GetAll(b => b.BasketId == basketId));
        }

        public CountResult DeleteWithId(int id)
        {
            var product = _basketItemDal.Get(b => b.Id == id);
            if (product != null && product.ProductCount.HasValue && product.ProductCount > 0 && product.ProductCount != 1)
            {
                product.ProductCount--;
                _basketItemDal.Update(product);
                return new CountResult("Başarıyla silindi.", product.ProductCount, true);
            }
            else if (product != null && product.ProductCount.HasValue && product.ProductCount == 1)
            {
                _basketItemDal.Delete(id);
                return new CountResult(Messages.Deleted, true);
            }

            return new CountResult("Ürün Sepette Bulunamadı", false);
        }

        public CountResult Add(BasketAddItemDto basketItem)
        {
            var product = _basketItemDal.Get(b => b.BasketId == basketItem.BasketId && b.ProductId == basketItem.ProductId);
            var productRepo = _productService.GetById(basketItem.ProductId).Data;
            if (productRepo != null)
            {
                if (product != null && product.ProductCount > 0)
                {
                    if (productRepo.UnitsInStock >= product.ProductCount + 1)
                    {
                        product.ProductCount++;
                        _basketItemDal.Update(product);
                        return new CountResult("Sepete Başarıyla Eklendi.", product.ProductCount, true);
                    }
                    return new CountResult("Ürün stokta bulunmamaktadır.", false);
                }

                if (product == null)
                {
                    if (productRepo.UnitsInStock >= 1)
                    {
                        var basketItemAdd = new BasketItem()
                        {
                            ProductCount = basketItem.ProductCount,
                            BasketId = basketItem.BasketId,
                            ProductId = productRepo.Id,
                            AddedDate = DateTime.Now,
                        };
                        _basketItemDal.Add(basketItemAdd);
                        basketItem.ProductCount = 1;
                        _basketItemDal.Update(basketItemAdd);
                        return new CountResult("Sepete Başarıyla Eklendi.", 1, true);
                    }
                    return new CountResult("Ürün stokta bulunmamaktadır.", false);
                }
            }
            return new CountResult("Ürün bulunamadı", false);
        }

        public CountResult Delete(int productId, int basketId)
        {
            var basketItem = _basketItemDal.Get(b => b.BasketId == basketId && b.ProductId == productId);
            if (basketItem != null && basketItem.ProductCount.HasValue && basketItem.ProductCount > 0 && basketItem.ProductCount != 1)
            {
                basketItem.ProductCount--;
                _basketItemDal.Update(basketItem);
                return new CountResult("Başarıyla silindi.", basketItem.ProductCount, true);
            }
            else if (basketItem != null && basketItem.ProductCount.HasValue && basketItem.ProductCount == 1)
            {
                _basketItemDal.Delete(basketItem.Id);
                return new CountResult(Messages.Deleted, true);
            }

            return new CountResult("Ürün Sepette Bulunamadı", false);
        }


        public IResult DeleteAllItem(int productId, int basketId)
        {
            var basketItem = _basketItemDal.Get(b => b.BasketId == basketId && b.ProductId == productId);
            if (basketItem != null && basketItem.ProductCount > 0)
            {
                _basketItemDal.Delete(basketItem.Id);
                return new SuccessResult("Ürün sepetten kaldırıldı");
            }
            else
            {
                return new ErrorResult("Sepette ürün bulunamadı");
            }
        }


        public IDataResult<List<BasketItem>> GetAllItems(int productId)
        {
            return new SuccessDataResult<List<BasketItem>>(_basketItemDal.GetAllItems(productId));
        }

        public IResult DeleteAllBasket(int basketId)
        {
            _basketItemDal.DeleteAllBasketItemsWithBasketId(basketId);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
