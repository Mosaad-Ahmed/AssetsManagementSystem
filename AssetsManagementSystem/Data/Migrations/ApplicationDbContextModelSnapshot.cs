﻿// <auto-generated />
using System;
using AssetsManagementSystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetsManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("AssignedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("ModelNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("WarrantyExpiryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetDisposalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("AssetId");

                    b.ToTable("AssetDisposalRecords");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetLifecycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PerformedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("PerformedById");

                    b.ToTable("AssetLifecycles");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetMaintenanceRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("PerformedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("PerformedById");

                    b.ToTable("AssetMaintenanceRecords");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetTransferRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FromLocationId")
                        .HasColumnType("int");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RejectionReason")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("ToLocationId")
                        .HasColumnType("int");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("FromLocationId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToLocationId");

                    b.HasIndex("ToUserId");

                    b.ToTable("AssetTransferRecords");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetsSuppliers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AssetId");

                    b.HasIndex("SupplierId");

                    b.ToTable("AssetsSuppliers");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.DataConsistencyCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IssuesFound")
                        .HasColumnType("bit");

                    b.Property<Guid>("PerformedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("PerformedById");

                    b.ToTable("DataConsistencyChecks");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UploadedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("UploadedById");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5285f833-cb8c-48b3-b51a-ea09a55e6900"),
                            ConcurrencyStamp = "c9bda18c-be45-49ce-8943-524cce790079",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("01b58219-7834-4c2e-abff-d5b878276f96"),
                            ConcurrencyStamp = "acbfa334-47c7-4b73-ad21-af6917afbc03",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = new Guid("50ddc3f5-11da-4df0-94ca-1ef2871d4a5f"),
                            ConcurrencyStamp = "8a9d9e15-82aa-46bf-8d96-3cd30e4c1967",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = new Guid("94201fa8-b0aa-40b8-b63f-d3130f5e53e4"),
                            ConcurrencyStamp = "cd64247c-ac81-4396-91bc-c566de9e868b",
                            Name = "Auditor",
                            NormalizedName = "AUDITOR"
                        });
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Asset", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "AssignedUser")
                        .WithMany("AssignedAssets")
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Location", "Location")
                        .WithMany("Assets")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.SubCategory", "AssetSubCategory")
                        .WithMany("Assets")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AssetSubCategory");

                    b.Navigation("AssignedUser");

                    b.Navigation("Category");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetDisposalRecord", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("AssetDisposalRecords")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetLifecycle", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("LifecycleEvents")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "PerformedBy")
                        .WithMany()
                        .HasForeignKey("PerformedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("PerformedBy");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetMaintenanceRecords", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("AssetMaintenanceRecords")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "PerformedBy")
                        .WithMany()
                        .HasForeignKey("PerformedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("PerformedBy");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetTransferRecords", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("AssetTransferRecords")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Location", "FromLocation")
                        .WithMany()
                        .HasForeignKey("FromLocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Location", "ToLocation")
                        .WithMany()
                        .HasForeignKey("ToLocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("FromLocation");

                    b.Navigation("FromUser");

                    b.Navigation("ToLocation");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.AssetsSuppliers", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("AssetsSuppliers")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.Supplier", "Supplier")
                        .WithMany("AssetsSuppliers")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.DataConsistencyCheck", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "PerformedBy")
                        .WithMany()
                        .HasForeignKey("PerformedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PerformedBy");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Document", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Asset", "Asset")
                        .WithMany("Documents")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.SubCategory", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Category", "MainAssetCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MainAssetCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AssetsManagementSystem.Models.DbSets.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Asset", b =>
                {
                    b.Navigation("AssetDisposalRecords");

                    b.Navigation("AssetMaintenanceRecords");

                    b.Navigation("AssetTransferRecords");

                    b.Navigation("AssetsSuppliers");

                    b.Navigation("Documents");

                    b.Navigation("LifecycleEvents");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Location", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.SubCategory", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.Supplier", b =>
                {
                    b.Navigation("AssetsSuppliers");
                });

            modelBuilder.Entity("AssetsManagementSystem.Models.DbSets.User", b =>
                {
                    b.Navigation("AssignedAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
