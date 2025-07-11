using System.ComponentModel.DataAnnotations;

namespace UserMnagementID.ViewModels
{
    public class RoleViewModels
    {
        [Required, StringLength(256)]
        public string? RoleName { get; set; }
    }
}
