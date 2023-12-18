using System;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ILiveChatFAQsService
    {
        IDataResult<List<LiveChatFAQs>> GetAll();

        IResult Update(LiveChatFAQs liveChatFAQs);
        IResult Add(LiveChatFAQs liveChatFAQs);
        IResult Delete(int id);
        IResult SetActive(int id);
    }
}

