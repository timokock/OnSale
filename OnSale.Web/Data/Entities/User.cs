using Microsoft.AspNetCore.Identity;
using OnSale.Common.Entities;
using OnSale.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20, ErrorMessage = "The field {0} must contain less than {1} characters.")]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characters.")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characters.")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} must contain less than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44324/images/noimage.png"
            : $"https://tkock.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public City City { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}
