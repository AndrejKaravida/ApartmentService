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
    [Migration("20191102105610_ApartmentChanged")]
    partial class ApartmentChanged
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

                    b.Property<string>("Street");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WEBProject.API.Models.Amentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Amentities");
                });

            modelBuilder.Entity("WEBProject.API.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HostId");

                    b.Property<int?>("LocationId");

                    b.Property<int>("NumberOfGuests");

                    b.Property<int>("NumberOfRooms");

                    b.Property<string>("Photo");

                    b.Property<int>("PricePerNight");

                    b.Property<string>("Status");

                    b.Property<string>("TimeToArrive");

                    b.Property<string>("TimeToLeave");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.HasIndex("LocationId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("WEBProject.API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<int?>("AuthorId");

                    b.Property<int>("Grade");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("AuthorId");

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

            modelBuilder.Entity("WEBProject.API.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppartmentId");

                    b.Property<int?>("GuestId");

                    b.Property<int>("NumberOfNights");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<int>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("AppartmentId");

                    b.HasIndex("GuestId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WEBProject.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WEBProject.API.Models.Amentity", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment")
                        .WithMany("Amentities")
                        .HasForeignKey("ApartmentId");
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

            modelBuilder.Entity("WEBProject.API.Models.Comment", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment", "Apartment")
                        .WithMany("Comments")
                        .HasForeignKey("ApartmentId");

                    b.HasOne("WEBProject.API.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Location", b =>
                {
                    b.HasOne("WEBProject.API.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("WEBProject.API.Models.Reservation", b =>
                {
                    b.HasOne("WEBProject.API.Models.Apartment", "Appartment")
                        .WithMany("Reservations")
                        .HasForeignKey("AppartmentId");

                    b.HasOne("WEBProject.API.Models.User", "Guest")
                        .WithMany("Reservations")
                        .HasForeignKey("GuestId");
                });
#pragma warning restore 612, 618
        }
    }
}
