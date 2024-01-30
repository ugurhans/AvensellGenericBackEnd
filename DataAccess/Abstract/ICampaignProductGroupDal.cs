using System;
using Core.DataAccess;
using Entity.Concrate;
using Entity.Dto;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Abstract
{
    public interface ICampaignProductGroupDal : IEntityRepository<CampaignProductGroup>
    {
        List<CampaignProductGroupDto> GetAllDto(Expression<Func<CampaignProductGroupDto, bool>> filter);

        public void DeleteRange(int ıd)
        {
            using AvenSellContext context = new AvenSellContext();
            context.CampaignProductGroups.RemoveRange(context.CampaignProductGroups.Where(a => a.Id == ıd));
            context.SaveChanges();

        }
    }
}

