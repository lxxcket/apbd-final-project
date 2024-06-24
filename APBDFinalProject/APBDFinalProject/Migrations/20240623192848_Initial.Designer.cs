﻿// <auto-generated />
using System;
using APBDFinalProject.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APBDFinalProject.Migrations
{
    [DbContext(typeof(IncomeContext))]
    [Migration("20240623192848_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APBDFinalProject.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DaysSpan")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("IdVersion")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupportTime")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalContractPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdVersion");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("APBDFinalProject.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdContract");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("YearlyPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Version", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdSoftware")
                        .HasColumnType("int");

                    b.Property<string>("VersionName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("APBDFinalProject.Models.BusinessCustomer", b =>
                {
                    b.HasBaseType("APBDFinalProject.Models.Customer");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("KRS")
                        .HasColumnType("int");

                    b.HasIndex("KRS")
                        .IsUnique()
                        .HasFilter("[KRS] IS NOT NULL");

                    b.ToTable("Business_Customers", (string)null);
                });

            modelBuilder.Entity("APBDFinalProject.Models.IndividualCustomer", b =>
                {
                    b.HasBaseType("APBDFinalProject.Models.Customer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PESEL")
                        .HasMaxLength(11)
                        .HasColumnType("int");

                    b.HasIndex("PESEL")
                        .IsUnique()
                        .HasFilter("[PESEL] IS NOT NULL");

                    b.ToTable("Individual_Customers", (string)null);
                });

            modelBuilder.Entity("APBDFinalProject.Models.Contract", b =>
                {
                    b.HasOne("APBDFinalProject.Models.Customer", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APBDFinalProject.Models.Version", "Version")
                        .WithMany()
                        .HasForeignKey("IdVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Version");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Payment", b =>
                {
                    b.HasOne("APBDFinalProject.Models.Contract", "Contract")
                        .WithMany("Payments")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Version", b =>
                {
                    b.HasOne("APBDFinalProject.Models.Software", "Software")
                        .WithMany("Versions")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("APBDFinalProject.Models.BusinessCustomer", b =>
                {
                    b.HasOne("APBDFinalProject.Models.Customer", null)
                        .WithOne()
                        .HasForeignKey("APBDFinalProject.Models.BusinessCustomer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APBDFinalProject.Models.IndividualCustomer", b =>
                {
                    b.HasOne("APBDFinalProject.Models.Customer", null)
                        .WithOne()
                        .HasForeignKey("APBDFinalProject.Models.IndividualCustomer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APBDFinalProject.Models.Contract", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Customer", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("APBDFinalProject.Models.Software", b =>
                {
                    b.Navigation("Versions");
                });
#pragma warning restore 612, 618
        }
    }
}
