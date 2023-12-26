using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Entity.Dto;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdminService
    {
        AdminDto GetByMailDtoAdmin(string email);
        List<OperationClaim> GetClaims(AdminDto admin);
        IDataResult<List<AdminProfileDtoIDto>>? GetAllAdmins();
        void Add(Admin admin);
 

     
    }
}
