using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.DTOs;
using Entity.Request;
using Entity.Concrate;
using Entity.Dto;
using Entity.Result;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<int> GetAllCount();
        IDataResult<string> GetImageBase64ForUpdate(int id);
        IDataResult<List<ProductDto>> GetAllDto();
        IDataResult<List<UserFavoriteDto>> GetAllTopFiveDto(int userId);
        IDataResult<ProductDto> GetDetailWithId(int id, int? userId);
        IDataResult<ProductDto> GetDtoById(int id);
        IDataResult<List<ProductSimpleDto>> GetAllByCategory(int categoryId);
        IDataResult<Product> GetById(int id);
        ProductResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(int id);
        IDataResult<List<ProductDto>> SearchProduct(string productName);
        IDataResult<List<UserFavoriteDto>> GetAllByCategoryIdForUser(int categoryId, int userId);
        IResult ReOrder(int from, int to, int productId);
        IDataResult<List<ProductSimpleDto>> GetAllCampaignProducts(int productCount);
        IDataResult<List<ProductSimpleDto>> GetAllRecommendedFiveProducts(int basketId);
        IDataResult<List<ProductSimpleDto>> GetRandomRecommendations();
    }
}
