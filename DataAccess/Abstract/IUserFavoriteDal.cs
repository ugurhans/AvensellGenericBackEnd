using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserFavoriteDal : IEntityRepository<UserFavorite>
    {
        List<UserFavoriteDto> GetByUserId(int userId);
        List<UserFavoriteDto> GetByUserIdAndCategoryId(int userId, int categoryId);
        List<UserFavoriteDto> GetAllTopFiveDto(int userId);
        void DeleteRange(int userId);
    }
}
