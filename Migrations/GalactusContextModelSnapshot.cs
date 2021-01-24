﻿// <auto-generated />
using Galactus.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Galactus.Migrations
{
    [DbContext(typeof(GalactusContext))]
    partial class GalactusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Galactus.Models.Planeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AstCarbono")
                        .HasColumnType("int");

                    b.Property<int>("AstLivre")
                        .HasColumnType("int");

                    b.Property<int>("AstNiobio")
                        .HasColumnType("int");

                    b.Property<int>("AstPlutonio")
                        .HasColumnType("int");

                    b.Property<int>("Carbono")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Niobio")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Plutonio")
                        .HasColumnType("int");

                    b.Property<int>("consEsta")
                        .HasColumnType("int");

                    b.Property<int>("consEstaAv")
                        .HasColumnType("int");

                    b.Property<int>("consEstaOrb")
                        .HasColumnType("int");

                    b.Property<int>("consFabDro")
                        .HasColumnType("int");

                    b.Property<int>("consRefAvan")
                        .HasColumnType("int");

                    b.Property<int>("consRefCar")
                        .HasColumnType("int");

                    b.Property<int>("consRefNio")
                        .HasColumnType("int");

                    b.Property<int>("pesGde")
                        .HasColumnType("int");

                    b.Property<int>("pesMin")
                        .HasColumnType("int");

                    b.Property<int>("pesNav")
                        .HasColumnType("int");

                    b.Property<int>("pesRad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Planeta");
                });

            modelBuilder.Entity("Galactus.Models.TimeControle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Tempo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeControle");
                });
#pragma warning restore 612, 618
        }
    }
}
