using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Numerics;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class NutritionRep : INutritionRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public NutritionRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public bool AddCustomFood(AddCustomFoodModel obj)
        {
            Food res = new Food();
            res.Name = obj.Name;
            res.Calories = obj.Calories;
            res.Protein = obj.Protein;
            res.Carb = obj.Carb;
            res.Fats = obj.Fats;
            res.ServingSize = obj.ServingSize;
            res.FoodCategoryId = obj.FoodCategoryId;
            if (obj.Image.Length > 0)
            {
                var ImageStream = new MemoryStream();
                int x = ImageStream.Capacity;
                obj.Image.CopyTo(ImageStream);
                ImageStream.Position = 0;
                byte[] ImageBytes = ImageStream.ToArray();
                res.ThumbnailData = ImageBytes;
                res.ThumbnailContentType = obj.Image.ContentType;
                res.ThumbnailExtension = obj.Image.FileName.Split('.')[obj.Image.FileName.Split('.').Length - 1];
                ImageStream.Dispose();
            }
            db.food.Add(res);
            return true;
        }

        public async Task<bool> AddNutritionPlan(AddNutritionPlanModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            var strategy = db.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (IDbContextTransaction transaction = await db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        NutritionPlan N = new NutritionPlan
                        {
                            Name = obj.Name,
                            Description = obj.Description,
                            TrainerId = obj.TrainerId,
                            Created = now,
                            Modified = now,
                            IsDraft = obj.IsDraft
                        };
                        db.nutritionPlan.Add(N);
                        db.SaveChanges();

                        foreach (var meal in obj.nutritionPlanMeal)
                        {
                            NutritionPlanMeal npm = new NutritionPlanMeal
                            {
                                Name = meal.Name,
                                NutritionPlanId = N.Id
                            };
                            db.nutritionPlanMeal.Add(npm);
                            db.SaveChanges();

                            if (npm.Id == 0)
                            {
                                throw new Exception("Failed to generate Id for NutritionPlanMeal.");
                            }

                            foreach (var food in meal.nutritionPlanMealFood)
                            {
                                Food m = db.food.Find(food.FoodId);

                                if (m != null)
                                {
                                    NutritionPlanMealFood npmf = new NutritionPlanMealFood
                                    {
                                        NutritionPlanMealId = npm.Id,
                                        FoodId = m.Id,
                                        Quantity = food.Quantity
                                    };
                                    db.nutritionPlanMealFood.Add(npmf);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    throw new Exception($"Food item with id {food.FoodId} not found.");
                                }



                            }


                        }
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        var x = ex.Message;
                        throw new Exception("Nutrition Errors", ex);
                    }
                }

            });
            return false;



        }

        public bool AssignNutritionForClient(AssignNutritionPlanModel obj)
        {
            NutritionPlan plan = db.nutritionPlan.Where(a => a.Token == obj.NutritionPlanToken).First();
            if (plan.ClientId == "" || plan.ClientId == null)
            {
                plan.ClientId = obj.ClientId;
                plan.Start = obj.Start;
                plan.End = obj.End;
                db.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool DeletePlan(string Token, string TrainerId)
        {
            NutritionPlan res = db.nutritionPlan.Where(a => a.Token == Token && a.TrainerId == TrainerId).First();
            res.IsDeleted = true;
            db.SaveChanges();
            return true;
        }

        public async Task<bool> DuplicateNutritionPlan(string NutritionToken)
        {
            DateTime now = ti.GetCurrentTime();
            var strategy = db.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (IDbContextTransaction transaction = await db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Get the old nutrition plan
                        var oldPlan = db.nutritionPlan.AsNoTracking().First(a => a.Token == NutritionToken);

                        // Create the new nutrition plan
                        var newPlan = new NutritionPlan
                        {
                            Name = oldPlan.Name + " (Copy)",
                            Description = oldPlan.Description,
                            TrainerId = oldPlan.TrainerId,
                            Created = now,
                            Modified = now
                        };
                        db.nutritionPlan.Add(newPlan);
                        db.SaveChanges();

                        // Get the old meals
                        var oldMeals = db.nutritionPlanMeal.AsNoTracking().Where(a => a.NutritionPlanId == oldPlan.Id).ToList();


                        foreach (var oldMeal in oldMeals)
                        {
                            var newMeal = new NutritionPlanMeal
                            {
                                Name = oldMeal.Name,
                                NutritionPlanId = newPlan.Id
                            };
                            db.nutritionPlanMeal.Add(newMeal);
                            db.SaveChanges();

                            // Get the old foods for each meal
                            var oldFoods = db.nutritionPlanMealFood.AsNoTracking().Where(a => a.NutritionPlanMealId == oldMeal.Id).ToList();
                            var newFoods = new List<NutritionPlanMealFood>();

                            foreach (var oldFood in oldFoods)
                            {
                                var newFood = new NutritionPlanMealFood
                                {
                                    FoodId = oldFood.FoodId,
                                    NutritionPlanMealId = newMeal.Id,
                                    Quantity = oldFood.Quantity
                                };
                                newFoods.Add(newFood);
                            }
                            db.nutritionPlanMealFood.AddRange(newFoods);
                            db.SaveChanges();
                        }



                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Duplicate Error", ex);
                    }
                }
            });

            return true;


        }

        public bool EditNutritionPlan(AddNutritionPlanModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    NutritionPlan N = db.nutritionPlan.Where(a => a.Token == obj.NutritionPlanToken).First();
                    N.Name = obj.Name;
                    N.Description = obj.Description;
                    N.Modified = now;
                    db.SaveChanges();

                    List<NutritionPlanMeal> delnpm = db.nutritionPlanMeal.Where(a => a.NutritionPlanId == N.Id).ToList();

                    foreach (var np in delnpm)
                    {
                        List<NutritionPlanMealFood> delnpmf = db.nutritionPlanMealFood.Where(a => a.NutritionPlanMealId == np.Id).ToList();
                        db.nutritionPlanMealFood.RemoveRange(delnpmf);
                    }
                    db.nutritionPlanMeal.RemoveRange(delnpm);
                    db.SaveChanges();

                    foreach (var meal in obj.nutritionPlanMeal)
                    {
                        NutritionPlanMeal npm = new NutritionPlanMeal();
                        npm.Name = meal.Name;
                        npm.NutritionPlanId = N.Id;
                        db.nutritionPlanMeal.Add(npm);
                        db.SaveChanges();

                        foreach (var food in meal.nutritionPlanMealFood)
                        {
                            Food m = db.food.Find(food.FoodId);
                            NutritionPlanMealFood npmf = new NutritionPlanMealFood();
                            npmf.NutritionPlanMealId = npm.Id;
                            npmf.FoodId = m.Id;
                            npmf.Quantity = food.Quantity;
                            db.nutritionPlanMealFood.Add(npmf);
                        }
                        db.SaveChanges();

                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var x = ex.Message;
                    return false;
                }
            }

        }

        public NutritionIdsViewModel GenerateNutrition(GenerateNutritionModel obj)
        {
        StartFunction:
            float Range = (float)1.1;

            float NeededProtein = (float)(obj.NeededCalories * 0.2) / 4;
            float NeededCarb = (float)(obj.NeededCalories * 0.45) / 4;
            float NeededFats = (float)(obj.NeededCalories * 0.35) / 9;

            float MinCalories = obj.NeededCalories / Range;
            float MaxCalories = obj.NeededCalories * Range;

            float BreakfastMinMealCalories = (float)(MinCalories * 0.3);
            float BreakfastMaxMealCalories = (float)(MaxCalories * 0.3);
            float BreakfastMinProtein = (float)(NeededProtein * 0.3) / Range;
            float BreakfastMaxProtein = (float)(NeededProtein * 0.3) * Range;
            float BreakfastMinCarb = (float)(NeededCarb * 0.3) / Range;
            float BreakfastMaxCarb = (float)(NeededCarb * 0.3) * Range;
            float BreakfastMinFats = (float)(NeededFats * 0.3) / Range;
            float BreakfastMaxFats = (float)(NeededFats * 0.3) * Range;

            float LunchMinMealCalories = (float)(MinCalories * 0.4);
            float LunchMaxMealCalories = (float)(MaxCalories * 0.4);
            float LunchMinProtein = (float)(NeededProtein * 0.4) / Range;
            float LunchMaxProtein = (float)(NeededProtein * 0.4) * Range;
            float LunchMinCarb = (float)(NeededCarb * 0.4) / Range;
            float LunchMaxCarb = (float)(NeededCarb * 0.4) * Range;
            float LunchMinFats = (float)(NeededFats * 0.4) / Range;
            float LunchMaxFats = (float)(NeededFats * 0.4) * Range;

            float DinnerMinMealCalories = (float)(MinCalories * 0.3);
            float DinnerMaxMealCalories = (float)(MaxCalories * 0.3);
            float DinnerMinProtein = (float)(NeededProtein * 0.3) / Range;
            float DinnerMaxProtein = (float)(NeededProtein * 0.3) * Range;
            float DinnerMinCarb = (float)(NeededCarb * 0.3) / Range;
            float DinnerMaxCarb = (float)(NeededCarb * 0.3) * Range;
            float DinnerMinFats = (float)(NeededFats * 0.3) / Range;
            float DinnerMaxFats = (float)(NeededFats * 0.3) * Range;

            NutritionIdsViewModel res = new NutritionIdsViewModel();
            res.Breakfast = new List<MiniFoodViewModel>();
            res.Lunch = new List<MiniFoodViewModel>();
            res.Dinner = new List<MiniFoodViewModel>();

            List<MealModel> breakfast = obj.FoodIds.Join(db.food, a => a, b => b.Id, (a, b) => new
            {
                Id = b.Id,
                Name = b.Name,
                Calories = b.Calories,
                Protein = b.Protein,
                Carb = b.Carb,
                Fats = b.Fats,
                FoodCategoryId = b.FoodCategoryId
            }).Join(db.foodMealTypes, a => a.Id, b => b.FoodId, (a, b) => new
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = b.MealTypesId
            }).Join(db.foodTags, a => a.Id, b => b.FoodId, (a, b) => new MealModel
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = a.MealTypeId,
                Tag = b.Tag
            }).Where(x => /*x.MealTypeId == 1 &&*/ x.Tag.Contains("Breakfast") && x.FoodCategoryId != 3 && x.FoodCategoryId != 6).Distinct().ToList();

            List<MealModel> lunch = obj.FoodIds.Join(db.food, a => a, b => b.Id, (a, b) => new
            {
                Id = b.Id,
                Name = b.Name,
                Calories = b.Calories,
                Protein = b.Protein,
                Carb = b.Carb,
                Fats = b.Fats,
                FoodCategoryId = b.FoodCategoryId,

            }).Join(db.foodMealTypes, a => a.Id, b => b.FoodId, (a, b) => new
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = b.MealTypesId
            }).Join(db.foodTags, a => a.Id, b => b.FoodId, (a, b) => new MealModel
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = a.MealTypeId,
                Tag = b.Tag
            }).Where(x => /*x.MealTypeId == 2 &&*/ x.Tag.Contains("Lunch") && x.FoodCategoryId != 3 && x.FoodCategoryId != 6).Distinct().ToList();

            List<MealModel> dinner = obj.FoodIds.Join(db.food, a => a, b => b.Id, (a, b) => new
            {
                Id = b.Id,
                Name = b.Name,
                Calories = b.Calories,
                Protein = b.Protein,
                Carb = b.Carb,
                Fats = b.Fats,
                FoodCategoryId = b.FoodCategoryId,

            }).Join(db.foodMealTypes, a => a.Id, b => b.FoodId, (a, b) => new
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = b.MealTypesId
            }).Join(db.foodTags, a => a.Id, b => b.FoodId, (a, b) => new MealModel
            {
                Id = a.Id,
                Name = a.Name,
                Calories = a.Calories,
                Protein = a.Protein,
                Carb = a.Carb,
                Fats = a.Fats,
                FoodCategoryId = a.FoodCategoryId,
                MealTypeId = a.MealTypeId,
                Tag = b.Tag
            }).Where(x => /*x.MealTypeId == 3 && */x.Tag.Contains("Dinner") && x.FoodCategoryId != 3 && x.FoodCategoryId != 6).Distinct().ToList();

            float LowestBreakFastCalories = (float)breakfast.Min(a => a.Calories);
            float LowestBreakfastProtein = (float)breakfast.Min(a => a.Protein);
            float LowestBreakfastCarb = (float)breakfast.Min(a => a.Carb);
            float LowestBreakfastFats = (float)breakfast.Min(a => a.Fats);

            float LowestLunchCalories = (float)lunch.Min(a => a.Calories);
            float LowestLunchProtein = (float)lunch.Min(a => a.Protein);
            float LowestLunchCarb = (float)lunch.Min(a => a.Carb);
            float LowestLunchFats = (float)lunch.Min(a => a.Fats);

            float LowestDinnerCalories = (float)dinner.Min(a => a.Calories);
            float LowestDinnerProtein = (float)dinner.Min(a => a.Protein);
            float LowestDinnerCarb = (float)dinner.Min(a => a.Carb);
            float LowestDinnerFats = (float)dinner.Min(a => a.Fats);

            var rand = new Random();

            var breakfast1list = breakfast.Where(a => a.Tag == "Breakfast1" || a.Tag == "Breakfast3" || a.Tag == "Breakfast5").Where(a => a.Calories <= BreakfastMaxMealCalories && a.Protein <= BreakfastMaxProtein && a.Carb <= BreakfastMaxCarb && a.Fats <= BreakfastMaxFats).ToList();
            var breakfast1meal = breakfast1list.ToList()[rand.Next(breakfast1list.Count)];

            MiniFoodViewModel BreakfastObj = new MiniFoodViewModel();
            BreakfastObj.FoodId = breakfast1meal.Id;
            BreakfastObj.Count = 2;
            res.Breakfast.Add(BreakfastObj);


            var RestBreakfastCalories = BreakfastMaxMealCalories - breakfast1meal.Calories;
            var RestBreakfastProtein = BreakfastMaxProtein - breakfast1meal.Protein;
            var RestBreakfastCarb = BreakfastMaxCarb - breakfast1meal.Carb;
            var RestBreakfastFats = BreakfastMaxFats - breakfast1meal.Fats;

            var lunch1list = lunch.Where(a => a.Tag == "Lunch1" || a.Tag == "Lunch3" || a.Tag == "Lunch5").Where(a => a.Calories <= LunchMaxMealCalories && a.Protein <= LunchMaxProtein && a.Carb <= LunchMaxCarb && a.Fats <= LunchMaxFats).ToList();
            var lunch1meal = lunch1list.ToList()[rand.Next(lunch1list.Count)];
            MiniFoodViewModel LunchObj = new MiniFoodViewModel();
            LunchObj.FoodId = lunch1meal.Id;
            LunchObj.Count = 1;
            res.Lunch.Add(LunchObj);

            var RestLunchCalories = LunchMaxMealCalories - lunch1meal.Calories;
            var RestLunchProtein = LunchMaxProtein - lunch1meal.Protein;
            var RestLunchCarb = LunchMaxCarb - lunch1meal.Carb;
            var RestLunchFats = LunchMaxFats - lunch1meal.Fats;



            var dinner1list = dinner.Where(a => a.Tag == "Dinner1" || a.Tag == "Dinner3" || a.Tag == "Dinner5").Where(a => a.Calories <= DinnerMaxMealCalories && a.Protein <= DinnerMaxProtein && a.Carb <= DinnerMaxCarb && a.Fats <= DinnerMaxFats).ToList();
            var dinner1meal = dinner1list.ToList()[rand.Next(dinner1list.Count)];
            MiniFoodViewModel DinnerObj = new MiniFoodViewModel();
            DinnerObj.FoodId = dinner1meal.Id;
            DinnerObj.Count = 1;
            res.Dinner.Add(DinnerObj);

            var RestDinnerCalories = DinnerMaxMealCalories - dinner1meal.Calories;
            var RestDinnerProtein = DinnerMaxProtein - dinner1meal.Protein;
            var RestDinnerCarb = DinnerMaxCarb - dinner1meal.Carb;
            var RestDinnerFats = DinnerMaxFats - dinner1meal.Fats;

        GenerateBreakFastMeal:

            if (res.Breakfast.Count > 2)
            {
                float Breakfast1Calories = (float)breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Calories;
                float Breakfast1Protein = (float)breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Protein;
                float Breakfast1Carb = (float)breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Carb;
                float Breakfast1Fats = (float)breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Fats;
                if (Breakfast1Calories <= RestBreakfastCalories && Breakfast1Protein <= RestBreakfastProtein && Breakfast1Carb <= RestBreakfastCarb && Breakfast1Fats <= RestBreakfastFats)
                {
                    res.Breakfast[0].Count = res.Breakfast[0].Count + 1;
                    RestBreakfastCalories = RestBreakfastCalories - breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Calories;
                    RestBreakfastProtein = RestBreakfastProtein - breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Protein;
                    RestBreakfastCarb = RestBreakfastCarb - breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Carb;
                    RestBreakfastFats = RestBreakfastFats - breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Fats;
                    if (RestBreakfastCalories > LowestBreakFastCalories && RestBreakfastProtein > LowestBreakfastProtein && RestBreakfastCarb > LowestBreakfastCarb && RestBreakfastFats > LowestBreakfastFats)
                    {
                        goto GenerateBreakFastMeal;
                    }
                    else
                    {
                        RestLunchCalories = RestLunchCalories + RestBreakfastCalories;
                        RestLunchProtein = RestLunchProtein + RestBreakfastProtein;
                        RestLunchCarb = RestLunchCarb + RestBreakfastCarb;
                        RestLunchFats = RestLunchFats + RestBreakfastFats;
                    }
                }
                else
                {
                    float Breakfast2Calories = (float)breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Calories;
                    float Breakfast2Protein = (float)breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Protein;
                    float Breakfast2Carb = (float)breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Carb;
                    float Breakfast2Fats = (float)breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Fats;
                    if (Breakfast2Calories <= RestBreakfastCalories && Breakfast2Protein <= RestBreakfastProtein && Breakfast2Carb <= RestBreakfastCarb && Breakfast2Fats <= RestBreakfastFats)
                    {
                        res.Breakfast[1].Count = res.Breakfast[1].Count + 1;
                        RestBreakfastCalories = RestBreakfastCalories - breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Calories;
                        RestBreakfastProtein = RestBreakfastProtein - breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Protein;
                        RestBreakfastCarb = RestBreakfastCarb - breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Carb;
                        RestBreakfastFats = RestBreakfastFats - breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Fats;
                        if (RestBreakfastCalories > LowestBreakFastCalories && RestBreakfastProtein > LowestBreakfastProtein && RestBreakfastCarb > LowestBreakfastCarb && RestBreakfastFats > LowestBreakfastFats)
                        {
                            goto GenerateBreakFastMeal;
                        }
                        else
                        {
                            RestLunchCalories = RestLunchCalories + RestBreakfastCalories;
                            RestLunchProtein = RestLunchProtein + RestBreakfastProtein;
                            RestLunchCarb = RestLunchCarb + RestBreakfastCarb;
                            RestLunchFats = RestLunchFats + RestBreakfastFats;
                        }
                    }
                    else
                    {
                        float Breakfast3Calories = (float)breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Calories;
                        float Breakfast3Protein = (float)breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Protein;
                        float Breakfast3Carb = (float)breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Carb;
                        float Breakfast3Fats = (float)breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Fats;
                        if (Breakfast3Calories <= RestBreakfastCalories && Breakfast3Protein <= RestBreakfastProtein && Breakfast3Carb <= RestBreakfastCarb && Breakfast3Fats <= RestBreakfastFats)
                        {
                            res.Breakfast[2].Count = res.Breakfast[2].Count + 1;
                            RestBreakfastCalories = RestBreakfastCalories - breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Calories;
                            RestBreakfastProtein = RestBreakfastProtein - breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Protein;
                            RestBreakfastCarb = RestBreakfastCarb - breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Carb;
                            RestBreakfastFats = RestBreakfastFats - breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Fats;
                            if (RestBreakfastCalories > LowestBreakFastCalories && RestBreakfastProtein > LowestBreakfastProtein && RestBreakfastCarb > LowestBreakfastCarb && RestBreakfastFats > LowestBreakfastFats)
                            {
                                goto GenerateBreakFastMeal;
                            }
                            else
                            {
                                RestLunchCalories = RestLunchCalories + RestBreakfastCalories;
                                RestLunchProtein = RestLunchProtein + RestBreakfastProtein;
                                RestLunchCarb = RestLunchCarb + RestBreakfastCarb;
                                RestLunchFats = RestLunchFats + RestBreakfastFats;
                            }
                        }
                        else
                        {
                            RestLunchCalories = RestLunchCalories + RestBreakfastCalories;
                            RestLunchProtein = RestLunchProtein + RestBreakfastProtein;
                            RestLunchCarb = RestLunchCarb + RestBreakfastCarb;
                            RestLunchFats = RestLunchFats + RestBreakfastFats;
                        }

                    }
                }
            }

            else
            {
                List<MealModel> breakfast2list = new List<MealModel>();
                switch (breakfast1meal.Tag)
                {
                    case "Breakfast1":
                        breakfast2list = breakfast.Where(a => a.Tag == "Breakfast2" && a.Calories <= RestBreakfastCalories && a.Protein <= RestBreakfastProtein && a.Carb <= RestBreakfastCarb && a.Fats <= RestBreakfastFats).ToList();
                        break;
                    case "Breakfast3":
                        breakfast2list = breakfast.Where(a => a.Tag == "Breakfast4" && a.Calories <= RestBreakfastCalories && a.Protein <= RestBreakfastProtein && a.Carb <= RestBreakfastCarb && a.Fats <= RestBreakfastFats).ToList();
                        break;
                    case "Breakfast5":
                        breakfast2list = breakfast.Where(a => a.Tag == "Breakfast6" && a.Calories <= RestBreakfastCalories && a.Protein <= RestBreakfastProtein && a.Carb <= RestBreakfastCarb && a.Fats <= RestBreakfastFats).ToList();
                        break;
                }

                if (breakfast2list.Count > 0)
                {
                    var breakfast2meal = breakfast2list.ToList()[rand.Next(breakfast2list.Count)];
                    MiniFoodViewModel BreakfastObj2 = new MiniFoodViewModel();
                    BreakfastObj2.FoodId = breakfast2meal.Id;
                    BreakfastObj2.Count = 1;
                    res.Breakfast.Add(BreakfastObj2);

                    RestBreakfastCalories = RestBreakfastCalories - breakfast2meal.Calories;
                    RestBreakfastProtein = RestBreakfastProtein - breakfast2meal.Protein;
                    RestBreakfastCarb = RestBreakfastCarb - breakfast2meal.Carb;
                    RestBreakfastFats = RestBreakfastFats - breakfast2meal.Fats;

                    for (int i = 1; i <= 10; i++)
                    {
                        var breakfast1obj = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First();
                        if (RestBreakfastCalories >= breakfast1obj.Calories && RestBreakfastProtein >= breakfast1obj.Protein && RestBreakfastCarb >= breakfast1obj.Carb && RestBreakfastFats >= breakfast1obj.Fats)
                        {
                            res.Breakfast[0].Count = res.Breakfast[0].Count + 1;
                            RestBreakfastCalories = RestBreakfastCalories - breakfast1obj.Calories;
                            RestBreakfastProtein = RestBreakfastProtein - breakfast1obj.Protein;
                            RestBreakfastCarb = RestBreakfastCarb - breakfast1obj.Carb;
                            RestBreakfastFats = RestBreakfastFats - breakfast1obj.Fats;
                            goto GenerateBreakFastMeal;
                        }
                        else
                        {
                            var breakfast2obj = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First();
                            if (RestBreakfastCalories >= breakfast2obj.Calories && RestBreakfastProtein >= breakfast2obj.Protein && RestBreakfastCarb >= breakfast2obj.Carb && RestBreakfastFats >= breakfast2obj.Fats)
                            {
                                res.Breakfast[1].Count = res.Breakfast[1].Count + 1;
                                RestBreakfastCalories = RestBreakfastCalories - breakfast2obj.Calories;
                                RestBreakfastProtein = RestBreakfastProtein - breakfast2obj.Protein;
                                RestBreakfastCarb = RestBreakfastCarb - breakfast2obj.Carb;
                                RestBreakfastFats = RestBreakfastFats - breakfast2obj.Fats;
                                goto GenerateBreakFastMeal;
                            }
                            else
                            {

                                if (res.Breakfast.Count > 2)
                                {
                                    var breakfast3obj = breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First();
                                    if (RestBreakfastCalories >= breakfast3obj.Calories && RestBreakfastProtein >= breakfast3obj.Protein && RestBreakfastCarb >= breakfast3obj.Carb && RestBreakfastFats >= breakfast3obj.Fats)
                                    {
                                        res.Breakfast[2].Count = res.Breakfast[2].Count + 1;
                                        RestBreakfastCalories = RestBreakfastCalories - breakfast3obj.Calories;
                                        RestBreakfastProtein = RestBreakfastProtein - breakfast3obj.Protein;
                                        RestBreakfastCarb = RestBreakfastCarb - breakfast3obj.Carb;
                                        RestBreakfastFats = RestBreakfastFats - breakfast3obj.Fats;
                                        goto GenerateBreakFastMeal;
                                    }
                                    else
                                    {
                                        goto GenerateBreakFastMeal;

                                    }
                                }
                                else
                                {
                                    goto GenerateBreakFastMeal;
                                }



                            }
                        }

                    }

                    if (RestBreakfastCalories >= LowestBreakFastCalories && RestBreakfastProtein >= LowestBreakfastProtein && RestBreakfastCarb >= LowestBreakfastCarb && RestBreakfastFats >= LowestBreakfastFats)
                    {
                        goto GenerateBreakFastMeal;
                    }
                    else
                    {
                        RestLunchCalories = RestLunchCalories + RestBreakfastCalories;
                        RestLunchProtein = RestLunchProtein + RestBreakfastProtein;
                        RestLunchCarb = RestLunchCarb + RestBreakfastCarb;
                        RestLunchFats = RestLunchFats + RestBreakfastFats;
                    }
                }

                else
                {

                    for (int i = 1; i <= 10; i++)
                    {
                        var breakfast1obj = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First();
                        if (RestBreakfastCalories >= breakfast1obj.Calories && RestBreakfastProtein >= breakfast1obj.Protein && RestBreakfastCarb >= breakfast1obj.Carb && RestBreakfastFats >= breakfast1obj.Fats)
                        {
                            res.Breakfast[0].Count = res.Breakfast[0].Count + 1;
                            RestBreakfastCalories = RestBreakfastCalories - breakfast1obj.Calories;
                            RestBreakfastProtein = RestBreakfastProtein - breakfast1obj.Protein;
                            RestBreakfastCarb = RestBreakfastCarb - breakfast1obj.Carb;
                            RestBreakfastFats = RestBreakfastFats - breakfast1obj.Fats;

                        }


                        if (res.Breakfast.Count == 2)
                        {
                            var breakfast2obj = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First();
                            if (RestBreakfastCalories >= breakfast2obj.Calories && RestBreakfastProtein >= breakfast2obj.Protein && RestBreakfastCarb >= breakfast2obj.Carb && RestBreakfastFats >= breakfast2obj.Fats)
                            {
                                res.Breakfast[1].Count = res.Breakfast[1].Count + 1;
                                RestBreakfastCalories = RestBreakfastCalories - breakfast2obj.Calories;
                                RestBreakfastProtein = RestBreakfastProtein - breakfast2obj.Protein;
                                RestBreakfastCarb = RestBreakfastCarb - breakfast2obj.Carb;
                                RestBreakfastFats = RestBreakfastFats - breakfast2obj.Fats;
                            }
                        }
                    }




                }

            }



        GenerateLunchMeal:
            if (res.Lunch.Count > 2)
            {
                float Lunch1Calories = (float)lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Calories;
                float Lunch1Protein = (float)lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Protein;
                float Lunch1Carb = (float)lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Carb;
                float Lunch1Fats = (float)lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Fats;
                if (Lunch1Calories <= RestLunchCalories && Lunch1Protein <= RestLunchProtein && Lunch1Carb <= RestLunchCarb && Lunch1Fats <= RestLunchFats)
                {
                    res.Lunch[0].Count = res.Lunch[0].Count + 1;
                    RestLunchCalories = RestLunchCalories - lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Calories;
                    RestLunchProtein = RestLunchProtein - lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Protein;
                    RestLunchCarb = RestLunchCarb - lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Carb;
                    RestLunchFats = RestLunchFats - lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Fats;
                    if (RestLunchCalories > LowestLunchCalories && RestLunchProtein > LowestLunchProtein && RestLunchCarb > LowestLunchCarb && RestLunchFats > LowestLunchFats)
                    {
                        goto GenerateLunchMeal;
                    }
                }
                else
                {
                    float Lunch2Calories = (float)lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Calories;
                    float Lunch2Protein = (float)lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Protein;
                    float Lunch2Carb = (float)lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Carb;
                    float Lunch2Fats = (float)lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Fats;
                    if (Lunch2Calories <= RestLunchCalories && Lunch2Protein <= RestLunchProtein && Lunch2Carb <= RestLunchCarb && Lunch2Fats <= RestLunchFats)
                    {
                        res.Lunch[1].Count = res.Lunch[1].Count + 1;
                        RestLunchCalories = RestLunchCalories - lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Calories;
                        RestLunchProtein = RestLunchProtein - lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Protein;
                        RestLunchCarb = RestLunchCarb - lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Carb;
                        RestLunchFats = RestLunchFats - lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Fats;
                        if (RestLunchCalories > LowestLunchCalories && RestLunchProtein > LowestLunchProtein && RestLunchCarb > LowestLunchCarb && RestLunchFats > LowestLunchFats)
                        {
                            goto GenerateLunchMeal;
                        }
                    }
                    else
                    {
                        float Lunch3Calories = (float)lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Calories;
                        float Lunch3Protein = (float)lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Protein;
                        float Lunch3Carb = (float)lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Carb;
                        float Lunch3Fats = (float)lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Fats;
                        if (Lunch3Calories <= RestLunchCalories && Lunch3Protein <= RestLunchProtein && Lunch3Carb <= RestLunchCarb && Lunch3Fats <= RestLunchFats)
                        {
                            res.Lunch[2].Count = res.Lunch[2].Count + 1;
                            RestLunchCalories = RestLunchCalories - lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Calories;
                            RestLunchProtein = RestLunchProtein - lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Protein;
                            RestLunchCarb = RestLunchCarb - lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Carb;
                            RestLunchFats = RestLunchFats - lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Fats;
                            if (RestLunchCalories > LowestLunchCalories && RestLunchProtein > LowestLunchProtein && RestLunchCarb > LowestLunchCarb && RestLunchFats > LowestLunchFats)
                            {
                                goto GenerateLunchMeal;
                            }
                        }
                    }
                }
            }
            else
            {
                List<MealModel> lunch2list = new List<MealModel>();
                switch (lunch1meal.Tag)
                {
                    case "Lunch1":
                        lunch2list = lunch.Where(a => a.Tag == "Lunch2" && a.Calories <= RestLunchCalories && a.Protein <= RestLunchProtein && a.Carb <= RestLunchCarb && a.Fats <= RestLunchFats).ToList();
                        break;
                    case "Lunch3":
                        lunch2list = lunch.Where(a => a.Tag == "Lunch4" && a.Calories <= RestLunchCalories && a.Protein <= RestLunchProtein && a.Carb <= RestLunchCarb && a.Fats <= RestLunchFats).ToList();
                        break;
                    case "Lunch5":
                        lunch2list = lunch.Where(a => a.Tag == "Lunch6" && a.Calories <= RestLunchCalories && a.Protein <= RestLunchProtein && a.Carb <= RestLunchCarb && a.Fats <= RestLunchFats).ToList();
                        break;
                }
                if (lunch2list.Count > 0)
                {
                    var lunch2meal = lunch2list.ToList()[rand.Next(lunch2list.Count)];
                    MiniFoodViewModel LunchObj2 = new MiniFoodViewModel();
                    LunchObj2.FoodId = lunch2meal.Id;
                    LunchObj2.Count = 1;
                    res.Lunch.Add(LunchObj2);
                    RestLunchCalories = RestLunchCalories - lunch2meal.Calories;
                    RestLunchProtein = RestLunchProtein - lunch2meal.Protein;
                    RestLunchCarb = RestLunchCarb - lunch2meal.Carb;
                    RestLunchFats = RestLunchFats - lunch2meal.Fats;


                    for (int i = 1; i <= 10; i++)
                    {
                        var lunch1obj = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First();
                        if (RestLunchCalories >= lunch1obj.Calories && RestLunchProtein >= lunch1obj.Protein && RestLunchCarb >= lunch1obj.Carb && RestLunchFats >= lunch1obj.Fats)
                        {
                            res.Lunch[0].Count = res.Lunch[0].Count + 1;
                            RestLunchCalories = RestLunchCalories - lunch1obj.Calories;
                            RestLunchProtein = RestLunchProtein - lunch1obj.Protein;
                            RestLunchCarb = RestLunchCarb - lunch1obj.Carb;
                            RestLunchFats = RestLunchFats - lunch1obj.Fats;
                        }
                        else
                        {
                            var lunch2obj = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First();
                            if (RestLunchCalories >= lunch2obj.Calories && RestLunchProtein >= lunch2obj.Protein && RestLunchCarb >= lunch2obj.Carb && RestLunchFats >= lunch2obj.Fats)
                            {
                                res.Lunch[1].Count = res.Lunch[1].Count + 1;
                                RestLunchCalories = RestLunchCalories - lunch2obj.Calories;
                                RestLunchProtein = RestLunchProtein - lunch2obj.Protein;
                                RestLunchCarb = RestLunchCarb - lunch2obj.Carb;
                                RestLunchFats = RestLunchFats - lunch2obj.Fats;
                            }
                            else
                            {

                                if (res.Lunch.Count > 2)
                                {
                                    var lunch3obj = lunch.Where(a => a.Id == res.Lunch[2].FoodId).First();
                                    if (RestLunchCalories >= lunch3obj.Calories && RestLunchProtein >= lunch3obj.Protein && RestLunchCarb >= lunch3obj.Carb && RestLunchFats >= lunch3obj.Fats)
                                    {
                                        res.Lunch[2].Count = res.Lunch[2].Count + 1;
                                        RestLunchCalories = RestLunchCalories - lunch3obj.Calories;
                                        RestLunchProtein = RestLunchProtein - lunch3obj.Protein;
                                        RestLunchCarb = RestLunchCarb - lunch3obj.Carb;
                                        RestLunchFats = RestLunchFats - lunch3obj.Fats;
                                    }
                                    else
                                    {
                                        goto GenerateLunchMeal;

                                    }
                                }
                                else
                                {
                                    goto GenerateLunchMeal;
                                }

                            }
                        }

                    }

                    if (RestLunchCalories > LowestLunchCalories && RestLunchProtein > LowestLunchProtein && RestLunchCarb > LowestLunchCarb && RestLunchFats > LowestLunchFats)
                    {
                        goto GenerateLunchMeal;
                    }
                }
                else
                {



                    for (int i = 1; i <= 10; i++)
                    {
                        var lunch1obj = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First();
                        if (RestLunchCalories >= lunch1obj.Calories && RestLunchProtein >= lunch1obj.Protein && RestLunchCarb >= lunch1obj.Carb && RestLunchFats >= lunch1obj.Fats)
                        {
                            res.Lunch[0].Count = res.Lunch[0].Count + 1;
                            RestLunchCalories = RestLunchCalories - lunch1obj.Calories;
                            RestLunchProtein = RestLunchProtein - lunch1obj.Protein;
                            RestLunchCarb = RestLunchCarb - lunch1obj.Carb;
                            RestLunchFats = RestLunchFats - lunch1obj.Fats;

                        }


                        if (res.Lunch.Count == 2)
                        {
                            var lunch2obj = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First();
                            if (RestLunchCalories >= lunch2obj.Calories && RestLunchProtein >= lunch2obj.Protein && RestLunchCarb >= lunch2obj.Carb && RestLunchFats >= lunch2obj.Fats)
                            {
                                res.Lunch[1].Count = res.Lunch[1].Count + 1;
                                RestLunchCalories = RestLunchCalories - lunch2obj.Calories;
                                RestLunchProtein = RestLunchProtein - lunch2obj.Protein;
                                RestLunchCarb = RestLunchCarb - lunch2obj.Carb;
                                RestLunchFats = RestLunchFats - lunch2obj.Fats;
                            }
                        }


                    }

                }

            }


        GenerateDinnerMeal:

            if (res.Dinner.Count > 2)
            {
                float Dinner1Calories = (float)dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Calories;
                float Dinner1Protein = (float)dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Protein;
                float Dinner1Carb = (float)dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Carb;
                float Dinner1Fats = (float)dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Fats;
                if (Dinner1Calories <= RestDinnerCalories && Dinner1Protein <= RestDinnerProtein && Dinner1Carb <= RestDinnerCarb && Dinner1Fats <= RestDinnerFats)
                {
                    res.Dinner[0].Count = res.Dinner[0].Count + 1;
                    RestDinnerCalories = RestDinnerCalories - dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Calories;
                    RestDinnerProtein = RestDinnerProtein - dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Protein;
                    RestDinnerCarb = RestDinnerCarb - dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Carb;
                    RestDinnerFats = RestDinnerFats - dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Fats;

                    if (RestDinnerCalories > LowestDinnerCalories && RestDinnerProtein > LowestDinnerProtein && RestDinnerCarb > LowestDinnerCarb && RestDinnerFats > LowestDinnerFats)
                    {
                        goto GenerateDinnerMeal;
                    }
                }
                else
                {
                    float Dinner2Calories = (float)dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Calories;
                    float Dinner2Protein = (float)dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Protein;
                    float Dinner2Carb = (float)dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Carb;
                    float Dinner2Fats = (float)dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Fats;
                    if (Dinner2Calories <= RestDinnerCalories && Dinner2Protein <= RestDinnerProtein && Dinner2Carb <= RestDinnerCarb && Dinner2Fats <= RestDinnerFats)
                    {
                        res.Dinner[1].Count = res.Dinner[1].Count + 1;
                        RestDinnerCalories = RestDinnerCalories - dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Calories;
                        RestDinnerProtein = RestDinnerProtein - dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Protein;
                        RestDinnerCarb = RestDinnerCarb - dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Carb;
                        RestDinnerFats = RestDinnerFats - dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Fats;

                        if (RestDinnerCalories > LowestDinnerCalories && RestDinnerProtein > LowestDinnerProtein && RestDinnerCarb > LowestDinnerCarb && RestDinnerFats > LowestDinnerFats)
                        {
                            goto GenerateDinnerMeal;
                        }
                    }
                    else
                    {
                        float Dinner3Calories = (float)dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Calories;
                        float Dinner3Protein = (float)dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Protein;
                        float Dinner3Carb = (float)dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Carb;
                        float Dinner3Fats = (float)dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Fats;
                        if (Dinner3Calories <= RestDinnerCalories && Dinner3Protein <= RestDinnerProtein && Dinner3Carb <= RestDinnerCarb && Dinner3Fats <= RestDinnerFats)
                        {
                            res.Dinner[2].Count = res.Dinner[2].Count + 1;
                            RestDinnerCalories = RestDinnerCalories - dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Calories;
                            RestDinnerProtein = RestDinnerProtein - dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Protein;
                            RestDinnerCarb = RestDinnerCarb - dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Carb;
                            RestDinnerFats = RestDinnerFats - dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Fats;

                            if (RestDinnerCalories > LowestDinnerCalories && RestDinnerProtein > LowestDinnerProtein && RestDinnerCarb > LowestDinnerCarb && RestDinnerFats > LowestDinnerFats)
                            {
                                goto GenerateDinnerMeal;
                            }
                        }
                    }
                }
            }
            else
            {
                List<MealModel> dinner2list = new List<MealModel>();
                switch (dinner1meal.Tag)
                {
                    case "Dinner1":
                        dinner2list = dinner.Where(a => a.Tag == "Dinner2" && a.Calories <= RestDinnerCalories && a.Protein <= RestDinnerProtein && a.Carb <= RestDinnerCarb && a.Fats <= RestDinnerFats).ToList();
                        break;
                    case "Dinner3":
                        dinner2list = dinner.Where(a => a.Tag == "Dinner4" && a.Calories <= RestDinnerCalories && a.Protein <= RestDinnerProtein && a.Carb <= RestDinnerCarb && a.Fats <= RestDinnerFats).ToList();
                        break;
                    case "Dinner5":
                        dinner2list = dinner.Where(a => a.Tag == "Dinner6" && a.Calories <= RestDinnerCalories && a.Protein <= RestDinnerProtein && a.Carb <= RestDinnerCarb && a.Fats <= RestDinnerFats).ToList();
                        break;
                }
                if (dinner2list.Count > 0)
                {
                    var dinner2meal = dinner2list.ToList()[rand.Next(dinner2list.Count)];
                    MiniFoodViewModel DinnerObj2 = new MiniFoodViewModel();
                    DinnerObj2.FoodId = dinner2meal.Id;
                    DinnerObj2.Count = 1;
                    res.Dinner.Add(DinnerObj2);

                    RestDinnerCalories = RestDinnerCalories - dinner2meal.Calories;
                    RestDinnerProtein = RestDinnerProtein - dinner2meal.Protein;
                    RestDinnerCarb = RestDinnerCarb - dinner2meal.Carb;
                    RestDinnerFats = RestDinnerFats - dinner2meal.Fats;

                    for (int i = 1; i <= 10; i++)
                    {
                        var dinner1obj = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First();
                        if (RestDinnerCalories >= dinner1obj.Calories && RestDinnerProtein >= dinner1obj.Protein && RestDinnerCarb >= dinner1obj.Carb && RestDinnerFats >= dinner1obj.Fats)
                        {
                            res.Dinner[0].Count = res.Dinner[0].Count + 1;
                            RestDinnerCalories = RestDinnerCalories - dinner1obj.Calories;
                            RestDinnerProtein = RestDinnerProtein - dinner1obj.Protein;
                            RestDinnerCarb = RestDinnerCarb - dinner1obj.Carb;
                            RestDinnerFats = RestDinnerFats - dinner1obj.Fats;
                        }
                        else
                        {
                            var dinner2obj = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First();
                            if (RestDinnerCalories >= dinner2obj.Calories && RestDinnerProtein >= dinner2obj.Protein && RestDinnerCarb >= dinner2obj.Carb && RestDinnerFats >= dinner2obj.Fats)
                            {
                                res.Dinner[1].Count = res.Dinner[1].Count + 1;
                                RestDinnerCalories = RestDinnerCalories - dinner2obj.Calories;
                                RestDinnerProtein = RestDinnerProtein - dinner2obj.Protein;
                                RestDinnerCarb = RestDinnerCarb - dinner2obj.Carb;
                                RestDinnerFats = RestDinnerFats - dinner2obj.Fats;
                            }
                            else
                            {

                                if (res.Dinner.Count > 2)
                                {
                                    var dinner3obj = dinner.Where(a => a.Id == res.Dinner[2].FoodId).First();
                                    if (RestDinnerCalories >= dinner3obj.Calories && RestDinnerProtein >= dinner3obj.Protein && RestDinnerCarb >= dinner3obj.Carb && RestDinnerFats >= dinner3obj.Fats)
                                    {
                                        res.Dinner[2].Count = res.Dinner[2].Count + 1;
                                        RestDinnerCalories = RestDinnerCalories - dinner3obj.Calories;
                                        RestDinnerProtein = RestDinnerProtein - dinner3obj.Protein;
                                        RestDinnerCarb = RestDinnerCarb - dinner3obj.Carb;
                                        RestDinnerFats = RestDinnerFats - dinner3obj.Fats;
                                    }
                                    else
                                    {
                                        goto GenerateDinnerMeal;

                                    }
                                }
                                else
                                {
                                    goto GenerateDinnerMeal;
                                }



                            }
                        }

                    }


                    if (RestDinnerCalories > LowestDinnerCalories && RestDinnerProtein > LowestDinnerProtein && RestDinnerCarb > LowestDinnerCarb && RestDinnerFats > LowestDinnerFats)
                    {
                        goto GenerateDinnerMeal;
                    }
                }
                else
                {


                    for (int i = 1; i <= 10; i++)
                    {
                        var dinner1obj = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First();
                        if (RestDinnerCalories >= dinner1obj.Calories && RestDinnerProtein >= dinner1obj.Protein && RestDinnerCarb >= dinner1obj.Carb && RestDinnerFats >= dinner1obj.Fats)
                        {
                            res.Dinner[0].Count = res.Dinner[0].Count + 1;
                            RestDinnerCalories = RestDinnerCalories - dinner1obj.Calories;
                            RestDinnerProtein = RestDinnerProtein - dinner1obj.Protein;
                            RestDinnerCarb = RestDinnerCarb - dinner1obj.Carb;
                            RestDinnerFats = RestDinnerFats - dinner1obj.Fats;
                        }


                        if (res.Dinner.Count == 2)
                        {
                            var dinner2obj = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First();
                            if (RestDinnerCalories >= dinner2obj.Calories && RestDinnerProtein >= dinner2obj.Protein && RestDinnerCarb >= dinner2obj.Carb && RestDinnerFats >= dinner2obj.Fats)
                            {
                                res.Dinner[1].Count = res.Dinner[1].Count + 1;
                                RestDinnerCalories = RestDinnerCalories - dinner2obj.Calories;
                                RestDinnerProtein = RestDinnerProtein - dinner2obj.Protein;
                                RestDinnerCarb = RestDinnerCarb - dinner2obj.Carb;
                                RestDinnerFats = RestDinnerFats - dinner2obj.Fats;
                            }
                        }
                    }




                }
            }
            //MinCalories

            float TotalCalories = 0;

            foreach (var item in res.Breakfast)
            {
                TotalCalories = (float)(TotalCalories + breakfast.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count);
            }
            foreach (var item in res.Lunch)
            {
                TotalCalories = (float)(TotalCalories + lunch.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count);
            }
            foreach (var item in res.Dinner)
            {
                TotalCalories = (float)(TotalCalories + dinner.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count);
            }


            if (TotalCalories > MaxCalories || TotalCalories < MinCalories)
            {
                goto StartFunction;
            }
            else
            {
                return res;
            }


        }

        public List<GetFoodCategoryViewModel> GetFoodCategories()
        {
            List<FoodCategory> FC = db.foodCategory.ToList();
            List<GetFoodCategoryViewModel> res = new List<GetFoodCategoryViewModel>();
            foreach (var item in FC)
            {
                GetFoodCategoryViewModel obj = new GetFoodCategoryViewModel();
                obj.Id = item.Id;
                obj.CategoryName = item.CategoryName;
                res.Add(obj);
            }
            return res;
        }

        public List<FoodCategoryViewModel> GetFoodMenu()
        {
            var res = db.foodCategory
     .GroupJoin(
         db.food,
         fc => fc.Id,
         f => f.FoodCategoryId,
         (fc, foods) => new FoodCategoryViewModel
         {
             Id = fc.Id,
             CategoryName = fc.CategoryName,
             food = foods.Select(f => new FoodViewModel
             {
                 Id = f.Id,
                 Name = f.Name,
                 FoodCategoryId = f.FoodCategoryId,
                 Calories = f.Calories,
                 Carb = f.Carb,
                 Fats = f.Fats,
                 Protein = f.Protein,
                 mealtypes = db.foodMealTypes
                     .Where(fmt => fmt.FoodId == f.Id)
                     .Join(
                         db.mealTypes,
                         fmt => fmt.MealTypesId,
                         mt => mt.Id,
                         (fmt, mt) => mt.MealTypeName
                     )
                     .ToList()
             }).ToList()
         }
     ).ToList();

            return res;
        }

        public List<NutritionPlansViewModel> GetTrainerPlans(string UserId)
        {

            DateTime now = ti.GetCurrentTime();

            var res = db.nutritionPlan
                .Where(plan => plan.TrainerId == UserId && !plan.IsDeleted && !plan.IsDraft)
                .Select(plan => new NutritionPlansViewModel
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Description = plan.Description,
                    Created = plan.Created,
                    Modified = plan.Modified,
                    Token = plan.Token,
                    IsDraft = plan.IsDraft,
                    PlanGoal = db.weightGoal
                        .Where(wg => wg.Id == plan.WeightGoalId)
                        .Select(wg => wg.GoalName)
                        .FirstOrDefault(),
                    ClientName = plan.ClientId != null ? db.Users
                        .Where(u => u.Id == plan.ClientId)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() : null,
                    status = plan.ClientId == null ? "Unassigned" :
                             plan.Start > now ? "Scheduled" :
                             plan.End > now ? "Active" : "Ended",
                    TotalCalories = db.nutritionPlanMeal
                        .Where(meal => meal.NutritionPlanId == plan.Id)
                        .SelectMany(meal => db.nutritionPlanMealFood
                            .Where(food => food.NutritionPlanMealId == meal.Id)
                            .Join(db.food, mealFood => mealFood.FoodId, food => food.Id, (mealFood, food) => food.Calories))
                        .Sum()
                })
                .ToList();

            return res;





            //DateTime now = ti.GetCurrentTime();

            //List<NutritionPlan> np = db.nutritionPlan.Where(a => a.TrainerId == UserId && a.IsDeleted == false).ToList();
            //List<NutritionPlansViewModel> res = new List<NutritionPlansViewModel>();
            //foreach (var plan in np)
            //{
            //    NutritionPlansViewModel ress = new NutritionPlansViewModel();
            //    ress.Id = plan.Id;
            //    ress.Name = plan.Name;
            //    ress.Description = plan.Description;
            //    ress.Created = plan.Created;
            //    ress.Modified = plan.Modified;
            //    ress.Token = plan.Token;
            //    ress.TotalCalories = 0;
            //    ress.PlanGoal = db.weightGoal.Find(plan.WeightGoalId).GoalName;
            //    ress.isDraft = plan.IsDraft;
            //    if (plan.ClientId != null && plan.ClientId != "")
            //    {
            //        var user = db.Users.Find(plan.ClientId);
            //        ress.ClientName = user.FirstName + " " + user.LastName;
            //        if (plan.Start > now)
            //        {
            //            ress.status = "Scheduled";
            //        }
            //        else
            //        {
            //            if (plan.End > now)
            //            {
            //                ress.status = "Active";
            //            }
            //            else
            //            {
            //                ress.status = "Ended";
            //            }
            //        }

            //    }
            //    else
            //    {
            //        ress.status = "Unassigned";
            //    }
            //    List<NutritionPlanMeal> npm = db.nutritionPlanMeal.Where(a => a.NutritionPlanId == plan.Id).ToList();

            //    foreach (var item in npm)
            //    {
            //        List<NutritionPlanMealFood> npmf = db.nutritionPlanMealFood.Where(a => a.NutritionPlanMealId == item.Id).ToList();

            //        foreach (var food in npmf)
            //        {
            //            ress.TotalCalories = ress.TotalCalories + db.food.Where(a => a.Id == food.FoodId).FirstOrDefault().Calories;
            //        }
            //    }

            //    res.Add(ress);
            //}
            //return res;
        }


        private string GetPlanStatus(DateTime start, DateTime? end)
        {
            DateTime now = ti.GetCurrentTime();

            if (start > now)
            {
                return "Scheduled";
            }
            else
            {
                if (end == null || end > now)
                {
                    return "Active";
                }
                else
                {
                    return "Ended";
                }
            }

        }

        public SingleNutritionPlanViewModel TrainerSingleNutritionPlan(string Token)
        {

            DateTime now = ti.GetCurrentTime();

            SingleNutritionPlanViewModel res = new SingleNutritionPlanViewModel();

            var query = from np in db.nutritionPlan
                        join client in db.Users on np.ClientId equals client.Id into npClient
                        from client in npClient.DefaultIfEmpty()
                        join state in db.state on client.StateId equals state.Id into clientState
                        from state in clientState.DefaultIfEmpty()
                        join country in db.country on state.CountryId equals country.Id into stateCountry
                        from country in stateCountry.DefaultIfEmpty()
                        where np.Token == Token
                        select new
                        {
                            np,
                            client,
                            state,
                            country
                        };

            var result = query.FirstOrDefault();
            if (result == null) return null;

            var NP = result.np;
            res.Id = NP.Id;
            res.Name = NP.Name;
            res.Description = NP.Description;
            res.Token = NP.Token;
            res.StartDate = NP.Start;

            if (result.client == null)
            {
                res.status = "Unassigned";
            }
            else
            {
                var client = result.client;
                res.Client = new NutritionClientViewModel
                {
                    Id = client.Id,
                    FullName = $"{client.FirstName} {client.LastName}",
                    Location = $"{result.state.StateName}, {result.country.CountryName}"
                };
                res.status = GetPlanStatus((DateTime)NP.Start, (DateTime)NP.End);
            }

            res.TotalCalories = 0;
            res.Carb = 0;
            res.Fat = 0;
            res.Protein = 0;

            var mealsQuery = from meal in db.nutritionPlanMeal
                             join food in db.nutritionPlanMealFood on meal.Id equals food.NutritionPlanMealId
                             join foodDetails in db.food on food.FoodId equals foodDetails.Id
                             where meal.NutritionPlanId == NP.Id
                             select new
                             {
                                 meal,
                                 food,
                                 foodDetails
                             };

            var meals = mealsQuery.ToList();

            var mealGroups = meals.GroupBy(m => m.meal.Id);

            List<NutritionPlanMealViewModel> NPMRes = new List<NutritionPlanMealViewModel>();

            foreach (var mealGroup in mealGroups)
            {
                var meal = mealGroup.First().meal;
                NutritionPlanMealViewModel NPMObj = new NutritionPlanMealViewModel
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    MealCalories = 0
                };

                List<NutritionPlanMealFoodViewModel> NPMFRes = new List<NutritionPlanMealFoodViewModel>();

                foreach (var item in mealGroup)
                {
                    var food = item.foodDetails;
                    var foodItem = item.food;
                    var calories = foodItem.Quantity * food.Calories;
                    NutritionPlanMealFoodViewModel NPMFObj = new NutritionPlanMealFoodViewModel
                    {
                        Name = food.Name,
                        Quantity = foodItem.Quantity,
                        ServingSize = food.ServingSize,
                        FoodToken = food.Token,
                        Calories = calories
                    };

                    NPMObj.MealCalories += calories;
                    res.TotalCalories += calories;
                    res.Carb += foodItem.Quantity * food.Carb;
                    res.Fat += foodItem.Quantity * food.Fats;
                    res.Protein += foodItem.Quantity * food.Protein;
                    NPMFRes.Add(NPMFObj);
                }
                NPMObj.nutritionPlanMealFood = NPMFRes;
                NPMRes.Add(NPMObj);
            }

            var FatCalories = res.Fat * 9;
            var CarbCalories = res.Carb * 4;
            var ProteinCalories = res.Protein * 4;

            res.FatPercentage = FatCalories / res.TotalCalories * 100;
            res.CarbPercentage = CarbCalories / res.TotalCalories * 100;
            res.ProteinPercentage = ProteinCalories / res.TotalCalories * 100;

            res.nutritionPlanMeal = NPMRes;
            return res;

            /////////////////////////////////////////////////////////////////////////
            //DateTime now = ti.GetCurrentTime();

            //SingleNutritionPlanViewModel res = new SingleNutritionPlanViewModel();
            //NutritionPlan NP = db.nutritionPlan.First(a => a.Token == Token);
            //res.Id = NP.Id;
            //res.Name = NP.Name;
            //res.Description = NP.Description;
            //res.Token = NP.Token;
            //res.StartDate = NP.Start;

            //if (string.IsNullOrEmpty(NP.ClientId))
            //{
            //    res.status = "Unassigned";
            //}
            //else
            //{
            //    var client = db.Users.Find(NP.ClientId);
            //    res.Client = new NutritionClientViewModel
            //    {
            //        Id = client.Id,
            //        FullName = $"{client.FirstName} {client.LastName}"
            //    };
            //    var state = db.state.Find(client.StateId);
            //    var country = db.country.First(a => a.Id == state.CountryId);
            //    res.Client.Location = $"{state.StateName}, {country.CountryName}";
            //    res.status = GetPlanStatus((DateTime)NP.Start, (DateTime)NP.End);
            //}

            //res.TotalCalories = 0;
            //res.Carb = 0;
            //res.Fat = 0;
            //res.Protein = 0;

            //List<NutritionPlanMeal> NPM = db.nutritionPlanMeal.Where(a => a.NutritionPlanId == NP.Id).ToList();
            //List<NutritionPlanMealViewModel> NPMRes = new List<NutritionPlanMealViewModel>();

            //foreach (var meal in NPM)
            //{
            //    NutritionPlanMealViewModel NPMObj = new NutritionPlanMealViewModel
            //    {
            //        Id = meal.Id,
            //        Name = meal.Name,
            //        MealCalories = 0
            //    };

            //    List<NutritionPlanMealFood> NPMF = db.nutritionPlanMealFood.Where(a => a.NutritionPlanMealId == meal.Id).ToList();
            //    List<NutritionPlanMealFoodViewModel> NPMFRes = new List<NutritionPlanMealFoodViewModel>();

            //    foreach (var foodItem in NPMF)
            //    {
            //        Food food = db.food.Find(foodItem.FoodId);
            //        var calories = foodItem.Quantity * food.Calories;
            //        NutritionPlanMealFoodViewModel NPMFObj = new NutritionPlanMealFoodViewModel
            //        {
            //            Name = food.Name,
            //            Quantity = foodItem.Quantity,
            //            ServingSize = food.ServingSize,
            //            FoodToken = food.Token,
            //            Calories = calories
            //        };

            //        NPMObj.MealCalories += calories;
            //        res.TotalCalories += calories;
            //        res.Carb += foodItem.Quantity * food.Carb;
            //        res.Fat += foodItem.Quantity * food.Fats;
            //        res.Protein += foodItem.Quantity * food.Protein;
            //        NPMFRes.Add(NPMFObj);
            //    }
            //    NPMObj.nutritionPlanMealFood = NPMFRes;
            //    NPMRes.Add(NPMObj);
            //}

            //var FatCalories = res.Fat * 9;
            //var CarbCalories = res.Carb * 4;
            //var ProteinCalories = res.Protein * 4;

            //res.FatPercentage = FatCalories / res.TotalCalories * 100;
            //res.CarbPercentage = CarbCalories / res.TotalCalories * 100;
            //res.ProteinPercentage = ProteinCalories / res.TotalCalories * 100;

            //res.nutritionPlanMeal = NPMRes;
            //return res;

            ////////////////////////////////////////

            //DateTime now = ti.GetCurrentTime();

            //SingleNutritionPlanViewModel res = new SingleNutritionPlanViewModel();
            //NutritionPlan NP = db.nutritionPlan.First(a => a.Token == Token);
            //res.Id = NP.Id;
            //res.Name = NP.Name;
            //res.Description = NP.Description;
            //res.Token = NP.Token;
            //res.StartDate = NP.Start;
            //if (NP.ClientId == null || NP.ClientId == "")
            //{
            //    res.status = "Unassigned";
            //}
            //else
            //{
            //    var client = db.Users.Find(NP.ClientId);
            //    res.Client = new NutritionClientViewModel();
            //    res.Client.Id = client.Id;
            //    res.Client.FullName = client.FirstName + " " + client.LastName;
            //    var state = db.state.Find(client.StateId);
            //    var country = db.country.Where(a => a.Id == state.CountryId).First();
            //    res.Client.Location = state.StateName + ", " + country.CountryName;
            //    res.status = GetPlanStatus((DateTime)NP.Start, (DateTime)NP.End);
            //}
            //res.TotalCalories = 0;
            //res.Carb = 0;
            //res.Fat = 0;
            //res.Protein = 0;

            //List<NutritionPlanMeal> NPM = db.nutritionPlanMeal.Where(a => a.NutritionPlanId == NP.Id).ToList();
            //List<NutritionPlanMealViewModel> NPMRes = new List<NutritionPlanMealViewModel>();
            //foreach (var item in NPM)
            //{
            //    NutritionPlanMealViewModel NPMObj = new NutritionPlanMealViewModel();
            //    NPMObj.Id = item.Id;
            //    NPMObj.Name = item.Name;
            //    NPMObj.MealCalories = 0;
            //    List<NutritionPlanMealFood> NPMF = db.nutritionPlanMealFood.Where(a => a.NutritionPlanMealId == item.Id).ToList();
            //    List<NutritionPlanMealFoodViewModel> NPMFRes = new List<NutritionPlanMealFoodViewModel>();

            //    foreach (var item1 in NPMF)
            //    {
            //        NutritionPlanMealFoodViewModel NPMFObj = new NutritionPlanMealFoodViewModel();
            //        Food food = db.food.Find(item1.FoodId);
            //        NPMFObj.Name = food.Name;
            //        NPMFObj.Quantity = item1.Quantity;
            //        NPMFObj.ServingSize = food.ServingSize;
            //        NPMFObj.FoodToken = food.Token;
            //        NPMFObj.Calories = item1.Quantity * food.Calories;

            //        NPMObj.MealCalories += NPMFObj.Calories;
            //        res.TotalCalories += NPMFObj.Calories;
            //        res.Carb += (NPMFObj.Quantity * food.Carb);
            //        res.Fat += (NPMFObj.Quantity * food.Fats);
            //        res.Protein += (NPMFObj.Quantity * food.Protein);
            //        NPMFRes.Add(NPMFObj);
            //    }
            //    NPMRes.Add(NPMObj);
            //}

            //var FatCalories = res.Fat * 9;
            //var CarbCalories = res.Carb * 4;
            //var ProteinCalories = res.Protein * 4;

            //res.FatPercentage = FatCalories / res.TotalCalories;
            //res.CarbPercentage = CarbCalories / res.TotalCalories;
            //res.ProteinPercentage = ProteinCalories / res.TotalCalories;

            //return res;

        }

        public List<NutritionPlansViewModel> GetTrainerDraftPlans(string UserId)
        {
            DateTime now = ti.GetCurrentTime();

            var res = db.nutritionPlan
                .Where(plan => plan.TrainerId == UserId && !plan.IsDeleted && plan.IsDraft)
                .Select(plan => new NutritionPlansViewModel
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    Description = plan.Description,
                    Created = plan.Created,
                    Modified = plan.Modified,
                    Token = plan.Token,
                    IsDraft = plan.IsDraft,
                    PlanGoal = db.weightGoal
                        .Where(wg => wg.Id == plan.WeightGoalId)
                        .Select(wg => wg.GoalName)
                        .FirstOrDefault(),
                    ClientName = plan.ClientId != null ? db.Users
                        .Where(u => u.Id == plan.ClientId)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() : null,
                    status = plan.ClientId == null ? "Unassigned" :
                             plan.Start > now ? "Scheduled" :
                             plan.End > now ? "Active" : "Ended",
                    TotalCalories = db.nutritionPlanMeal
                        .Where(meal => meal.NutritionPlanId == plan.Id)
                        .SelectMany(meal => db.nutritionPlanMealFood
                            .Where(food => food.NutritionPlanMealId == meal.Id)
                            .Join(db.food, mealFood => mealFood.FoodId, food => food.Id, (mealFood, food) => food.Calories))
                        .Sum()
                })
                .ToList();

            return res;

        }
    }
}
