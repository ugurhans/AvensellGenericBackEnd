using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserFavoriteService
    {
        IDataResult<List<UserFavoriteDto>> GetSimpleDtoByUserId(int userId);
        IResult Add(UserFavorite favorite);
        IResult Delete(int userId, int productId);
        IDataResult<List<UserFavoriteDto>> GetByUserId(int userId);
        IDataResult<List<UserFavoriteDto>> GetByUserIdAllProducts(int userId);
    }
}
