using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Admin:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool? Status { get; set; }
        public string? LostPin { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastWrongLogin { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Gender? Gender { get; set; }
        public string? DateofBirth { get; set; }
        public string? SocialMediaProfiles { get; set; }
    }
}
