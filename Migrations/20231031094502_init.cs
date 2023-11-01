using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ID);
                });


                for ( int i = 1 ; i <= 200 ; i ++) {
                    migrationBuilder.InsertData (
                        table : "Article" ,
                        columns : new [] {"Title", "CreateTime" , "Content"} ,
                        values : new object [] {
                            $"Bai viet so {i}",
                            new DateTime(2021, 08, 20) ,
                            $"Day la phan noi dung cua bai viet so {i}"
                        }
                    );
                }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}
