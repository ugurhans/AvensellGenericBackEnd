using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
	public static class ClaimExtensions
    {
        //ICollection tipinde bir Claim extend edicem.                  parametre
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        { //Id
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
        //gönderilen rolleri listeye cevir, her bir rolü dolas ve claime ekle
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        { 
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }  
}
