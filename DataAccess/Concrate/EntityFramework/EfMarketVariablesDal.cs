using System;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrate;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfMarketVariablesDal : EfEntityRepositoryBase<MarketVariable, AvenSellContext>, IMarketVariablesDal
    {

    }
}

