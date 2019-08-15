using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Onion.Repositories.Data.BusinessEntity.Migrations
{
	public partial class InitMigrationSqlServerBusinessEntity : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Books",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Created = table.Column<DateTimeOffset>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Modified = table.Column<DateTimeOffset>(nullable: false),
					Name = table.Column<string>(nullable: true),
					Ordinal = table.Column<int>(nullable: false),
					UpdatedBy = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Books", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Books");
		}
	}
}