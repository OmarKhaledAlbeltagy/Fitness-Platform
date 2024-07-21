using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Trainify.Context;
using Trainify.Privilage;
using Microsoft.EntityFrameworkCore;
using Trainify.Repo;
using Microsoft.Extensions.DependencyInjection.Extensions;
using static System.Net.Mime.MediaTypeNames;
using Xabe.FFmpeg;

namespace Trainify
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


           

            services.AddIdentity<ExtendIdentityUser, ExtendIdentityRole>(op =>
            {
                op.Password.RequiredLength = 7;
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.User.RequireUniqueEmail = true;
                op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                op.SignIn.RequireConfirmedEmail = true;
                op.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                op.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<DbContainer>();


     

            services.AddMvc(a => a.EnableEndpointRouting = false);

        
            services.AddHttpContextAccessor();

            services.AddDbContextPool<DbContainer>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("con"),a=>a.EnableRetryOnFailure());
               
    
            });


    

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("allow",
                                    a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                                  );

            });

            services.AddScoped<ITimeRep, TimeRep>();
            services.AddScoped<ICountryRep, CountryRep>();
            services.AddScoped<IUserRep, UserRep>();
            services.AddScoped<IExerciseRep, ExerciseRep>();
            services.AddScoped<IDevFuncRep, DevFuncRep>();
            services.AddScoped<IWorkoutRep, WorkoutRep>();
            services.AddScoped<IMealRep, MealRep>();
            services.AddScoped<INutritionRep, NutritionRep>();
            services.AddScoped<IMyProfileRep, MyProfileRep>();
            services.AddScoped<ISocialMediaRep, SocialMediaRep>();
            services.AddScoped<ILanguageRep, LanguageRep>();
            services.AddScoped<IPostRep, PostRep>();
            services.AddScoped<ISubscriptionRep, SubscriptionRep>();
            services.AddScoped<IBMICalcRep, BMICalcRep>();
            services.AddScoped<IDevRep, DevRep>();
            services.AddScoped<IHomeRep, HomeRep>();
            services.AddScoped<IPropertiesRep, PropertiesRep>();
            services.AddScoped<IAuthRep, AuthRep>();
            services.AddAuthentication();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
                        
            app.UseStaticFiles();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors();
            app.UseHsts();


        }
    }
}
