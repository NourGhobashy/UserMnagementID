//using Microsoft.AspNetCore.Identity;
//using System.ComponentModel.DataAnnotations;

//namespace UserMnagementID.Models
//{
//    public class AppUser : IdentityUser
//    {
//        internal string Id;

//        [Required, MaxLength(100)]
//        public string FirstName { get; set; }
//        [Required, MaxLength(100)]
//        public string LastName { get; set; }

//        public byte[]? ProfilePicture { get; set; }
//    }
//}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserMnagementID.Models
{
	public class AppUser : IdentityUser
	{
		[Required, MaxLength(100)]
		public string? FirstName { get; set; }

		[Required, MaxLength(100)]
		public string? LastName { get; set; }

		public byte[]? ProfilePicture { get; set; }
	}
}
