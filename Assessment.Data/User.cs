using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Assessment.Data
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required"), EmailAddress, StringLength(250, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = base.NormalizedEmail = value; }
        }
    }
}
