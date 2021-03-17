﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolicySetService.Models;

namespace PolicySetService.Migrations
{
    [DbContext(typeof(PolicySetDbContext))]
    [Migration("20210317192531_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Logins");

                    b.HasData(
                        new
                        {
                            LoginId = new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"),
                            Certificate = "BwIAAACkAABSU0EyAAQAAAEAAQDBA9RfpDDf7vPUf760Zd3I2qknF+TBKmTkqPfLomG751XKApjRtNOG+3uvytkFocqjPoJOpEkuxR8YNBTqQ8GwpKfQGROeie3zNdgTwwyr+QptqJZICLBqMKV/C1O4rl7SNn8nSa8+iISviPa5mzV3Jip9WZWAy2NUISVk4VHotn80LWYup0rRd69i/mQX3iIy/lX2tCq53EKSePxLdu8zG1DdoafcnJtKroVgoD3CvS2mYuwrGNgBBjolHw6C+ua/p6WvJxGD0my9QTGtPsv6Ysg22ZpdWdYryAztq7LQFQyGKQHjfsJkPDtCxTFWeQb4PT6mN7YgI5sg2MzitLjKS0+L43eZI5iHcJ89cep6cXmU8KOimmjx08UFqLVshQs3STovQfxyafMSd6uDF1x8ZqhfHCJlcE3d/yPPqQkBVDlH+2VvAgYTzQU9ya5tLd9qQlulCpOJawjTLwn7ngd5pSz4PgDx9cC0ScarZR6Xl7slEHYk3o3HoE8m399ESIsj/qKKlvARGukw1roSrxY3I7RT6Hj5D1le0rh8Wr738vgNk3x8fnk1Xo6Q+HUeMI7fOLYFXTqYaD/4cWBHYbUtNQ0hdobO+dZHwhTDS6519oDb+b9kJPfQkOFL4kbEcFi3I0on2QdJVzBLMzDhAeziSy3OLg0PWKPQmTAZWfroVCs2X6GyRsn4BoLsW+Kfidghn1rpfrSnt68yvCMehXqmYojtWQxd/SDbSuR5D/Wc5F98DefQphY2lNOyFc8sRp8=",
                            GroupId = new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"),
                            LogIn = "Admin",
                            Password = "827ccb0eea8a706c4c34a16891f84e7b",
                            UserId = new Guid("a1972983-5389-49c5-a64d-bc4a65861a53")
                        });
                });

            modelBuilder.Entity("PolicyService.Policy", b =>
                {
                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("PlatformId")
                        .HasColumnType("smallint");

                    b.Property<string>("PolicyDefaultParam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyInstruction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("PolicyId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("PolicySetService.Models.PolicySet", b =>
                {
                    b.Property<Guid>("PolicySetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PolicyParam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Selected")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("PolicySetId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LoginId");

                    b.HasIndex("PolicyId");

                    b.ToTable("PolicySets");
                });

            modelBuilder.Entity("PolicySetService.Models.PolicySet", b =>
                {
                    b.HasOne("GroupService.Group", "PolicySetGroup")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("LoginService.Models.Login", "UserLogin")
                        .WithMany()
                        .HasForeignKey("LoginId");

                    b.HasOne("PolicyService.Policy", "PolicySetPolicy")
                        .WithMany()
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PolicySetGroup");

                    b.Navigation("PolicySetPolicy");

                    b.Navigation("UserLogin");
                });
#pragma warning restore 612, 618
        }
    }
}
