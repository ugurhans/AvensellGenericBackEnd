﻿using Core.DataAccess;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IThemaDal : IEntityRepository<Thema>
    {
    }
}
