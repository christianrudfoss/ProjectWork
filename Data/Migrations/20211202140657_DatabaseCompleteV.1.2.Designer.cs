// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DbProjectWorkContext))]
    [Migration("20211202140657_DatabaseCompleteV.1.2")]
    partial class DatabaseCompleteV12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.ProjectAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ApplicationCore.Entities.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WorkAggregate.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Work");
                });
#pragma warning restore 612, 618
        }
    }
}
