using System;
using Core.Utilities.Results;
using Entity.Concrate;

namespace Business.Abstract
{
    public interface IReviewService
    {
        IDataResult<List<Review>> GetAll();
        //IDataResult<List<Review>> Get();

        IResult Update(Review review);
        IResult Add(Review review);
        IResult Delete(int id);

    }
}

