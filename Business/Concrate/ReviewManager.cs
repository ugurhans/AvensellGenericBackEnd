using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;

namespace Business.Concrate
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public IResult Add(Review review)
        {
            _reviewDal.Add(review);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id)
        {
            _reviewDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Review>> GetAll()
        {
            return new SuccessDataResult<List<Review>>(_reviewDal.GetAll());
        }

        public IResult Update(Review review)
        {
            _reviewDal.Update(review);
            return new SuccessResult(Messages.Updated);
        }
    }
}

