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
    [Migration("20211119191904_Role_AddRoleidentityId")]
    partial class Role_AddRoleidentityId
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

                    b.Property<int?>("ApplicationToItId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EventDateAndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("InitiatorEmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("StateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationToItId");

                    b.HasIndex("InitiatorEmployeeId");

                    b.HasIndex("StateId");

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

                    b.Property<int?>("DepartamentId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int?>("StateId")
                        .HasColumnType("integer");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("RoomId");

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

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Departament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SubdivisionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("Departaments");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("DepartamentId")
                        .HasColumnType("integer");

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("MobileTelephone")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<int?>("PositionId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("User")
                        .HasColumnType("text");

                    b.Property<string>("WorkTelephone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentId");

                    b.HasIndex("PhotoId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.EmployeePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("EmployeePhotos");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.Property<string>("RoleIdentityId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentCountSocket")
                        .HasColumnType("integer");

                    b.Property<int?>("DepartamentId")
                        .HasColumnType("integer");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RequiredCountSocket")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("DepartamentId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Subdivision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subdivisions");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationAction", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationToIt", "ApplicationToIt")
                        .WithMany()
                        .HasForeignKey("ApplicationToItId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Employee", "InitiatorEmployee")
                        .WithMany()
                        .HasForeignKey("InitiatorEmployeeId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.Navigation("ApplicationToIt");

                    b.Navigation("InitiatorEmployee");

                    b.Navigation("State");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.ApplicationEntity.ApplicationToIt", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Departament", "Departament")
                        .WithMany()
                        .HasForeignKey("DepartamentId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationPriority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("ManagementIt.Core.Domain.ApplicationEntity.ApplicationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Departament");

                    b.Navigation("Employee");

                    b.Navigation("Priority");

                    b.Navigation("Room");

                    b.Navigation("State");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Departament", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Subdivision", "Subdivision")
                        .WithMany()
                        .HasForeignKey("SubdivisionId");

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Employee", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Departament", "Departament")
                        .WithMany()
                        .HasForeignKey("DepartamentId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.EmployeePhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.Navigation("Departament");

                    b.Navigation("Photo");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Role", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Employee", null)
                        .WithMany("Roles")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Room", b =>
                {
                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId");

                    b.HasOne("ManagementIt.Core.Domain.SharedTables.Departament", "Departament")
                        .WithMany()
                        .HasForeignKey("DepartamentId");

                    b.Navigation("Building");

                    b.Navigation("Departament");
                });

            modelBuilder.Entity("ManagementIt.Core.Domain.SharedTables.Employee", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
