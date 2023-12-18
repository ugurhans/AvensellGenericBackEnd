using System;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        //IDataResult<List<Brand>> Get();

        IResult Update(Brand brand);
        IResult Add(Brand brand);
        IResult Delete(int id);
        IDataResult<List<Brand>> SearchBrand(string brandName);
    }
}

