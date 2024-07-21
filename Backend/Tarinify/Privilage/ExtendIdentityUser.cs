using Microsoft.AspNetCore.Identity;
using Trainify.Entities;

namespace Trainify.Privilage
{
    public class ExtendIdentityUser:IdentityUser
    {
        public IEnumerable<Exercise> exercise { get; set; }

        public IEnumerable<WorkoutExercise> workoutExercise { get; set; }

        public IEnumerable<TrainerPlan> trainerPlan { get; set; }

        public IEnumerable<ClientRegisteredPlan> ClientRegisteredPlan { get; set; }

        public IEnumerable<TrainerRegisteredPlan> trainerRegisteredPlan { get; set; }

        public IEnumerable<UserLanguage> userLanguage { get; set; }

        public IEnumerable<UserGallery> userGallery { get; set; }

        public IEnumerable<TrainerCertificate> certificates { get; set; }

        public int? StateId { get; set; }

        public State state { get; set; }

        public int? PhoneCodeId { get; set; }

        public Country PhoneCode { get; set; }

        public int? TrainerTitleId { get; set; }

        public TrainerTitle trainerTitle { get; set; }

        public string? Address { get; set; }

        public byte[]? ProfilePictureData { get; set; }

        public string? ProfilePictureExtension { get; set; }

        public string? ProfilePictureContentType { get; set; }

        public byte[]? CoverData { get; set; }

        public string? CoverExtension { get; set; }

        public string? CoverContentType { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? PostalCode { get; set; }

        public string? About { get; set; }

        public string? ShortBio { get; set; }

        public DateTime Birthday { get; set; }

        public int? WeightGoalId { get; set; }

        public WeightGoal weightGoal { get; set; }

        public double? WeightKG { get; set; }

        public double? HeightCM { get; set; }

        public bool Gender { get; set; }

        public int NeededCalories { get; set; }

        public int? BodyTypeId { get; set; }

        public BodyType bodyType { get; set; }

        public int? ActivityLevelId { get; set; }

        public ActivityLevel activityLevel { get; set; }


    }
}
