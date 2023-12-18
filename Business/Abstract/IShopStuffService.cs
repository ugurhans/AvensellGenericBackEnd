using System;
using Core.Utilities.Results;
using Entity.Concrate;

namespace Business.Abstract
{
    public interface IShopStuffService
    {
        IDataResult<List<FAQs>> GetAllFAQs();
        IDataResult<FAQs> GetFAQsWithId(int id);
        IResult AddFAQs(FAQs fAQs);
        IResult UpdateFaqs(FAQs fAQs);
        IResult DeleteFaqs(int id);


        IDataResult<UserPermission> GetAllUserPermissionsWithUserId(int userId);
        IResult UpdateUserPermission(UserPermission userPermission);

        IDataResult<MarketVariable> GetMarketVariables();
        IResult UpdateMarketVariables(MarketVariable marketVariables);
        IResult AddMarketVariables(MarketVariable marketVariables);

    }
}

