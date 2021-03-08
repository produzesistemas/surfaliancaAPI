
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public string Token { get; set; }
        public ApplicationUser()
        {

        }
    }
}
