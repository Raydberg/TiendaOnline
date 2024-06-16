using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaOnline.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    IDMARCA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_MARCA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.IDMARCA);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    IdModelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaIDMARCA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IdModelo);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaIDMARCA",
                        column: x => x.MarcaIDMARCA,
                        principalTable: "Marcas",
                        principalColumn: "IDMARCA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    año = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloIDMODELO = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Modelos_ModeloIDMODELO",
                        column: x => x.ModeloIDMODELO,
                        principalTable: "Modelos",
                        principalColumn: "IdModelo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 1, "Samsung" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 2, "Apple" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 3, "Xiaomi" });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IdModelo", "MarcaIDMARCA", "Nom_Modelo" },
                values: new object[,]
                {
                    { 1, 1, "Galaxy S23 Ultra" },
                    { 2, 1, " Galaxy Z Fold 4" },
                    { 3, 1, "Galaxy Watch 5 Pro" },
                    { 4, 1, "Galaxy Buds 2 Pro" },
                    { 5, 1, "QN90A Neo QLED" },
                    { 6, 2, "iPhone 14 Pro Max" },
                    { 7, 2, "Watch Series 8" },
                    { 8, 2, "AirPods Pro" },
                    { 9, 2, "AirPods Max" },
                    { 10, 2, "iPhone SE" },
                    { 11, 3, "Xiaomi 13 Pro" },
                    { 12, 3, "Redmi Note 12 Pro" },
                    { 13, 3, "True Wireless Earphones 2 Pro" },
                    { 14, 3, "TV Q1 75" },
                    { 15, 3, "Redmi Buds 4 Pro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaIDMARCA",
                table: "Modelos",
                column: "MarcaIDMARCA");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ModeloIDMODELO",
                table: "Productos",
                column: "ModeloIDMODELO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
