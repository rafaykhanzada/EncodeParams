using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace EncodeParams.Models
{
    public partial class Role : IdentityRole
    {
        public bool IsActive { get; set; }
    }
}
