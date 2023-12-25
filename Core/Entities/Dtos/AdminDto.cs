using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class AdminDto:IDto
    {
       
            public int Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public byte[]? PasswordSalt { get; set; }
            public byte[]? PasswordHash { get; set; }
            public bool? Status { get; set; }
            public string? LostPin { get; set; }
            [NotMapped]
            public AccessToken? Token { get; set; }
            [NotMapped]
            public UserOperationClaim? Roles { get; set; }
            public int? BasketId { get; set; }

    }
    
}
