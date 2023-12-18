using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Concrete;

namespace DataAccess.Abstract
{
    public interface IReviewDal : IEntityRepository<Review>
    {
    }
}

