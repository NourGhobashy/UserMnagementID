//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
//using UserMnagementID.Models;

//namespace UserMnagementID.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<AppUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.HasDefaultSchema("security");

//            builder.Entity<AppUser>().ToTable("Users");
//            builder.Entity<IdentityRole>().ToTable("Roles");
//            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
//            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
//            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
//            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
//            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
//        }

//        //protected override void OnModelCreating(ModelBuilder builder)
//        //{
//        //    base.OnModelCreating(builder);

//        //    //builder.HasDefaultSchema(""); لتغعيير الاسكيما للداتا بيس كله 

//        //    builder.Entity<AppUser>().ToTable("Users", "security");
//        //    builder.Entity<AppUser>().ToTable("Roles", "security");
//        //    builder.Entity<IdentityUser<string>>().ToTable("UserRoles", "security");
//        //    builder.Entity<IdentityUser<string>>().ToTable("UserClaims", "security");
//        //    builder.Entity<IdentityUser<string>>().ToTable("UserLodins", "security");
//        //    builder.Entity<IdentityUser<string>>().ToTable("RoleClaims", "security");
//        //    builder.Entity<IdentityUser<string>>().ToTable("UserTokens", "security");

//        //        //.Ignore(e => e.PhoneNumberConfirmed); لو عايز تحذف
//        //}
//    }
//}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserMnagementID.Models;

namespace UserMnagementID.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("security");

            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}

