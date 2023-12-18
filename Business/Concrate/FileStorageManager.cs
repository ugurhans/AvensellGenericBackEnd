using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Concrete;
using Entity.DTOs;
using Entity.Enum;
using Entity.Request;
using Entity.Result;
using Microsoft.VisualBasic.FileIO;

namespace Business.Concrete
{
    public class FileStorageManager : IFileStorageService
    {
        private IFileStorageDal _fileStorageDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;

        public FileStorageManager(IFileStorageDal fileStorageDal, ICategoryDal categoryDal, IProductDal productDal)
        {
            _fileStorageDal = fileStorageDal;
            _categoryDal = categoryDal;
            _productDal = productDal;
        }


        private Result UpdateTypeEntity(FileUploadRequest fileUploadRequest, string fileName, string fileType)
        {
            var uri = $@"https://www.kadimgross.com.tr/Api/Assets/{fileUploadRequest.Collection}/{fileUploadRequest.OwnerId}/{fileName}.{fileType}_";

            if (fileUploadRequest.OwnerId == (int)FileOwnerTypes.Categories)
            {
                var category = _categoryDal.Get(x => x.Id == fileUploadRequest.OwnerId);
                category.ImageUrl = uri;
                _categoryDal.Update(category);

            }
            else if (fileUploadRequest.OwnerId == (int)FileOwnerTypes.Products)
            {
                var product = _productDal.Get(x => x.Id == fileUploadRequest.OwnerId);
                product.ImageUrl = uri;
                _productDal.Update(product);
            }
            else if (fileUploadRequest.OwnerId == (int)FileOwnerTypes.Reviews)
            {
                //  reviewlar henüz yok amaburasıarray kaydecek fileUploadRequest.OwnerId, FileOwnerTypes.Reviews'a eşitse, burada ilgili işlem yapılır
            }
            else
            {
                return new ErrorResult("Geçersiz file owner type.");
            }
            return new SuccessResult();
        }

        public IDataResult<ImageResult> AddUri(FileUploadRequest fileUploadRequest)
        {
            var indexofData = fileUploadRequest.Data.IndexOf(";base64,", StringComparison.OrdinalIgnoreCase) + 8;

            var dataS = fileUploadRequest.Data.Substring(indexofData);
            var subPath = $@"C:\inetpub\wwwroot\sites\KadimSite\Api\Assets\{fileUploadRequest.Collection}\{fileUploadRequest.OwnerId}\";
            bool exists = System.IO.Directory.Exists(subPath);
            var fileType = fileUploadRequest.FileName.Substring(fileUploadRequest.FileName.LastIndexOf(".") + 1);
            var fileName = GuidHelper.CreateGuid();
            if (exists == true)
            {
                return new ErrorDataResult<ImageResult>(message: "Bu " + fileUploadRequest.Collection + " ' a zaten bir fotoğraf yüklenmiş.");
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(subPath);
                File.WriteAllBytes($@"{subPath}{fileName}.{fileType}_", Convert.FromBase64String(dataS));
            }
            FileStorage fileStorage = new FileStorage()
            {
                AddedDate = DateTime.Now,
                Collection = fileUploadRequest.Collection,
                DataType = fileUploadRequest.Data.Substring(0, indexofData),
                FileName = fileUploadRequest.FileName,
                FileSize = new System.IO.FileInfo(subPath + fileName + '.' + fileType + '_').Length,
                OwnerId = fileUploadRequest.OwnerId,
                RealFileName = fileName,
                State = 0,
                FileType = fileType,
                AddedUserId = fileUploadRequest.UserId
            };
            _fileStorageDal.Add(fileStorage);
            UpdateTypeEntity(fileUploadRequest, fileName, fileType);
            return new SuccessDataResult<ImageResult>(message: Messages.Added, data: new ImageResult(url: $@"https://www.kadimgross.com.tr/Api/Assets/{fileStorage.Collection}/{fileStorage.OwnerId}/{fileStorage.RealFileName}.{fileStorage.FileType}_"));
        }

        public IResult UpdateUri(FileUploadRequest fileUploadRequest)
        {
            var indexofData = fileUploadRequest.Data.IndexOf(";base64,", StringComparison.OrdinalIgnoreCase) + 8;

            var dataS = fileUploadRequest.Data.Substring(indexofData);
            var subPath = $@"C:\inetpub\wwwroot\sites\KadimSite\Api\Assets\{fileUploadRequest.Collection}\{fileUploadRequest.OwnerId}\";
            var fileType = fileUploadRequest.FileName.Substring(fileUploadRequest.FileName.LastIndexOf(".") + 1);
            var fileName = GuidHelper.CreateGuid();

            DirectoryInfo di = Directory.CreateDirectory(subPath);
            File.WriteAllBytes($@"{subPath}{fileName}.{fileType}_", Convert.FromBase64String(dataS));

            var existingFile = _fileStorageDal.Get(x => x.OwnerId == fileUploadRequest.OwnerId && x.Collection == fileUploadRequest.Collection);
            if (existingFile == null)
            {
                // If no existing file found, create a new one
                FileStorage fileStorage = new FileStorage()
                {
                    AddedDate = DateTime.Now,
                    Collection = fileUploadRequest.Collection,
                    DataType = fileUploadRequest.Data.Substring(0, indexofData),
                    FileName = fileUploadRequest.FileName,
                    FileSize = new System.IO.FileInfo(subPath + fileName + '.' + fileType + '_').Length,
                    OwnerId = fileUploadRequest.OwnerId,
                    RealFileName = fileName,
                    State = 0,
                    FileType = fileType,
                    AddedUserId = fileUploadRequest.UserId
                };
                _fileStorageDal.Add(fileStorage);
            }
            else
            {
                // If existing file found, update its properties and save changes
                existingFile.DataType = fileUploadRequest.Data.Substring(0, indexofData);
                existingFile.FileName = fileUploadRequest.FileName;
                existingFile.FileSize = new System.IO.FileInfo(subPath + fileName + '.' + fileType + '_').Length;
                existingFile.RealFileName = fileName;
                existingFile.FileType = fileType;
                existingFile.State = 0;
                existingFile.ModifiedDate = DateTime.Now;
                existingFile.ModifiedUserId = fileUploadRequest.UserId;
                _fileStorageDal.Update(existingFile);
                var product = _productDal.Get(x => x.Id == fileUploadRequest.OwnerId);
                product.ImageUrl = $@"https://www.kadimgross.com.tr/Api/Assets/{fileUploadRequest.Collection}/{fileUploadRequest.OwnerId}/{fileName}.{fileType}_";
                _productDal.Update(product);
            }
            return new SuccessResult(Messages.Updated);
        }


        public IResult DeleteUri(FileUploadRequest fileUploadRequest)
        {
            // burada silme işlemi realfile name üzerinden yapılacak ve ilgili nesnelerin url leri db de boşalatılacak
            return new SuccessResult();
        }

        public IDataResult<FileResponseDto> Get(int id, string collection)
        {
            var file = _fileStorageDal.Get(f => f.OwnerId == id && f.Collection == collection);
            if (file != null)
            {
                var uri = $@"https://www.kadimgross.com.tr/Api/Assets/{file.Collection}/{file.OwnerId}/{file.RealFileName}.{file.FileType}_";
                var result = new FileResponseDto()
                {
                    Uri = uri
                };
                return new SuccessDataResult<FileResponseDto>(result);
            }
            return new ErrorDataResult<FileResponseDto>("Dosya Bulunamadı");
        }
    }
}
