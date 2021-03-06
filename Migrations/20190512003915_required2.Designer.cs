﻿// <auto-generated />
using System;
using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendApi.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20190512003915_required2")]
    partial class required2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendApi.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company");

                    b.Property<bool>("Running");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("BackendApi.Entities.ProjectAndUser", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("UserId");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectAndUser");
                });

            modelBuilder.Entity("BackendApi.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName");

                    b.Property<int?>("TaskId");

                    b.HasKey("TagId");

                    b.HasIndex("TaskId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BackendApi.Entities.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Finished");

                    b.Property<int?>("ProjectID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("ToFinish");

                    b.Property<DateTime?>("ToStart");

                    b.Property<int?>("UserID");

                    b.HasKey("TaskId");

                    b.HasIndex("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("BackendApi.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BackendApi.Entities.ProjectAndUser", b =>
                {
                    b.HasOne("BackendApi.Entities.Project", "Project")
                        .WithMany("Users")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackendApi.Entities.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BackendApi.Entities.Tag", b =>
                {
                    b.HasOne("BackendApi.Entities.Task")
                        .WithMany("Tags")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("BackendApi.Entities.Task", b =>
                {
                    b.HasOne("BackendApi.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectID");

                    b.HasOne("BackendApi.Entities.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
