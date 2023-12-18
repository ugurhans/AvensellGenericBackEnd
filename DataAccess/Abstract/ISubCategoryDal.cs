using Core.DataAccess;
using Entity.Concrate;
using Entity.Concrete;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISubCategoryDal : IEntityRepository<SubCategory>
    {
        List<SubCategoryDto> GetAllDto();
        List<SubCategoryDto> GetAllDtoWithCategoryId(int categoyId);
        List<SubCategoryDto> SearchProductWithSubCategory(string searchString);
    }
}
