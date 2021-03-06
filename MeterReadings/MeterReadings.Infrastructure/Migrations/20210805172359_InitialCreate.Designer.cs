// <auto-generated />
using System;
using MeterReadings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeterReadings.Infrastructure.Migrations
{
    [DbContext(typeof(MeterReadingDbContext))]
    [Migration("20210805172359_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeterReadings.Domain.MeterReadingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("111e6780-5d38-48bd-b81e-42b0b9cc8ef9"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("MeterReadValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MeterReadingDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MeterReadings");
                });
#pragma warning restore 612, 618
        }
    }
}
