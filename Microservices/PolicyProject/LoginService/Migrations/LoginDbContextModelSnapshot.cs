﻿// <auto-generated />
using System;
using LoginService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginService.Migrations
{
    [DbContext(typeof(LoginDbContext))]
    partial class LoginDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("GroupService.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            GroupId = new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"),
                            GroupName = "Admin"
                        });
                });

            modelBuilder.Entity("LoginService.Models.Login", b =>
                {
                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Certificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LogIn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Logins");

                    b.HasData(
                        new
                        {
                            LoginId = new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"),
                            Certificate = "BwIAAACkAABSU0EyAAQAAAEAAQC1EzUrc1PDqwnFsfX5YByYLcdyZZsv5arMReG2Hf5QPHYdOOhlrvcfANW45Ex32F8Tq0AFQyjgh1tIdWLTmKicTJnRuYjq5DzZIa3o6er6t+JkKFaaVQI7gk4mI7HrmufYvzJDuWxiFoogm+uUqnT3YKxmkbUYODtTZjJlpy302mddZbjdVb0zwxUxSQt2M2w/6GA4KhYp76z9UwV0LqBRIBgIL1jhhrXlrUXrMaysj0fmxSJMr6/nnTNOIz74DuSDeEKxaCV9EX2jdm7k6iUBejWQfATxrrmKdEDfYA+2mdWbDex3ZMuCnIPVeqUn3lijDdRjgX+eJx/uWBJipMf1IYB2Yhwk3/CtKBrG9IIpJnMcloYTbFFl5/cx/Qsg9mrlIVxyT+/qRVr9xxXKJK6FXaSOJm57+IbYwDs1R7HcPyOwncUs5Au9GA6K3GXQEGEAXj9ZiDlDwbM0z/0ZirgzgvZMOpQL5ZGB2cJWwpuPU42HDkq+3fQ/a7D9EXWXq2PgirBFQulsK5nHalEAGRxsRq8wWyiIomfrSCFSjSRldaRr4GP1FzADg5TUsp6qYX0eCB9Y2Ea95Avcw1+C0Hdi5a2FaXzNobCOo3EfgNDwig7WJbVm/fP6QRcZqLcG+TpI0J8h9yzkVvZDc5/qdoi4Y2i+g6bUvbLLzcv3pSSem/l+pHnz+PigInv/Z2taTt2qgPsOV2rFNTvgTVhiztGUMV/Q3c13WVBdEssFBiAHztzaKsmBM0wWsdvwQRDerKk=",
                            GroupId = new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"),
                            LogIn = "Admin",
                            Password = "827ccb0eea8a706c4c34a16891f84e7b",
                            UserId = new Guid("a1972983-5389-49c5-a64d-bc4a65861a53")
                        });
                });

            modelBuilder.Entity("UserService.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserMiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a1972983-5389-49c5-a64d-bc4a65861a53"),
                            UserFirstName = "Admin",
                            UserLastName = "Admin"
                        });
                });

            modelBuilder.Entity("LoginService.Models.Login", b =>
                {
                    b.HasOne("GroupService.Group", "LoginGroup")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserService.Models.User", "LoginUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginGroup");

                    b.Navigation("LoginUser");
                });
#pragma warning restore 612, 618
        }
    }
}
