using System;
using Core.Utilities.Results;
using Entity.Entities;

namespace Business.Abstract
{
    public interface INeighborhoodService
    {
        IResult Add(Neighborhood neighborhood);
        IResult Update(Neighborhood neighborhood);
        IDataResult<Neighborhood> Get(int neighborhoodId);
        IDataResult<List<Neighborhood>> GetAllByDistrict(int districtId);
    }
}

