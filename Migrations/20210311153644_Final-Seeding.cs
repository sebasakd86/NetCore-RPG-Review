using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_RPG.Migrations
{
    public partial class FinalSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 1, new byte[] { 35, 30, 80, 90, 17, 168, 1, 158, 172, 196, 238, 92, 149, 12, 3, 185, 123, 187, 111, 125, 17, 186, 215, 4, 65, 92, 119, 1, 137, 0, 57, 163, 140, 101, 149, 194, 88, 15, 28, 254, 24, 211, 115, 92, 112, 140, 249, 255, 180, 138, 104, 29, 245, 120, 174, 182, 243, 8, 52, 81, 21, 8, 77, 35 }, new byte[] { 130, 231, 205, 173, 69, 116, 182, 182, 155, 105, 11, 203, 222, 250, 246, 71, 55, 124, 105, 107, 243, 165, 95, 133, 153, 165, 138, 40, 121, 59, 59, 201, 245, 140, 154, 233, 193, 175, 90, 10, 53, 18, 245, 94, 231, 241, 41, 141, 244, 39, 203, 195, 153, 134, 131, 192, 69, 178, 174, 137, 117, 241, 1, 9, 97, 222, 169, 12, 228, 192, 194, 252, 140, 225, 162, 84, 200, 3, 243, 130, 30, 190, 8, 203, 30, 242, 118, 42, 158, 57, 115, 23, 207, 117, 85, 181, 179, 5, 106, 249, 172, 99, 183, 170, 193, 102, 200, 130, 78, 109, 113, 105, 156, 44, 32, 114, 97, 19, 5, 203, 8, 178, 215, 89, 120, 33, 158, 46 }, "sebasakd86" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 2, new byte[] { 35, 30, 80, 90, 17, 168, 1, 158, 172, 196, 238, 92, 149, 12, 3, 185, 123, 187, 111, 125, 17, 186, 215, 4, 65, 92, 119, 1, 137, 0, 57, 163, 140, 101, 149, 194, 88, 15, 28, 254, 24, 211, 115, 92, 112, 140, 249, 255, 180, 138, 104, 29, 245, 120, 174, 182, 243, 8, 52, 81, 21, 8, 77, 35 }, new byte[] { 130, 231, 205, 173, 69, 116, 182, 182, 155, 105, 11, 203, 222, 250, 246, 71, 55, 124, 105, 107, 243, 165, 95, 133, 153, 165, 138, 40, 121, 59, 59, 201, 245, 140, 154, 233, 193, 175, 90, 10, 53, 18, 245, 94, 231, 241, 41, 141, 244, 39, 203, 195, 153, 134, 131, 192, 69, 178, 174, 137, 117, 241, 1, 9, 97, 222, 169, 12, 228, 192, 194, 252, 140, 225, 162, 84, 200, 3, 243, 130, 30, 190, 8, 203, 30, 242, 118, 42, 158, 57, 115, 23, 207, 117, 85, 181, 179, 5, 106, 249, 172, 99, 183, 170, 193, 102, 200, 130, 78, 109, 113, 105, 156, 44, 32, 114, 97, 19, 5, 203, 8, 178, 215, 89, 120, 33, 158, 46 }, "pablo" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Role", "UserName" },
                values: new object[] { 3, new byte[] { 35, 30, 80, 90, 17, 168, 1, 158, 172, 196, 238, 92, 149, 12, 3, 185, 123, 187, 111, 125, 17, 186, 215, 4, 65, 92, 119, 1, 137, 0, 57, 163, 140, 101, 149, 194, 88, 15, 28, 254, 24, 211, 115, 92, 112, 140, 249, 255, 180, 138, 104, 29, 245, 120, 174, 182, 243, 8, 52, 81, 21, 8, 77, 35 }, new byte[] { 130, 231, 205, 173, 69, 116, 182, 182, 155, 105, 11, 203, 222, 250, 246, 71, 55, 124, 105, 107, 243, 165, 95, 133, 153, 165, 138, 40, 121, 59, 59, 201, 245, 140, 154, 233, 193, 175, 90, 10, 53, 18, 245, 94, 231, 241, 41, 141, 244, 39, 203, 195, 153, 134, 131, 192, 69, 178, 174, 137, 117, 241, 1, 9, 97, 222, 169, 12, 228, 192, 194, 252, 140, 225, 162, 84, 200, 3, 243, 130, 30, 190, 8, 203, 30, 242, 118, 42, 158, 57, 115, 23, 207, 117, 85, 181, 179, 5, 106, 249, 172, 99, 183, 170, 193, 102, 200, 130, 78, 109, 113, 105, 156, 44, 32, 114, 97, 19, 5, 203, 8, 178, 215, 89, 120, 33, 158, 46 }, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[] { 1, 1, 0, 10, 0, 100, 10, "Frodo", 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[] { 2, 1, 0, 15, 0, 100, 25, "Gandalf", 15, 1, 0 });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defeats", "Defense", "Fights", "HitPoints", "Intelligence", "Name", "Strength", "UserId", "Victories" },
                values: new object[] { 3, 2, 0, 1, 0, 100, 50, "Sauron", 100, 2, 0 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillsId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillsId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillsId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillsId" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 1, 1, 50, "The One Ring" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 2, 3, 50, "Nazgul" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillsId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
