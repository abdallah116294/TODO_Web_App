﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TODO_Web_App.Models;

namespace TODO_Web_App.Migrations
{
    [DbContext(typeof(ToDoContext))]
    partial class ToDoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TODO_Web_App.Models.Category", b =>
                {
                    b.Property<string>("categoryID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("categoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("categoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            categoryID = "work",
                            categoryName = "Work"
                        },
                        new
                        {
                            categoryID = "home",
                            categoryName = "Home"
                        },
                        new
                        {
                            categoryID = "shop",
                            categoryName = "Shopping"
                        },
                        new
                        {
                            categoryID = "call",
                            categoryName = "Contact"
                        },
                        new
                        {
                            categoryID = "ex",
                            categoryName = "Exercise"
                        });
                });

            modelBuilder.Entity("TODO_Web_App.Models.Status", b =>
                {
                    b.Property<string>("statusID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("statusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("statusID");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            statusID = "open",
                            statusName = "Open"
                        },
                        new
                        {
                            statusID = "closed",
                            statusName = "Closed"
                        });
                });

            modelBuilder.Entity("TODO_Web_App.Models.ToDO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("categoryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("dueDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("statusID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("categoryID");

                    b.HasIndex("statusID");

                    b.ToTable("ToDOs");
                });

            modelBuilder.Entity("TODO_Web_App.Models.ToDO", b =>
                {
                    b.HasOne("TODO_Web_App.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TODO_Web_App.Models.Status", "status")
                        .WithMany()
                        .HasForeignKey("statusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("status");
                });
#pragma warning restore 612, 618
        }
    }
}
