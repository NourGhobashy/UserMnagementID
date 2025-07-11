using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMnagementID.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT 1 FROM [security].[Roles] WHERE Id = '62541974-f224-4640-9c9f-9e56b2f22839')
            BEGIN
                INSERT INTO [security].[Roles] 
                ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) 
                VALUES 
                ('62541974-f224-4640-9c9f-9e56b2f22839', 'd03ca3a4-7a0c-47fb-bbc9-9b34659b211e', 'Admin', 'ADMIN')
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DELETE FROM [security].[Roles] 
            WHERE Id = '62541974-f224-4640-9c9f-9e56b2f22839'
        ");
        }
    }

}
