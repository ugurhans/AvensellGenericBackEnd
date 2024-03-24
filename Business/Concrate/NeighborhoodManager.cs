using System;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Entities;

namespace Business.Concrate
{
    public class NeighborhoodManager : INeighborhoodService
    {
        private readonly INeighborhoodDal _neighborhoodDal;
        public NeighborhoodManager(INeighborhoodDal neighborhoodDal)
        {
            _neighborhoodDal = neighborhoodDal;
        }
        public IResult Add(Neighborhood neighborhood)
        {
            _neighborhoodDal.Add(neighborhood);
            return new SuccessResult();
        }

        public IDataResult<Neighborhood> Get(int neighborhoodId)
        {
            return new SuccessDataResult<Neighborhood>(_neighborhoodDal.Get(x => x.Id == neighborhoodId));
        }

        public IDataResult<List<Neighborhood>> GetAllByDistrict(int districtId)
        {
            return new SuccessDataResult<List<Neighborhood>>(_neighborhoodDal.GetAll(x => x.DistrictId == districtId));
        }

        public IResult Update(Neighborhood neighborhood)
        {
            var findDatum = Get(neighborhood.Id);
            if (findDatum.Data != null)
            {
                var datum = findDatum.Data;
                datum = neighborhood;
                _neighborhoodDal.Update(datum);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }

}

