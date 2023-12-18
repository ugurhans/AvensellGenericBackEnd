using System;
using Core.Utilities.Results;
using Entity.Dto;

namespace Business.Abstract
{
    public interface IDashboardSummaryService
    {
        IDataResult<DashBoardSummaryDto> GetSummary();
    }
}

