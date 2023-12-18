using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
	public class SecurityKeyHelper
	{
		public static SecurityKey CreateSecurityKey(string securityKey)
		{   
			//appsetting jasonda yazdıgın security keyi bytea çeviriyor.
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		}
	}
}
