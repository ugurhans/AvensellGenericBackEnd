using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entity.DTOs;
using Entity.Request;
using Entity.Result;

namespace Business.Abstract
{
    public interface IFileStorageService
    {
        IDataResult<ImageResult> AddUri(FileUploadRequest fileUploadRequest);
        IResult UpdateUri(FileUploadRequest fileUploadRequest);
        IResult DeleteUri(FileUploadRequest fileUploadRequest);

        IDataResult<FileResponseDto> Get(int id, string collection);
    }
}
