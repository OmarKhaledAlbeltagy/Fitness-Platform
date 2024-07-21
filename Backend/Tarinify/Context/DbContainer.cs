using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Trainify.Entities;
using Trainify.Privilage;
namespace Trainify.Context
{
    public class DbContainer : IdentityDbContext<ExtendIdentityUser, ExtendIdentityRole, string>
    {
        public DbContainer(DbContextOptions<DbContainer> ops) : base(ops)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NutritionPlanMeal>()
            .HasKey(a => a.Id);

            builder.Entity<NutritionPlanMeal>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<NutritionPlan>()
           .HasOne(n => n.Trainer)
           .WithMany()  // Assuming that a trainer can have multiple nutrition plans
           .HasForeignKey(n => n.TrainerId)
           .OnDelete(DeleteBehavior.Restrict);  // Or any other delete behavior you need

            // Configure relationship between NutritionPlan and ExtendIdentityUser for Client
            builder.Entity<NutritionPlan>()
                .HasOne(n => n.Client)
                .WithMany()  // Assuming that a client can have multiple nutrition plans
                .HasForeignKey(n => n.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<Exercise> exersice { get; set; }

        public DbSet<Food> food { get; set; }

        public DbSet<NutritionPlan> nutritionPlan { get; set; }
        
        public DbSet<NutritionPlanMeal> nutritionPlanMeal { get; set; }

        public DbSet<Workout> workout { get; set; }

        public DbSet<WorkoutExercise> workoutExercise { get; set; }

        public DbSet<PlatformPlan> platformPlan { get; set; }

        public DbSet<TrainerPlan> trainerPlan { get; set; }

        public DbSet<ClientRegisteredPlan> ClientRegisteredPlan { get; set; }

        public DbSet<TrainerRegisteredPlan> trainerRegisteredPlan { get; set; }

        public DbSet<Country> country { get; set; }

        public DbSet<State> state { get; set; }

        public DbSet<UserPost> post { get; set; }

        public DbSet<ClientInvoice> ClientInvoice { get; set; }

        public DbSet<TrainerInvoice> trainerInvoice { get; set; }

        public DbSet<ClientWorkout> ClientWorkout { get; set; }

        public DbSet<NutritionPlanMealFood> nutritionPlanMealFood { get; set; }

        public DbSet<Follow> follow { get; set; }

        public DbSet<Language> language { get; set; }

        public DbSet<UserLanguage> userLanguage { get; set; }

        public DbSet<SocialMedia> socialMedia { get; set; }

        public DbSet<UserSocialMedia> userSocialMedia { get; set; }

        public DbSet<UserGallery> userGallery { get; set; }

        public DbSet<TrainerCertificate> trainerCertificate { get; set; }

        public DbSet<FoodCategory> foodCategory { get; set; }

        public DbSet<FoodTags> foodTags { get; set; }

        public DbSet<FoodMealTypes> foodMealTypes { get; set; }

        public DbSet<MealTypes> mealTypes { get; set; }

        public DbSet<ActivityLevel> activityLevel { get; set; }

        public DbSet<BodyType> bodyType { get; set; }

        public DbSet<WeightGoal> weightGoal { get; set; }

        public DbSet<TrainerTitle> trainerTitle { get; set; }

        public DbSet<NotCompletedSignup> notCompletedSignup { get; set; }
    }
}
