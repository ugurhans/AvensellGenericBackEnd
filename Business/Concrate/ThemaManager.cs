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
    public class ThemaManager:IThemaService
    {

        private readonly IThemaDal _themaDal;

            public ThemaManager(IThemaDal themaDal)
            {
            _themaDal = themaDal;
            }
            public IResult Add(Thema shopAndThema)
            {
            _themaDal.Add(shopAndThema);
                return new SuccessResult(Messages.Added);
            }

            public IResult Delete(int id)
            {
            _themaDal.Delete(id);
                return new SuccessResult(Messages.Deleted);
            }

            public IDataResult<Thema> GetById(int id)
            {
                return new SuccessDataResult<Thema>(_themaDal.Get(b => b.Id == id));
            }

            public IDataResult<List<Thema>> GetAll()
            {
                return new SuccessDataResult<List<Thema>>(_themaDal.GetAll());
            }

            public IResult Update(Thema shopAndThema)
            {
            _themaDal.Update(shopAndThema);
                return new SuccessResult(Messages.Updated);
            }
        }
    
}
