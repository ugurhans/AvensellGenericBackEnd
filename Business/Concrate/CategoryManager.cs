using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dto;

namespace Business.Concrate
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<List<CategoryAndSubDto>> GetallCategoryAndSubCategoriesDto()
        {
            return new SuccessDataResult<List<CategoryAndSubDto>>(_categoryDal.GetallCategoryAndSubCategoriesDto());
        }

        public IDataResult<List<CategoryDtoWithSubAndProduct>> GetAllDto()
        {
            return new SuccessDataResult<List<CategoryDtoWithSubAndProduct>>(_categoryDal.GetDto());
        }

        public IDataResult<List<CategorySimpleDto>> GetAllSimpleDtoDto()
        {
            return new SuccessDataResult<List<CategorySimpleDto>>(_categoryDal.GetAllSimpleDtoDto());
        }
    }
}
