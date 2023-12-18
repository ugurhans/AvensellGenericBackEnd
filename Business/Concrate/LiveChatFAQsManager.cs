using System;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;

namespace Business.Concrate
{
    public class LiveChatFAQsManager : ILiveChatFAQsService
    {
        private readonly ILiveChatFAQsDal _liveChatFAQsDal;

        public LiveChatFAQsManager(ILiveChatFAQsDal liveChatFAQsDal)
        {
            _liveChatFAQsDal = liveChatFAQsDal;
        }

        public IResult Add(LiveChatFAQs liveChatFAQs)
        {
            _liveChatFAQsDal.Add(liveChatFAQs);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id)
        {
            _liveChatFAQsDal.Delete(id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<LiveChatFAQs>> GetAll()
        {
            return new SuccessDataResult<List<LiveChatFAQs>>(_liveChatFAQsDal.GetAll(x => x.IsActive == true));
        }

        public IResult SetActive(int id)
        {
            var faq = _liveChatFAQsDal.Get(x => x.Id == id);
            faq.IsActive = !faq.IsActive;
            _liveChatFAQsDal.Update(faq);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Update(LiveChatFAQs liveChatFAQs)
        {
            _liveChatFAQsDal.Update(liveChatFAQs);
            return new SuccessResult(Messages.Updated);
        }
    }
}

