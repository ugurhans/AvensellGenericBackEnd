using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;

namespace Entity.DTOs
{
    public class UserDto : IDto
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
        public UserOperationClaim? Roles { get; set; }
        [NotMapped]
        public AccessToken? Token { get; set; }

        public int? BasketId { get; set; }
    }
}
