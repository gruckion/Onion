using Microsoft.EntityFrameworkCore.Migrations;

namespace Onion.Repositories.Data.BusinessEntity.Migrations
{
	public partial class ChangedAuditableNamingsForSampleArch : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Modified",
				table: "Books",
				newName: "UpdatedDate");

			migrationBuilder.RenameColumn(
				name: "Created",
				table: "Books",
				newName: "CreatedDate");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "Books",
				maxLength: 100,
				nullable: false,
				oldClrType: typeof(string),
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Books",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldNullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "UpdatedDate",
				table: "Books",
				newName: "Modified");

			migrationBuilder.RenameColumn(
				name: "CreatedDate",
				table: "Books",
				newName: "Created");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "Books",
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 100);

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Books",
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 250);
		}
	}
}