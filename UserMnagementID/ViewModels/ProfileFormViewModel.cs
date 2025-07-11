using System.ComponentModel.DataAnnotations;
using UserMnagementID.Models;

namespace UserMnagementID.ViewModels
{
    public class ProfileFormViewModel
    {
        public string ProfileID { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public IFormFile? ProfileImage { get; set; }           
        public byte[]? ExistingProfilePicture { get; set; }
    }
}
