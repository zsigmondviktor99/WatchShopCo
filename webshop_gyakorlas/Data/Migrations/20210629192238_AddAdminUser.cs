using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_gyakorlas.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO[dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES(N'e322ca0d-1130-42d5-be93-e6acf71a16fc', N'admin', N'ADMIN', N'admin@watchshopco.com', N'ADMIN@WATCHSHOPCO.COM', 1, N'AQAAAAEAACcQAAAAEBJG/0kxy9OLI+Cm1fhttTkhsgK50MAyC33WpcEsI9alrR+OyDQDan5ffyZLEwDolg==', N'BJTPCGWNQMPKEVTGNREL3CFRK3NP2Y3T', N'4daaf49c-22b5-483f-a50b-c3f3dbab3a7f', NULL, 0, 0, NULL, 1, 0)
            
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f85c8973-4dc2-4e18-901a-c26ac8f0f444', N'Admin', N'ADMIN', N'5bd55d90-fd8b-49b2-8fab-0dcca591f2b8')
            
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e322ca0d-1130-42d5-be93-e6acf71a16fc', N'f85c8973-4dc2-4e18-901a-c26ac8f0f444')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
