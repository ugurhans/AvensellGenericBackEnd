using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDto> GetAllDto();
        List<ProductSimpleDto> GetAllSimpleDto(Expression<Func<ProductSimpleDto, bool>> filter = null);
        ProductDto GetDetailWithId(int id, int? userId);
        ProductDto GetDtoById(int id);
        List<ProductDto> SearchProduct(string productName);
        List<ProductDto> GetAllTopFiveDto();
        List<ProductSimpleDto> GetDtoByCategoryId(int categoryId);
    }
}

