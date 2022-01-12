﻿// <auto-generated />
using System;
using ManagementIt.DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ManagementIt.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211213102852_UpdateEntityOrBase")]
    partial class UpdateEntityOrBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AppId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int>("DeptId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EventDateAndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StateName")
                        .HasColumnType("text");

                    b.Property<string>("UserNameIniciator")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationsAction");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationsPriority");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BGColor")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationsState");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationToIt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Contact")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int>("DepartamentId")
                        .HasColumnType("integer");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<bool>("OnDelete")
                        .HasColumnType("boolean");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("integer");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int?>("StateId")
                        .HasColumnType("integer");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PriorityId");

                    b.HasIndex("StateId");

                    b.HasIndex("TypeId");

                    b.ToTable("ApplicationsToIt");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationsType");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationToIt", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationPriority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Priority");

                    b.Navigation("State");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
