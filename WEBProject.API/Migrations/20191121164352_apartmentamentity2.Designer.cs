﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEBProject.API.Data;

namespace WEBProject.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191121164352_apartmentamentity2")]
    partial class apartmentamentity2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WEBProject.API.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Street");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WEBProject.API.Models.Amentity", b =>
                {
                    b.Property<int>("AmentityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.HasKey("AmentityId");

                    b.ToTable("Amentities");
                });

            modelBuilder.Entity("WEBProject.API.Models.Apartment", b =>
                {
                    b.Property<int>("ApartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HostId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("LocationId");

                    b.Property<int>("NumberOfGuests");

                    b.Property<int>("NumberOfRooms");

                    b.Property<int>("PricePerNight");

                    b.Property<string>("Status");

                    b.Property<string>("TimeToArrive");

                    b.Property<string>("TimeToLeave");

                    b.Property<string>("Type");

                    b.HasKey("ApartmentId");

                    b.HasIndex("HostId");

                    b.HasIndex("LocationId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("WEBProject.API.Models.ApartmentAmentity", b =>
                {
                    b.Property<int>("ApartmentId");

                    b.Property<int>("AmentityId");

                    b.HasKey("ApartmentId", "AmentityId");

                    b.HasIndex("AmentityId");

                    b.ToTable("ApartmentAmentities");
                });

            modelBuilder.Entity("WEBProject.API.Models.BlockedDate", b =>
                {
                    b.Property<DateTime>("Date");

                    b.Property<int?>("ApartmentId");

                    b.HasKey("Date");

                    b.HasIndex("ApartmentId");

                    b.ToTable("BlockedDate");
                });

            modelBuilder.Entity("WEBProject.API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<bool>("Approved");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Grade");

                    b.Property<string>("Text");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WEBProject.API.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("WEBProject.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("WEBProject.API.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppartmentApartmentId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("GuestId");

                    b.Property<int>("NumberOfNights");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<double>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("AppartmentApartmentId");

                    b.HasIndex("GuestId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WEBProject.API.Models.ReservedDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<int>("CurrentNumberOfGuests");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ReservedDate");
                });

            modelBuilder.Entity("WEBProject.API.Models.ReservedDayFromToday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<int>("DayFromToday");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ReservedDayFromToday");
                });

            modelBuilder.Entity("WEBProject.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsBlocked");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WEBProject.API.Models.Apartment", b =>
                {
                    b.HasOne("WEBProject.API.Models.User", "Host")
                        .WithMany("RentedApartments")
                        .HasForeignKey("HostId");

                    b.HasOne("WEBProject.API.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("WEBProject.API.Models.ApartmentAmentity", b =>
                {
                    b.HasOne("WEBProject.API.Models.Amentity", "Amentity")
                        .WithMany("ApartmentAmentities")
                        .HasForeignKey("AmentityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WEBProject.API.Models.Apartment", "Apartment")
                        .WithMany("ApartmentAmentities")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBProject.API.Models.BlockedDate", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment")
                        .WithMany("BlockedDates")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Comment", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment", "Apartment")
                        .WithMany("Comments")
                        .HasForeignKey("ApartmentId");

                    b.HasOne("WEBProject.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Location", b =>
                {
                    b.HasOne("WEBProject.API.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Photo", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment", "Apartment")
                        .WithMany("Photos")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Reservation", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment", "Appartment")
                        .WithMany("Reservations")
                        .HasForeignKey("AppartmentApartmentId");

                    b.HasOne("WEBProject.API.Models.User", "Guest")
                        .WithMany("Reservations")
                        .HasForeignKey("GuestId");
                });

            modelBuilder.Entity("WEBProject.API.Models.ReservedDate", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment")
                        .WithMany("ReservedDates")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("WEBProject.API.Models.ReservedDayFromToday", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment")
                        .WithMany("ReservedDaysFromToday")
                        .HasForeignKey("ApartmentId");
                });
#pragma warning restore 612, 618
        }
    }
}
