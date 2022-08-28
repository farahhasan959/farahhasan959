using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RightProject.Migrations
{
    public partial class kk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminManagers",
                columns: table => new
                {
                    Managerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lockout = table.Column<bool>(type: "bit", nullable: false),
                    lockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorLogCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminManagers", x => x.Managerid);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Customerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lockout = table.Column<bool>(type: "bit", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    lockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorLogCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customerid);
                });

            migrationBuilder.CreateTable(
                name: "ShopManagers",
                columns: table => new
                {
                    Shopmanagerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lockout = table.Column<bool>(type: "bit", nullable: false),
                    lockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorLogCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopManagers", x => x.Shopmanagerid);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roleid = table.Column<int>(type: "int", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRoles_AppRoles_Roleid",
                        column: x => x.Roleid,
                        principalTable: "AppRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Giftid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IdCustomerNavigationCustomerid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Giftid);
                    table.ForeignKey(
                        name: "FK_Gifts_Customers_IdCustomerNavigationCustomerid",
                        column: x => x.IdCustomerNavigationCustomerid,
                        principalTable: "Customers",
                        principalColumn: "Customerid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Orderid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Custid = table.Column<int>(type: "int", nullable: false),
                    Parcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Orderid);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_Custid",
                        column: x => x.Custid,
                        principalTable: "Customers",
                        principalColumn: "Customerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Shopid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Managerid = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locathion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Shopid);
                    table.ForeignKey(
                        name: "FK_Shops_ShopManagers_Managerid",
                        column: x => x.Managerid,
                        principalTable: "ShopManagers",
                        principalColumn: "Shopmanagerid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    DateOfBurth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_ShopManagers_Userid",
                        column: x => x.Userid,
                        principalTable: "ShopManagers",
                        principalColumn: "Shopmanagerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Billid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Returned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Billid);
                    table.ForeignKey(
                        name: "FK_Bills_Orders_order",
                        column: x => x.order,
                        principalTable: "Orders",
                        principalColumn: "Orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Productid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shopId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    PointsToBuy = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Productid);
                    table.ForeignKey(
                        name: "FK_Products_Shops_shopId",
                        column: x => x.shopId,
                        principalTable: "Shops",
                        principalColumn: "Shopid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductShopOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOrderNavigationOrderid = table.Column<int>(type: "int", nullable: true),
                    IdProductNavigationProductid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShopOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductShopOrders_Orders_IdOrderNavigationOrderid",
                        column: x => x.IdOrderNavigationOrderid,
                        principalTable: "Orders",
                        principalColumn: "Orderid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductShopOrders_Products_IdProductNavigationProductid",
                        column: x => x.IdProductNavigationProductid,
                        principalTable: "Products",
                        principalColumn: "Productid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "id", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "id", "RoleName" },
                values: new object[] { 2, "ShopManager" });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "id", "RoleName" },
                values: new object[] { 3, "Customer" });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_order",
                table: "Bills",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_IdCustomerNavigationCustomerid",
                table: "Gifts",
                column: "IdCustomerNavigationCustomerid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Custid",
                table: "Orders",
                column: "Custid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_shopId",
                table: "Products",
                column: "shopId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShopOrders_IdOrderNavigationOrderid",
                table: "ProductShopOrders",
                column: "IdOrderNavigationOrderid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShopOrders_IdProductNavigationProductid",
                table: "ProductShopOrders",
                column: "IdProductNavigationProductid");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_Managerid",
                table: "Shops",
                column: "Managerid",
                unique: true,
                filter: "[Managerid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Userid",
                table: "UserProfiles",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Roleid",
                table: "UserRoles",
                column: "Roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminManagers");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "ProductShopOrders");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "ShopManagers");
        }
    }
}
