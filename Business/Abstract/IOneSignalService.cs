using System;
using Core.Utilities.Results;
using Entity.Dtos;

namespace Business.Abstract
{
    public interface IOneSignalService
    {
        Task<IResult> SendPushAppAsync(OneSignalPushDto oneSignalPushDto);

    }
}