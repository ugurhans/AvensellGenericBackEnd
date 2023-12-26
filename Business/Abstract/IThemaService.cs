using Core.Utilities.Results;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IThemaService
    {
        IDataResult<List<Thema>> GetAll();
        IDataResult<Thema> GetById(int id);
        IResult Update(Thema thema);
        IResult Add(Thema thema);
        IResult Delete(int id);
    }
}
