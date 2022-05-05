using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenesisBlog.Data.Migrations
{
    public partial class RemainingProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogPostState",
                table: "BlogPost",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "BlogPost",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "BlogPost",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogPost",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogPost",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogPostState",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogPost");
        }
    }
}
