﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
	public class SigningCredentialsHelper
	{ 
		//kulllanılacak anahtar ve kullanılacak sifreleme algoritmasını belirtir 
		public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
		{
			return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);	
		}
	}
}