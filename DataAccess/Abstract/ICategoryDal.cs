using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        List<CategoryAndSubDto> GetallCategoryAndSubCategoriesDto();
        List<CategorySimpleDto> GetAllSimpleDtoDto();
        List<CategoryDtoWithSubAndProduct> GetDto();
    }
}

