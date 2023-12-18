using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDal _subCategoryDal;

        public SubCategoryManager(ISubCategoryDal subCategoryDal)
        {
            _subCategoryDal = subCategoryDal;
        }

        public IResult Add(SubCategory subCategory)
        {
            _subCategoryDal.Add(subCategory);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id)
        {
            _subCategoryDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<SubCategory>> GetAll()
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetAll());
        }

        public IDataResult<List<SubCategoryDto>> GetAllDto()
        {
            return new SuccessDataResult<List<SubCategoryDto>>(_subCategoryDal.GetAllDto());
        }

        public IDataResult<List<SubCategoryDto>> GetAllDtoWithCategoryId(int categoyId)
        {
            return new SuccessDataResult<List<SubCategoryDto>>(_subCategoryDal.GetAllDtoWithCategoryId(categoyId));
        }

        public IDataResult<List<SubCategory>> GetAllWithCategoryId(int categoyId)
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetAll(x => x.CategoryId == categoyId));
        }

        public IDataResult<List<SubCategoryDto>> SearchProductWithSubCategory(string searchString)
        {
            return new SuccessDataResult<List<SubCategoryDto>>(_subCategoryDal.SearchProductWithSubCategory(searchString));
        }

        public IResult Update(SubCategory subCategory)
        {
            _subCategoryDal.Update(subCategory);
            return new SuccessResult(Messages.Updated);
        }
    }
}
