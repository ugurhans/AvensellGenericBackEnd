using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;

namespace Business.Concrate
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<List<Brand>> SearchBrand(string brandName)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(x => x.Name.Contains(brandName)));
        }
        
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Added);
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(int id)
        {
            _brandDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }


    }
}

