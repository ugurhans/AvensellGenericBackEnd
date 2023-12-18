using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class UserFavoriteManager : IUserFavoriteService
    {
        private IUserFavoriteDal _userFavoriteDal;

        public UserFavoriteManager(IUserFavoriteDal userFavoriteDal)
        {
            _userFavoriteDal = userFavoriteDal;
        }

        public IDataResult<List<UserFavoriteDto>> GetSimpleDtoByUserId(int userId)
        {
            return new SuccessDataResult<List<UserFavoriteDto>>(_userFavoriteDal.GetByUserId(userId));
        }

        public IResult Add(UserFavorite favorite)
        {
            var favoriteExist = _userFavoriteDal.Get(f => f.ProductId == favorite.ProductId && f.UserId == favorite.UserId);
            if (favoriteExist != null)
            {
                return new SuccessResult("Ürün zaten favorilerde mevcut.");
            }
            else
            {
                _userFavoriteDal.Add(favorite);
                return new SuccessResult(Messages.Added);
            }
        }

        public IResult Delete(int userId, int productId)
        {
            var userFavorite = _userFavoriteDal.Get(uf => uf.ProductId == productId && uf.UserId == userId);
            _userFavoriteDal.Delete(userFavorite.Id);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<UserFavoriteDto>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserFavoriteDto>>(_userFavoriteDal.GetByUserId(userId));
        }

        public IDataResult<List<UserFavoriteDto>> GetByUserIdAllProducts(int userId)
        {
            return new SuccessDataResult<List<UserFavoriteDto>>(_userFavoriteDal.GetByUserId(userId));
        }
    }
}
