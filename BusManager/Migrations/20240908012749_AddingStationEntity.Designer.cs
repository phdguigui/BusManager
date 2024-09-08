﻿// <auto-generated />
using System;
using BusManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusManager.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240908012749_AddingStationEntity")]
    partial class AddingStationEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BusManager.Model.Entities.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Plate");

                    b.ToTable("Buses", (string)null);
                });

            modelBuilder.Entity("BusManager.Model.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("DATE");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("DATE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)");

                    b.HasKey("Id");

                    b.HasIndex("CPF");

                    b.HasIndex("Id");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("BusManager.Model.Entities.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Number");

                    b.ToTable("Stations", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}