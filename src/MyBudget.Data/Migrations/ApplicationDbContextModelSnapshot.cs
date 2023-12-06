﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBudget.Data;

#nullable disable

namespace MyBudget.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("MyBudget.Application.Entities.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Amount")
                        .HasPrecision(2)
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EffectiveDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ExpenseCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExpenseType")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseCategoryId");

                    b.ToTable("Expense");
                });

            modelBuilder.Entity("MyBudget.Application.Entities.ExpenseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExpenseCategory");
                });

            modelBuilder.Entity("MyBudget.Application.Entities.ExpenseEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasPrecision(2)
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseEntry");
                });

            modelBuilder.Entity("MyBudget.Application.Entities.Expense", b =>
                {
                    b.HasOne("MyBudget.Application.Entities.ExpenseCategory", null)
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseCategoryId");
                });

            modelBuilder.Entity("MyBudget.Application.Entities.ExpenseEntry", b =>
                {
                    b.HasOne("MyBudget.Application.Entities.Expense", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("MyBudget.Application.Entities.ExpenseCategory", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
