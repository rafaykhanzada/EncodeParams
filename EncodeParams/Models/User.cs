using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace EncodeParams.Models
{
    public class User : IdentityUser
    {
        public string? UserType { get; set; }
        public string? Authentication_Type { get; set; }
        public string? Profile_pic { get; set; }
        public string? Otp { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
