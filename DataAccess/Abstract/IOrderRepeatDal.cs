using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderRepeatDal : IEntityRepository<OrderRepeat>
    {
        public ChartDto GetMostOrderedServices();
        public ChartDto GetMostServicedCities();
        public ChartDto GetServiceCountForMonths();
    }
}
