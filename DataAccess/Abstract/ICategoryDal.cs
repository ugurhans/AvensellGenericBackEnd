using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        public List<CategoryAndSubDto> GetallCategoryAndSubCategoriesDto();
        public List<CategorySimpleDto> GetAllSimpleDtoDto();
        public List<CategoryDtoWithSubAndProduct> GetDto();
    }
}

