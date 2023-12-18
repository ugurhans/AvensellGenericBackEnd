using System;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<CategoryAndSubDto>> GetallCategoryAndSubCategoriesDto();
        IDataResult<List<CategoryDtoWithSubAndProduct>> GetAllDto();
        IDataResult<List<CategorySimpleDto>> GetAllSimpleDtoDto();
        IResult Add(Category category);



        //IDataResult<Category> GetById(int categoryId);
        //IResult Update(Category category);
        //IResult Delete(int id);
        //IDataResult<List<Category>> GetAll();
        //IDataResult<CategoryDto> GetDtoById(int id);
        //IResult ReOrder(int from, int to);
    }
}

