﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20220222155021_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Профиль ориентирован на подготовку квалифицированных руководителей и специалистов в области бухгалтерского учета, прикладного экономического анализа и аудита для работы в предприятиях различных форм собственности различного уровня, консалтинговых и аудиторских фирмах, банках, инвестиционных и страховых компаниях.",
                            Name = "Экономика"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Профиль ориентирован на подготовку квалифицированных специалистов в организациях любых отраслей экономики и форм собственности в качестве программиста, специалиста по тестированию в области информационных технологий, администратора баз данных, специалиста по технической документации в области информационных технологий; системного аналитика; специалиста по дизайну, графических и пользовательских интерфейсов, системного администратора информационно-коммуникационных систем, специалиста по администрированию сетевых устройств информационно-коммуникационных систем, системным программистом.",
                            Name = "Информатика и вычислительная техника"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Профиль ориентирован на подготовку квалифицированных специалистов занимающихся разработкой программных комплексов внедрением и эксплуатацией информационно-коммуникативных технологий различных предметных областях, а также в организациях и на предприятиях различных отраслей экономики и форм собственности в качестве программиста, специалиста по информационным системам, руководителя проектов в области информационных технологий, руководителя разработки программного обеспечения, системного аналитика.",
                            Name = "Прикладная информатика"
                        });
                });

            modelBuilder.Entity("Model.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            Name = "ПИ"
                        });
                });

            modelBuilder.Entity("Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Model.Group", b =>
                {
                    b.HasOne("Model.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Model.Student", b =>
                {
                    b.HasOne("Model.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });
#pragma warning restore 612, 618
        }
    }
}