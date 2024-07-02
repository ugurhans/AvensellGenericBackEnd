using Core.Utilities.Results;
using Entity.Concrate;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        IResult Update(Brand brand);
        IResult Add(Brand brand);
        IResult Delete(int id);
        IDataResult<List<Brand>> SearchBrand(string brandName);
    }
}

