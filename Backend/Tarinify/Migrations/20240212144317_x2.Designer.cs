﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trainify.Context;

#nullable disable

namespace Trainify.Migrations
{
    [DbContext(typeof(DbContainer))]
    [Migration("20240212144317_x2")]
    partial class x2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Trainify.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("ExampleContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ExampleData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ExampleExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rep")
                        .HasColumnType("int");

                    b.Property<int?>("Rest")
                        .HasColumnType("int");

                    b.Property<int?>("Set")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ThumbnailData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ThumbnailExtension")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.ToTable("exersice");
                });

            modelBuilder.Entity("Trainify.Entities.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int?>("Carb")
                        .HasColumnType("int");

                    b.Property<int?>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Fats")
                        .HasColumnType("int");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Protein")
                        .HasColumnType("int");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.ToTable("meal");
                });

            modelBuilder.Entity("Trainify.Entities.MealType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("mealType");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.ToTable("nutritionPlan");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlanMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("MealTypeId")
                        .HasColumnType("int");

                    b.Property<int>("NutritionPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("MealTypeId");

                    b.HasIndex("NutritionPlanId");

                    b.ToTable("nutritionPlanMeal");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlanMealDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NutritionPlanMealId")
                        .HasColumnType("int");

                    b.Property<int>("WeekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NutritionPlanMealId");

                    b.HasIndex("WeekId");

                    b.ToTable("nutritionPlanMealDay");
                });

            modelBuilder.Entity("Trainify.Entities.PlatformPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ClientLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PlatformPlan");
                });

            modelBuilder.Entity("Trainify.Entities.ClientRegisteredPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrainerPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.HasIndex("TrainerPlanId");

                    b.ToTable("ClientRegisteredPlan");
                });

            modelBuilder.Entity("Trainify.Entities.TrainerPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.ToTable("TrainerPlan");
                });

            modelBuilder.Entity("Trainify.Entities.TrainerRegisteredPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PlatformPlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.HasIndex("PlatformPlanId");

                    b.ToTable("TrainerRegisteredPlan");
                });

            modelBuilder.Entity("Trainify.Entities.Week", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("week");
                });

            modelBuilder.Entity("Trainify.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtendIdentityUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExtendIdentityUserId");

                    b.ToTable("workout");
                });

            modelBuilder.Entity("Trainify.Entities.WorkoutExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("ExtendIdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("ExtendIdentityUserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("workoutExercise");
                });

            modelBuilder.Entity("Trainify.Privilage.ExtendIdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Trainify.Privilage.ExtendIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trainify.Entities.Exercise", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("exercise")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");
                });

            modelBuilder.Entity("Trainify.Entities.Meal", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("meal")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlan", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("nutritionPlan")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlanMeal", b =>
                {
                    b.HasOne("Trainify.Entities.Meal", "meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Entities.MealType", "mealType")
                        .WithMany("nutritionPlanMeal")
                        .HasForeignKey("MealTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Entities.NutritionPlan", "nutritionPlan")
                        .WithMany()
                        .HasForeignKey("NutritionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("meal");

                    b.Navigation("mealType");

                    b.Navigation("nutritionPlan");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlanMealDay", b =>
                {
                    b.HasOne("Trainify.Entities.NutritionPlanMeal", "nutritionPlanMeal")
                        .WithMany("nutritionPlanMealDay")
                        .HasForeignKey("NutritionPlanMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Entities.Week", "week")
                        .WithMany("nutritionPlanMealDay")
                        .HasForeignKey("WeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("nutritionPlanMeal");

                    b.Navigation("week");
                });

            modelBuilder.Entity("Trainify.Entities.ClientRegisteredPlan", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("ClientRegisteredPlan")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Entities.TrainerPlan", "trainerPlan")
                        .WithMany()
                        .HasForeignKey("TrainerPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");

                    b.Navigation("trainerPlan");
                });

            modelBuilder.Entity("Trainify.Entities.TrainerPlan", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("trainerPlan")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");
                });

            modelBuilder.Entity("Trainify.Entities.TrainerRegisteredPlan", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany("trainerRegisteredPlan")
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Entities.PlatformPlan", "platformPlan")
                        .WithMany()
                        .HasForeignKey("PlatformPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");

                    b.Navigation("platformPlan");
                });

            modelBuilder.Entity("Trainify.Entities.Workout", b =>
                {
                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", "extendIdentityUser")
                        .WithMany()
                        .HasForeignKey("ExtendIdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("extendIdentityUser");
                });

            modelBuilder.Entity("Trainify.Entities.WorkoutExercise", b =>
                {
                    b.HasOne("Trainify.Entities.Exercise", "exersice")
                        .WithMany("workoutExersice")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainify.Privilage.ExtendIdentityUser", null)
                        .WithMany("workoutExercise")
                        .HasForeignKey("ExtendIdentityUserId");

                    b.HasOne("Trainify.Entities.Workout", "workout")
                        .WithMany("workoutExersice")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("exersice");

                    b.Navigation("workout");
                });

            modelBuilder.Entity("Trainify.Entities.Exercise", b =>
                {
                    b.Navigation("workoutExersice");
                });

            modelBuilder.Entity("Trainify.Entities.MealType", b =>
                {
                    b.Navigation("nutritionPlanMeal");
                });

            modelBuilder.Entity("Trainify.Entities.NutritionPlanMeal", b =>
                {
                    b.Navigation("nutritionPlanMealDay");
                });

            modelBuilder.Entity("Trainify.Entities.Week", b =>
                {
                    b.Navigation("nutritionPlanMealDay");
                });

            modelBuilder.Entity("Trainify.Entities.Workout", b =>
                {
                    b.Navigation("workoutExersice");
                });

            modelBuilder.Entity("Trainify.Privilage.ExtendIdentityUser", b =>
                {
                    b.Navigation("exercise");

                    b.Navigation("meal");

                    b.Navigation("nutritionPlan");

                    b.Navigation("ClientRegisteredPlan");

                    b.Navigation("trainerPlan");

                    b.Navigation("trainerRegisteredPlan");

                    b.Navigation("workoutExercise");
                });
#pragma warning restore 612, 618
        }
    }
}
