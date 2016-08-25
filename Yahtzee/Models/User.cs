using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Xml;

namespace Yahtzee.Models
{
    public class User: IdentityUser

    {
       
        
        public string ScreenName { get; set; }
        [Url]
        public string Avatar { get; set; }
        [Required]
        public bool PublicAccount { get; set; }
        [NotMapped]
        public string Password { get; set; }

        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Game> Invitations { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public User()
        {
            Friends = new HashSet<User>();
        }
    }
}