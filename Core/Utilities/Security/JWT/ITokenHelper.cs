using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.DTOs;

namespace Core.Utilities.Security.JWT
{
	public interface ITokenHelper
	{
		//Token üretimi--> kullanıcı+rolleri tokena eklenir.
		AccessToken CreateToken(UserDto user,List<OperationClaim> operationClaims);
       // AccessToken CreateToken(User user, List<OperationClaim> operationClaims);//
    }
}
