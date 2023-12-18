using System;
using Core.Entities;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;
using Entity.Enum;

namespace Business.Abstract
{
    public interface ICampaignService
    {
        public IDataResult<List<CampaignDto>> GetAllDto();
        public IResult Add(CampaignAddDto campaignAddDto);
        public IDataResult<T> Get<T>(CampaignTypes campaignType, int id) where T : class, IEntity, new();
        public IDataResult<List<CampaignDto>> GetAllForBasket(int basketId);
    }
}

