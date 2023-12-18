
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Collections.Generic;
using Entity.DTOs;
using Entity.Request;
using System.Collections;
using System.Linq;
using System;
using Entity.Concrate;
using Entity.Dto;
using Entity.Result;
using System.Net;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private IUserFavoriteService _userFavoriteService;
        private readonly IBasketItemDal _basketItemDal;
        private readonly IUserFavoriteDal _userFavoriteDal;
        private readonly IOrderDal _orderDal;
        private readonly IOrderItemDal _orderItemDal;
        public ProductManager(IProductDal productDal, IUserFavoriteService userFavoriteService, IBasketItemDal basketItemDal, IUserFavoriteDal userFavoriteDal, IOrderDal orderDal, IOrderItemDal orderItemDal)
        {
            _productDal = productDal;
            _userFavoriteService = userFavoriteService;
            _basketItemDal = basketItemDal;
            _userFavoriteDal = userFavoriteDal;
            _orderDal = orderDal;
            _orderItemDal = orderItemDal;
        }


        public ProductResult Add(Product product)
        {
            var list = _productDal.GetAll(c => c.CategoryId == product.CategoryId).OrderBy(cc => cc.OrderBy).ToList();
            if (list.Count > 0)
            {
                product.OrderBy = list[^1].OrderBy + 1;
            }
            else
            {
                product.OrderBy = 1;
            }
            _productDal.Add(product);
            return new ProductResult("Ürün Başarıyla Eklendi.", product.Id, true);
        }


        public IResult Delete(int id)
        {
            var productToDelete = _productDal.Get(p => p.Id == id);
            if (productToDelete != null)
            {
                var list = _productDal.GetAll(c => c.CategoryId == productToDelete.CategoryId && c.OrderBy > productToDelete.OrderBy).OrderBy(c => c.OrderBy);
                foreach (var item in list)
                {
                    item.OrderBy--;
                    _productDal.Update(item);
                }

                var basketItemsToDelete = _basketItemDal.GetAllItems(id);
                if (basketItemsToDelete != null)
                {
                    foreach (var item in basketItemsToDelete)
                    {
                        _basketItemDal.Delete(item.Id);
                    }
                }

                _productDal.Delete(id);
                return new SuccessResult("Product Deleted Successfully.");
            }
            else
            {
                return new ErrorResult("Product Not Found");
            }
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<ProductDto>> GetAllDto()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllDto());
        }

        public IDataResult<List<UserFavoriteDto>> GetAllTopFiveDto(int userId)
        {
            return new SuccessDataResult<List<UserFavoriteDto>>(_userFavoriteDal.GetAllTopFiveDto(userId));
        }

        public IDataResult<ProductDto> GetDtoById(int id)
        {
            return new SuccessDataResult<ProductDto>(_productDal.GetDtoById(id));
        }

        public IDataResult<List<ProductSimpleDto>> GetAllByCategory(int categoryId)
        {
            return new SuccessDataResult<List<ProductSimpleDto>>(_productDal.GetDtoByCategoryId(categoryId));
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));
        }

        public IDataResult<ProductDto> GetDetailWithId(int id, int? userId)
        {
            return new SuccessDataResult<ProductDto>(_productDal.GetDetailWithId(id, userId));
        }
        public IResult Update(Product product)
        {
            //  var list = _productDal.GetAll(t => t.CategoryId == product.CategoryId)
            //.OrderBy(t => t.OrderBy).ToList();
            //  for (int i = 0; i < list.Count - 1; i++)
            //  {
            //      if (list[i].OrderBy == 0 || list[i].OrderBy != i + 1)
            //      {
            //          list[i].OrderBy = i + 1;
            //          _productDal.Update(list[i]);
            //      }
            //  }
            var exProduct = _productDal.Get(x => x.Id == product.Id);
            product.ImageUrl = exProduct.ImageUrl;
            product.CreatedDate = exProduct.CreatedDate;
            product.Rating = exProduct.Rating;
            product.ModifiedDate = DateTime.Now;

            _productDal.Update(product);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<List<ProductDto>> SearchProduct(string productName)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.SearchProduct(productName));
        }

        public IDataResult<List<UserFavoriteDto>> GetAllByCategoryIdForUser(int categoryId, int userId)
        {

            var temp = _userFavoriteDal.GetByUserIdAndCategoryId(userId, categoryId);

            return new SuccessDataResult<List<UserFavoriteDto>>(temp);
        }

        public IResult ReOrder(int from, int to, int categoryId)
        {
            //    if (to == from) return new SuccessResult();
            //    to++;
            //    from++;
            //    var list = _productDal.GetAll(c =>
            //      from > to
            //        ? c.CategoryId == categoryId && c.OrderBy >= to && c.OrderBy <= from
            //        : c.CategoryId == categoryId && c.OrderBy <= to && c.OrderBy >= from).OrderBy(c => c.OrderBy).ToList();
            //    if (from > to)
            //    {
            //        for (int i = 0; i < list.Count - 1; i++)
            //        {
            //            list[i].OrderBy++;
            //            _productDal.Update(list[i]);
            //        }
            //        var draggedItem = list[^1];
            //        draggedItem.OrderBy = to;
            //        _productDal.Update(draggedItem);
            //    }
            //    else
            //    {
            //        for (int i = list.Count - 1; i > 0; i--)
            //        {
            //            list[i].OrderBy--;
            //            _productDal.Update(list[i]);
            //        }
            //        var draggedItem = list[0];
            //        draggedItem.OrderBy = to;
            //        _productDal.Update(draggedItem);
            //    }
            //    return new SuccessResult("Başarıyla Sıralandı");
            //}
            if (to == from) return new SuccessResult();
            var list = _productDal.GetAll(c =>
              from > to
                ? c.CategoryId == categoryId && c.OrderBy >= to && c.OrderBy <= from
               : c.CategoryId == categoryId && c.OrderBy <= to && c.OrderBy >= from).OrderBy(c => c.OrderBy).ToList();
            var draggedItem = list.Where(p => p.OrderBy == from).FirstOrDefault();
            if (draggedItem != null)
            {
                int increment = (to > from) ? -1 : 1;
                foreach (var item in list)
                {
                    item.OrderBy += increment;
                    _productDal.Update(item);
                }
                draggedItem.OrderBy = to;
                _productDal.Update(draggedItem);


                //var products = _productDal.GetAll(c => c.CategoryId == categoryId).OrderBy(c => c.OrderBy).ToList();
                //var draggedItem = products.Where(p => p.OrderBy == from).FirstOrDefault();

                //if (draggedItem != null)
                //{
                //    int increment = (to > from) ? 1 : -1;

                //    for (int i = from + increment; i != to; i += increment)
                //    {
                //        var currentProduct = products.Where(p => p.OrderBy == i).FirstOrDefault();
                //        currentProduct.OrderBy += -increment;
                //        _productDal.Update(currentProduct);
                //    }

                //    draggedItem.OrderBy = to;
                //    _productDal.Update(draggedItem);

                return new SuccessResult("Successfully Reordered");
            }
            else
            {
                return new ErrorResult("Item not found");
            }

        }

        public IDataResult<List<ProductSimpleDto>> GetAllCampaignProducts(int productCount)
        {
            var products = _productDal.GetAllSimpleDto(x => x.Discount > 0);
            if (productCount == 0 && products.Count > 0)
            {
                var countProduct = products.Take(productCount).ToList();
                return new SuccessDataResult<List<ProductSimpleDto>>(countProduct);
            }
            return new SuccessDataResult<List<ProductSimpleDto>>(products);
        }

        public IDataResult<List<ProductSimpleDto>> GetAllRecommendedFiveProducts(int basketId)
        {
            var basket = _basketItemDal.GetAll(x => x.BasketId == basketId);

            if (basket.Count > 0)
            {
                var orderItemCounts = new Dictionary<int, int>();

                foreach (var item in basket)
                {
                    var productId = item.ProductId;

                    var orderItems = _orderItemDal.GetAll(x => x.ProductId == productId);

                    foreach (var orderItem in orderItems)
                    {
                        if (orderItemCounts.ContainsKey((int)orderItem.ProductId))
                        {
                            orderItemCounts[(int)orderItem.ProductId]++;
                        }
                        else
                        {
                            orderItemCounts[(int)orderItem.ProductId] = 1;
                        }
                    }
                }

                var maxRecurringItems = orderItemCounts.OrderByDescending(x => x.Value).Take(5);

                var recommendedItems = new List<ProductSimpleDto>();
                foreach (var item in maxRecurringItems)
                {
                    var productId = item.Key;
                    var product = _productDal.Get(x => x.Id == productId);

                    if (product != null)
                    {
                        var recommendedProduct = new ProductSimpleDto
                        {
                            Id = product.Id,
                            CategoryId = product.CategoryId,
                            Name = product.Name,
                            UnitPrice = product.UnitPrice,
                            Discount = product.Discount,
                            OrderBy = product.OrderBy,
                            UnitType = product.UnitType,
                            UnitQuantity = product.UnitQuantity,
                            UnitCount = product.UnitCount,
                            ImageUrl = product.ImageUrl,
                            PaidPrice = product.UnitPrice - product.Discount,
                            BrandName = product.Brand?.Name
                        };

                        recommendedItems.Add(recommendedProduct);
                    }
                }
                if (recommendedItems.Count < 5)
                {
                    if (recommendedItems.Count == 0)
                    {
                        var basketProductIds = basket.Select(x => x.ProductId).Distinct().ToList();
                        var similarItems = _productDal.GetAll(x => basketProductIds.Contains(x.Id)).Take(5);

                        foreach (var similarItem in similarItems)
                        {
                            var similarProduct = new ProductSimpleDto
                            {
                                Id = similarItem.Id,
                                Name = similarItem.Name,
                                UnitPrice = similarItem.UnitPrice,
                                Discount = similarItem.Discount,
                                OrderBy = similarItem.OrderBy,
                                UnitType = similarItem.UnitType,
                                UnitQuantity = similarItem.UnitQuantity,
                                UnitCount = similarItem.UnitCount,
                                ImageUrl = similarItem.ImageUrl,
                                PaidPrice = similarItem.UnitPrice - similarItem.Discount,
                                BrandName = similarItem.Brand?.Name
                            };

                            recommendedItems.Add(similarProduct);
                        }
                    }
                    else
                    {
                        var categoryIds = recommendedItems.Select(x => x.CategoryId).Distinct().ToList();
                        var additionalItems = _productDal.GetAll(x => categoryIds.Contains(x.CategoryId) && !recommendedItems.Any(r => r.Id == x.Id)).Take(5 - recommendedItems.Count);

                        foreach (var additionalItem in additionalItems)
                        {
                            var additionalProduct = new ProductSimpleDto
                            {
                                Id = additionalItem.Id,
                                Name = additionalItem.Name,
                                UnitPrice = additionalItem.UnitPrice,
                                Discount = additionalItem.Discount,
                                OrderBy = additionalItem.OrderBy,
                                UnitType = additionalItem.UnitType,
                                UnitQuantity = additionalItem.UnitQuantity,
                                UnitCount = additionalItem.UnitCount,
                                ImageUrl = additionalItem.ImageUrl,
                                PaidPrice = additionalItem.UnitPrice - additionalItem.Discount,
                                BrandName = additionalItem.Brand?.Name
                            };

                            recommendedItems.Add(additionalProduct);
                        }
                    }
                }

                return new SuccessDataResult<List<ProductSimpleDto>>(recommendedItems);
            }
            else
            {
                return new ErrorDataResult<List<ProductSimpleDto>>("Öneri almak için sepete ürün ekleyiniz.");
            }
        }

        public IDataResult<int> GetAllCount()
        {
            return new SuccessDataResult<int>(_productDal.GetAll().Count);
        }

        string GetImageTypeFromUrl(string imageUrl)
        {
            string extension = Path.GetExtension(imageUrl)?.ToLower().Replace("_", "");
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "jpeg";
                case ".png":
                    return "png";
                case ".gif":
                    return "gif";
                case ".webp":
                    return "webp";
                case ".pdf":
                    return "pdf";
                default:
                    return "unknown";
            }
        }

        public IDataResult<string> GetImageBase64ForUpdate(int id)
        {
            var product = _productDal.Get(x => x.Id == id);
            if (product != null)
            {
                using (WebClient client = new WebClient())
                {
                    // Download the image data as bytes
                    byte[] imageBytes = client.DownloadData(product.ImageUrl);

                    // Determine the image type based on the file extension
                    string imageType = GetImageTypeFromUrl(product.ImageUrl);

                    // Convert the image bytes to base64 string with the type prefix
                    string base64String = $"data:image/{imageType};base64,{Convert.ToBase64String(imageBytes)}";

                    // Create and return the data result
                    return new DataResult<string>(base64String, true, "Image converted to base64 successfully.");
                }
            }

            // If the product is not found, return an error data result
            return new DataResult<string>(null, false, "Product not found.");
        }

    }

}
