using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;

namespace Business.Abstract
{
    public interface ICategoryService
    {
       public IDataResult<List<CategoryAndSubDto>> GetallCategoryAndSubCategoriesDto();
       public IDataResult<List<CategoryDtoWithSubAndProduct>> GetAllDto();
       public IDataResult<List<CategorySimpleDto>> GetAllSimpleDtoDto();
       public IResult Add(Category category);



        //IDataResult<Category> GetById(int categoryId);
        //IResult Update(Category category);
        //IResult Delete(int id);
        //IDataResult<List<Category>> GetAll();
        //IDataResult<CategoryDto> GetDtoById(int id);
        //IResult ReOrder(int from, int to);
    }
}

