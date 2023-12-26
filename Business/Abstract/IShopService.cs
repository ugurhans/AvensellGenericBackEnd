using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShopService
    {
        IDataResult<List<Shop>> GetAll();
        IDataResult<Shop> GetById(int id);
        IResult Update(Shop Shop);
        IResult Add(Shop shop);
        IResult Delete(int id);

    }
}
