using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFileStorageDal : EfEntityRepositoryBase<FileStorage, AvenSellContext>, IFileStorageDal
    {
    }
}
