﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using SmartWatts.Data;

#nullable disable

namespace SmartWatts.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartWatts.Models.ContadeLuz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BandeiraTarifaria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<decimal>("ConsumoKwh")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MesReferencia")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("ResidenciaId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<decimal>("ValorPago")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ResidenciaId");

                    b.ToTable("ContasDeLuz");
                });

            modelBuilder.Entity("SmartWatts.Models.Residencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ConsumoTotal")
                        .HasColumnType("DECIMAL(18, 2)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("QuantidadeMoradores")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Residencias");
                });

            modelBuilder.Entity("SmartWatts.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SmartWatts.Models.ContadeLuz", b =>
                {
                    b.HasOne("SmartWatts.Models.Residencia", "Residencia")
                        .WithMany("ContasDeLuz")
                        .HasForeignKey("ResidenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Residencia");
                });

            modelBuilder.Entity("SmartWatts.Models.Residencia", b =>
                {
                    b.HasOne("SmartWatts.Models.Usuario", "Usuario")
                        .WithMany("Residencias")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SmartWatts.Models.Residencia", b =>
                {
                    b.Navigation("ContasDeLuz");
                });

            modelBuilder.Entity("SmartWatts.Models.Usuario", b =>
                {
                    b.Navigation("Residencias");
                });
#pragma warning restore 612, 618
        }
    }
}
