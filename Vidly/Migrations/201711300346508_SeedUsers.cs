namespace Vidly.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration {
        public override void Up() {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'280558de-9140-4c6c-91cb-d7cd4f8440d6', N'guest@vidly.com', 0, N'ANZhXvwFJiRLgE5mGDXXvVGxcJRy8f7jExHSvGj04rjSjWPHpQJ2ZCyEDt6fazaYoQ==', N'a5ac3212-537c-470b-9d38-85dc6e2c3d0b', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd4b9c8fe-57af-4e2f-b094-2b8a543a4cfa', N'admin@vidly.com', 0, N'ABOWE5vbA1A0n7RSOehFC91Gu7bwFh2pHTf+rO48aLxy7GxVWcelvQvXsMQpOEqdNw==', N'5c19ef41-9e9c-44a3-82c8-fb7b908179ed', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e5b71b86-52cc-4b6c-ac00-5a0bfcb26604', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd4b9c8fe-57af-4e2f-b094-2b8a543a4cfa', N'e5b71b86-52cc-4b6c-ac00-5a0bfcb26604')
            ");
        }

        public override void Down() {
        }
    }
}
