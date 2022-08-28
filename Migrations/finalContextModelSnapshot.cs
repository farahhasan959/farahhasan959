﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RightProject.Models;

namespace RightProject.Migrations
{
    [DbContext(typeof(finalContext))]
    partial class finalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Arabic_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RightProject.Models.AdminManager", b =>
                {
                    b.Property<int>("Managerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ErrorLogCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordConfirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lockTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("lockout")
                        .HasColumnType("bit");

                    b.HasKey("Managerid");

                    b.ToTable("AdminManagers");
                });

            modelBuilder.Entity("RightProject.Models.AppRole", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            id = 2,
                            RoleName = "ShopManager"
                        },
                        new
                        {
                            id = 3,
                            RoleName = "Customer"
                        });
                });

            modelBuilder.Entity("RightProject.Models.Bill", b =>
                {
                    b.Property<int>("Billid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PayMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Returned")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.HasKey("Billid");

                    b.HasIndex("order")
                        .IsUnique();

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("RightProject.Models.Customer", b =>
                {
                    b.Property<int>("Customerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Bdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ErrorLogCount")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordConfirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lockTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("lockout")
                        .HasColumnType("bit");

                    b.HasKey("Customerid");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RightProject.Models.Gift", b =>
                {
                    b.Property<int>("Giftid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdCustomerNavigationCustomerid")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Giftid");

                    b.HasIndex("IdCustomerNavigationCustomerid");

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("RightProject.Models.Order", b =>
                {
                    b.Property<int>("Orderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Custid")
                        .HasColumnType("int");

                    b.Property<string>("Parcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Orderid");

                    b.HasIndex("Custid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RightProject.Models.Product", b =>
                {
                    b.Property<int>("Productid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("PointsToBuy")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("shopId")
                        .HasColumnType("int");

                    b.HasKey("Productid");

                    b.HasIndex("shopId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RightProject.Models.ProductShopOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int?>("IdOrderNavigationOrderid")
                        .HasColumnType("int");

                    b.Property<int>("IdProduct")
                        .HasColumnType("int");

                    b.Property<int?>("IdProductNavigationProductid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdOrderNavigationOrderid");

                    b.HasIndex("IdProductNavigationProductid");

                    b.ToTable("ProductShopOrders");
                });

            modelBuilder.Entity("RightProject.Models.Shop", b =>
                {
                    b.Property<int>("Shopid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Locathion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Managerid")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phonenumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Shopid");

                    b.HasIndex("Managerid")
                        .IsUnique()
                        .HasFilter("[Managerid] IS NOT NULL");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("RightProject.Models.ShopManager", b =>
                {
                    b.Property<int>("Shopmanagerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ErrorLogCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordConfirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lockTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("lockout")
                        .HasColumnType("bit");

                    b.HasKey("Shopmanagerid");

                    b.ToTable("ShopManagers");
                });

            modelBuilder.Entity("RightProject.Models.UserProfile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBurth")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Userid")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("RightProject.Models.UserRole", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Roleid")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Roleid");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("RightProject.Models.Bill", b =>
                {
                    b.HasOne("RightProject.Models.Order", "orderN")
                        .WithOne("bill")
                        .HasForeignKey("RightProject.Models.Bill", "order")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("orderN");
                });

            modelBuilder.Entity("RightProject.Models.Gift", b =>
                {
                    b.HasOne("RightProject.Models.Customer", "IdCustomerNavigation")
                        .WithMany()
                        .HasForeignKey("IdCustomerNavigationCustomerid");

                    b.Navigation("IdCustomerNavigation");
                });

            modelBuilder.Entity("RightProject.Models.Order", b =>
                {
                    b.HasOne("RightProject.Models.Customer", "Cust")
                        .WithMany("Orders")
                        .HasForeignKey("Custid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cust");
                });

            modelBuilder.Entity("RightProject.Models.Product", b =>
                {
                    b.HasOne("RightProject.Models.Shop", "shopIdNavigation")
                        .WithMany("Products")
                        .HasForeignKey("shopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("shopIdNavigation");
                });

            modelBuilder.Entity("RightProject.Models.ProductShopOrder", b =>
                {
                    b.HasOne("RightProject.Models.Order", "IdOrderNavigation")
                        .WithMany("ProductShopOrders")
                        .HasForeignKey("IdOrderNavigationOrderid");

                    b.HasOne("RightProject.Models.Product", "IdProductNavigation")
                        .WithMany("ProductShopOrders")
                        .HasForeignKey("IdProductNavigationProductid");

                    b.Navigation("IdOrderNavigation");

                    b.Navigation("IdProductNavigation");
                });

            modelBuilder.Entity("RightProject.Models.Shop", b =>
                {
                    b.HasOne("RightProject.Models.ShopManager", "Manager")
                        .WithOne("Shops")
                        .HasForeignKey("RightProject.Models.Shop", "Managerid");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("RightProject.Models.UserProfile", b =>
                {
                    b.HasOne("RightProject.Models.ShopManager", "shopManager")
                        .WithMany()
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("shopManager");
                });

            modelBuilder.Entity("RightProject.Models.UserRole", b =>
                {
                    b.HasOne("RightProject.Models.AppRole", "appRole")
                        .WithMany()
                        .HasForeignKey("Roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appRole");
                });

            modelBuilder.Entity("RightProject.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RightProject.Models.Order", b =>
                {
                    b.Navigation("bill");

                    b.Navigation("ProductShopOrders");
                });

            modelBuilder.Entity("RightProject.Models.Product", b =>
                {
                    b.Navigation("ProductShopOrders");
                });

            modelBuilder.Entity("RightProject.Models.Shop", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("RightProject.Models.ShopManager", b =>
                {
                    b.Navigation("Shops");
                });
#pragma warning restore 612, 618
        }
    }
}
