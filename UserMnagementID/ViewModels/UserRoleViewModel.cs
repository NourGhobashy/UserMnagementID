﻿using UserMnagementID.Models;

namespace UserMnagementID.ViewModels
{
    public class UserRoleViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public List<RoleViewModel>? Roles { get; set; }
    }
}
