using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrate.EntityFramework;
using Entity.Concrate;

namespace Business.Concrate
{
    public class ShopStuffManager : IShopStuffService
    {
        private readonly IUserPermissionDal _userPermissionDal;
        private readonly IFAQsDal _fAQsDal;
        private readonly IMarketVariablesDal _marketVariablesDal;

        public ShopStuffManager(IUserPermissionDal userPermissionDal, IFAQsDal fAQsDal, IMarketVariablesDal marketVariablesDal)
        {
            _userPermissionDal = userPermissionDal;
            _fAQsDal = fAQsDal;
            _marketVariablesDal = marketVariablesDal;
        }

        public IResult AddFAQs(FAQs fAQs)
        {
            _fAQsDal.Add(fAQs);
            return new SuccessResult(Messages.Added);
        }

        public IResult DeleteFaqs(int id)
        {
            _fAQsDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IResult UpdateFaqs(FAQs fAQs)
        {
            _fAQsDal.Update(fAQs);
            return new SuccessResult(Messages.Updated);
        }
        public IDataResult<List<FAQs>> GetAllFAQs()
        {
            return new SuccessDataResult<List<FAQs>>(_fAQsDal.GetAll());
        }

        public IDataResult<FAQs> GetFAQsWithId(int id)
        {
            return new SuccessDataResult<FAQs>(_fAQsDal.Get(x => x.Id == id));
        }


        public IDataResult<UserPermission> GetAllUserPermissionsWithUserId(int userId)
        {
            return new SuccessDataResult<UserPermission>(_userPermissionDal.Get(x => x.UserId == userId));
        }


        public IResult UpdateUserPermission(UserPermission userPermission)
        {
            _userPermissionDal.Update(userPermission);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<MarketVariable> GetMarketVariables()
        {
            return new SuccessDataResult<MarketVariable>(_marketVariablesDal.GetAll()?.FirstOrDefault()!);
        }

        public IResult UpdateMarketVariables(MarketVariable marketVariables)
        {
            _marketVariablesDal.Update(marketVariables);
            return new SuccessResult(Messages.Updated);
        }

        public IResult AddMarketVariables(MarketVariable marketVariables)
        {
            _marketVariablesDal.Add(marketVariables);
            return new SuccessResult(Messages.Added);
        }
    }
}

