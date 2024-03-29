﻿using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entity.Dto;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAdminDal : IEntityRepository<Admin>
    {
        AdminDto GetDto(string email);
        List<OperationClaim> GetClaims(AdminDto adminDto);
        List<AdminProfileDtoIDto> GetAllAdmin();

    }
}
