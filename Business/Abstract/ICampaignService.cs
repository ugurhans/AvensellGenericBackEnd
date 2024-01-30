using System;
using System.ComponentModel;
using Core.Entities;
using Core.Utilities.Results;
using Entity.Abtract;
using Entity.Concrate;
using Entity.Dto;
using Entity.Enum;

namespace Business.Abstract
{
    public interface ICampaignService
    {
       // public IDataResult<List<CampaignDto>> GetAllDto();
        IDataResult<List<ICampaign>> GetAll();//
        public IResult Add(CampaignAddDto campaignAddDto);
        public IDataResult<T> Get<T>(CampaignTypes campaignType, int id) where T : class, IEntity, new();
        public IDataResult<List<CampaignDto>> GetAllForBasket(int basketId);
        public IDataResult<CampaignDto> GetCampaignID(int id);
        //public IResult Add(CampaignDto campaignAddDto);
        public IResult Update(CampaignUpdateDto campaignUpdateDto);
        public IResult Delete(int campaignId, CampaignTypes campaignType);
        //public IDataResult<List<CampaignDto>> GetAll();
        
    }
}

