using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clean.Database.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class RemoveDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "232fc559-2d3f-4de5-a1c8-b2779e58262e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1878954-ff6e-406a-acaa-27ba0a628358", "bfbd89d5-ef2d-4dc1-86cb-fea44d40cb59" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1878954-ff6e-406a-acaa-27ba0a628358");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bfbd89d5-ef2d-4dc1-86cb-fea44d40cb59");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "232fc559-2d3f-4de5-a1c8-b2779e58262e", null, "User", null },
                    { "a1878954-ff6e-406a-acaa-27ba0a628358", null, "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bfbd89d5-ef2d-4dc1-86cb-fea44d40cb59", 0, "af71f6de-b91f-4d2e-90d0-6ea70bb7cf26", null, true, false, null, null, "ADMIN@ADMIN.PL", "AQAAAAIAAYagAAAAEMuwGLcDcRQeSCgVsxGTdUMs1aYePPJxc/0Vn13nS4AJ6I+4flgTP1pAy32XlvBVYA==", null, false, "4a8c1d94-512c-4822-96b9-ff3294079c95", false, "admin@admin.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a1878954-ff6e-406a-acaa-27ba0a628358", "bfbd89d5-ef2d-4dc1-86cb-fea44d40cb59" });
        }
    }
}
