using System;
using Core.Utilities.Results;
using Entity.Request;

namespace Business.Abstract
{
    public interface IMailService
    {
        Task<IResult> SendLostEmailAsync(MailRequest mailRequest);
        Task<IResult> SendResetEmailAsync(MailRequest mailRequest);
    }
}

