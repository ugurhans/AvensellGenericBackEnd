using System;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Dto;

namespace Business.Abstract
{
    public interface ISubCategoryService
    {
        IDataResult<List<SubCategory>> GetAll();
        IDataResult<List<SubCategory>> GetAllWithCategoryId(int categoyId);
        IDataResult<List<SubCategoryDto>> GetAllDto();
        IDataResult<List<SubCategoryDto>> GetAllDtoWithCategoryId(int categoyId);
        IResult Add(SubCategory subCategory);
        IResult Update(SubCategory subCategory);
        IResult Delete(int id);
        IDataResult<List<SubCategoryDto>> SearchProductWithSubCategory(string productName);
    }

}
