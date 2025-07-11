using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMnagementID.Data.Migrations
{
    public partial class SeedAdminUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        -- 1) إضافة مستخدم Admin إذا لم يكن موجودًا
        IF NOT EXISTS (SELECT 1 FROM [security].[Users] WHERE UserName = 'admin@admin.com')
        BEGIN
            INSERT INTO [security].[Users]
            ([Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],
             [PasswordHash],[SecurityStamp],[ConcurrencyStamp],
             [AccessFailedCount],[LockoutEnabled],[PhoneNumberConfirmed],[TwoFactorEnabled],
             [FirstName],[LastName])
            VALUES
            ('2e1b7b0b-6c3e-41a6-bf35-08db8b234eee','admin@admin.com','ADMIN@ADMIN.COM',
             'admin@admin.com','ADMIN@ADMIN.COM',1,
             'AQAAAAIAAYagAAAAELhrFd35LUQUa0z3RdozXynLKzgoL5w7lbFz+LKjaZXGdBaNILmJh0YFCEYpz8kt8w==',
             NEWID(),NEWID(),
             0,0,0,0,
             'Admin','User')
        END;

        -- 2) إضافة الدور Admin إذا لم يكن موجودًا
        IF NOT EXISTS (SELECT 1 FROM [security].[Roles] WHERE Id = '62541974-f224-4640-9c9f-9e56b2f22839')
        BEGIN
            INSERT INTO [security].[Roles]
            ([Id],[ConcurrencyStamp],[Name],[NormalizedName])
            VALUES
            ('62541974-f224-4640-9c9f-9e56b2f22839',NEWID(),'Admin','ADMIN')
        END;

        -- 3) ربط المستخدم بالدور
        IF NOT EXISTS (
            SELECT 1 FROM [security].[UserRoles]
            WHERE UserId = '2e1b7b0b-6c3e-41a6-bf35-08db8b234eee'
              AND RoleId = '62541974-f224-4640-9c9f-9e56b2f22839')
        BEGIN
            INSERT INTO [security].[UserRoles](UserId, RoleId)
            VALUES ('2e1b7b0b-6c3e-41a6-bf35-08db8b234eee', '62541974-f224-4640-9c9f-9e56b2f22839')
        END;
    ");
        }

    

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [security].[UserRoles]
                WHERE UserId = '2e1b7b0b-6c3e-41a6-bf35-08db8b234eee'
                  AND RoleId = '62541974-f224-4640-9c9f-9e56b2f22839';

                DELETE FROM [security].[Users]
                WHERE Id = '2e1b7b0b-6c3e-41a6-bf35-08db8b234eee';

                DELETE FROM [security].[Roles]
                WHERE Id = '62541974-f224-4640-9c9f-9e56b2f22839';
            ");
        }
    }
}
