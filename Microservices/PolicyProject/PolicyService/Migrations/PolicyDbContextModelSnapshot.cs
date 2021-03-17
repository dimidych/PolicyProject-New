﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolicyService.Models;

namespace PolicyService.Migrations
{
    [DbContext(typeof(PolicyDbContext))]
    partial class PolicyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("DevicePlatformEntity.DevicePlatform", b =>
                {
                    b.Property<short>("DevicePlatformId")
                        .HasColumnType("smallint");

                    b.Property<string>("DevicePlatformName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("DevicePlatformId");

                    b.ToTable("DevicePlatforms");

                    b.HasData(
                        new
                        {
                            DevicePlatformId = (short)1,
                            DevicePlatformName = "Android"
                        },
                        new
                        {
                            DevicePlatformId = (short)2,
                            DevicePlatformName = "IOS"
                        },
                        new
                        {
                            DevicePlatformId = (short)3,
                            DevicePlatformName = "Windows"
                        },
                        new
                        {
                            DevicePlatformId = (short)4,
                            DevicePlatformName = "Linux"
                        });
                });

            modelBuilder.Entity("PolicyService.Policy", b =>
                {
                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short?>("DevicePlatformId")
                        .HasColumnType("smallint");

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

                    b.HasIndex("DevicePlatformId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("PolicyService.Policy", b =>
                {
                    b.HasOne("DevicePlatformEntity.DevicePlatform", "DevicePlatform")
                        .WithMany()
                        .HasForeignKey("DevicePlatformId");

                    b.Navigation("DevicePlatform");
                });
#pragma warning restore 612, 618
        }
    }
}
