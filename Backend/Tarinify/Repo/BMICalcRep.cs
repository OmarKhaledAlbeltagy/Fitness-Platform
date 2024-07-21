using Microsoft.Graph.Models;
using System.Diagnostics.Eventing.Reader;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class BMICalcRep : IBMICalcRep
    {
        private readonly DbContainer db;

        public BMICalcRep(DbContainer db)
        {
            this.db = db;
        }

        public BMIViewModel CalculateBMI(BMIModel obj)
        {
            WeightGoal wg = db.weightGoal.Find(obj.WeightGoalId);
            ActivityLevel al = db.activityLevel.Find(obj.ActivityLevelId);

            if (obj.HeightCM == 0 && obj.WeightKG == 0)
            {
                double totalheightinch = ((double)obj.HeightFT * 12) + (double)obj.HeightIN;
                obj.WeightKG = obj.WeightLB * 0.45;
                obj.HeightCM = totalheightinch * 2.54;
            }

            BMIViewModel res = new BMIViewModel();

            //Gender false = male --- True = Female
            switch (obj.Gender)
            {
                case false:
                    //88.362 + (13.397 × weight in kg) + (4.799 × height in cm) - (5.677 × age in years)
                    res.BMR = 88.362 + (13.397 * obj.WeightKG) + (4.799 * obj.HeightCM) - (5.677 * obj.Age);
                    break;
                case true:
                    //447.593 + (9.247 × weight in kg) + (3.098 × height in cm) - (4.330 × age in years)
                    res.BMR = 447.593 + (9.247 * obj.WeightKG) + (3.098 * obj.HeightCM) - (4.330 * obj.Age);
                    break;
            }

            res.BMI = obj.WeightKG / ((obj.HeightCM / 100) * (obj.HeightCM / 100));
            res.GoalName = wg.GoalName;
            res.NeededCalories = (res.BMR * al.ActivityLevelValue) + wg.GoalValue;
            return res;
        }


        public NutritionIdsViewModel GenerateNutrition(GenerateNutritionModel obj)
        {
            StartFunction:
            double Range = 1.1;

            double NeededProtein = (obj.NeededCalories * 0.2) / 4;
            double NeededCarb = (obj.NeededCalories * 0.45) / 4;
            double NeededFats = (obj.NeededCalories * 0.35) / 9;

            double MinCalories = obj.NeededCalories / Range;
            double MaxCalories = obj.NeededCalories * Range;

            double BreakfastMinMealCalories = MinCalories * 0.3;
            double BreakfastMaxMealCalories = MaxCalories * 0.3;
            double BreakfastMinProtein = (NeededProtein * 0.3) / Range;
            double BreakfastMaxProtein = (NeededProtein * 0.3) * Range;
            double BreakfastMinCarb = (NeededCarb * 0.3) / Range;
            double BreakfastMaxCarb = (NeededCarb * 0.3) * Range;
            double BreakfastMinFats = (NeededFats * 0.3) / Range;
            double BreakfastMaxFats = (NeededFats * 0.3) * Range;

            double LunchMinMealCalories = MinCalories * 0.4;
            double LunchMaxMealCalories = MaxCalories * 0.4;
            double LunchMinProtein = (NeededProtein * 0.4) / Range;
            double LunchMaxProtein = (NeededProtein * 0.4) * Range;
            double LunchMinCarb = (NeededCarb * 0.4) / Range;
            double LunchMaxCarb = (NeededCarb * 0.4) * Range;
            double LunchMinFats = (NeededFats * 0.4) / Range;
            double LunchMaxFats = (NeededFats * 0.4) * Range;

            double DinnerMinMealCalories = MinCalories * 0.3;
            double DinnerMaxMealCalories = MaxCalories * 0.3;
            double DinnerMinProtein = (NeededProtein * 0.3) / Range;
            double DinnerMaxProtein = (NeededProtein * 0.3) * Range;
            double DinnerMinCarb = (NeededCarb * 0.3) / Range;
            double DinnerMaxCarb = (NeededCarb * 0.3) * Range;
            double DinnerMinFats = (NeededFats * 0.3) / Range;
            double DinnerMaxFats = (NeededFats * 0.3) * Range;

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

            double LowestBreakFastCalories = breakfast.Min(a => a.Calories);
            double LowestBreakfastProtein = breakfast.Min(a => a.Protein);
            double LowestBreakfastCarb = breakfast.Min(a => a.Carb);
            double LowestBreakfastFats = breakfast.Min(a => a.Fats);

            double LowestLunchCalories = lunch.Min(a => a.Calories);
            double LowestLunchProtein = lunch.Min(a => a.Protein);
            double LowestLunchCarb = lunch.Min(a => a.Carb);
            double LowestLunchFats = lunch.Min(a => a.Fats);

            double LowestDinnerCalories = dinner.Min(a => a.Calories);
            double LowestDinnerProtein = dinner.Min(a => a.Protein);
            double LowestDinnerCarb = dinner.Min(a => a.Carb);
            double LowestDinnerFats = dinner.Min(a => a.Fats);

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
                double Breakfast1Calories = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Calories;
                double Breakfast1Protein = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Protein;
                double Breakfast1Carb = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Carb;
                double Breakfast1Fats = breakfast.Where(a => a.Id == res.Breakfast[0].FoodId).First().Fats;
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
                    double Breakfast2Calories = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Calories;
                    double Breakfast2Protein = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Protein;
                    double Breakfast2Carb = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Carb;
                    double Breakfast2Fats = breakfast.Where(a => a.Id == res.Breakfast[1].FoodId).First().Fats;
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
                        double Breakfast3Calories = breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Calories;
                        double Breakfast3Protein = breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Protein;
                        double Breakfast3Carb = breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Carb;
                        double Breakfast3Fats = breakfast.Where(a => a.Id == res.Breakfast[2].FoodId).First().Fats;
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
                double Lunch1Calories = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Calories;
                double Lunch1Protein = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Protein;
                double Lunch1Carb = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Carb;
                double Lunch1Fats = lunch.Where(a => a.Id == res.Lunch[0].FoodId).First().Fats;
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
                    double Lunch2Calories = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Calories;
                    double Lunch2Protein = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Protein;
                    double Lunch2Carb = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Carb;
                    double Lunch2Fats = lunch.Where(a => a.Id == res.Lunch[1].FoodId).First().Fats;
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
                        double Lunch3Calories = lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Calories;
                        double Lunch3Protein = lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Protein;
                        double Lunch3Carb = lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Carb;
                        double Lunch3Fats = lunch.Where(a => a.Id == res.Lunch[2].FoodId).First().Fats;
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
                double Dinner1Calories = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Calories;
                double Dinner1Protein = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Protein;
                double Dinner1Carb = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Carb;
                double Dinner1Fats = dinner.Where(a => a.Id == res.Dinner[0].FoodId).First().Fats;
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
                    double Dinner2Calories = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Calories;
                    double Dinner2Protein = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Protein;
                    double Dinner2Carb = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Carb;
                    double Dinner2Fats = dinner.Where(a => a.Id == res.Dinner[1].FoodId).First().Fats;
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
                        double Dinner3Calories = dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Calories;
                        double Dinner3Protein = dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Protein;
                        double Dinner3Carb = dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Carb;
                        double Dinner3Fats = dinner.Where(a => a.Id == res.Dinner[2].FoodId).First().Fats;
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

            double TotalCalories = 0;

            foreach (var item in res.Breakfast)
            {
                TotalCalories = TotalCalories + breakfast.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count;
            }
            foreach (var item in res.Lunch)
            {
                TotalCalories = TotalCalories + lunch.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count;
            }
            foreach (var item in res.Dinner)
            {
                TotalCalories = TotalCalories + dinner.Where(a => a.Id == item.FoodId).FirstOrDefault().Calories * item.Count;
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

     
        public List<FoodCategoryViewModel> GetFoodMenu()
        {
            List<FoodCategoryViewModel> res = new List<FoodCategoryViewModel>();
            List<FoodCategory> FoodCategory = db.foodCategory.ToList();



            foreach (var item in FoodCategory)
            {
                FoodCategoryViewModel FoodCategoryRes = new FoodCategoryViewModel();
                FoodCategoryRes.Id = item.Id;
                FoodCategoryRes.CategoryName = item.CategoryName;
                FoodCategoryRes.food = new List<FoodViewModel>();
                List<Food> food = db.food.Where(a => a.FoodCategoryId == item.Id).ToList();

                foreach (var item1 in food)
                {
                    FoodViewModel FoodRes = new FoodViewModel();
                    FoodRes.Id = item1.Id;
                    FoodRes.Name = item1.Name;
                    FoodRes.FoodCategoryId = item1.FoodCategoryId;
                    FoodRes.Calories = item1.Calories;
                    FoodRes.Carb = item1.Carb;
                    FoodRes.Fats = item1.Fats;
                    FoodRes.Protein = item1.Protein;
                    FoodRes.mealtypes = db.foodMealTypes.Where(a => a.FoodId == item1.Id).Join(db.mealTypes, a => a.MealTypesId, b => b.Id, (a, b) => new { mealtypename = b.MealTypeName }).Select(a => a.mealtypename).ToList();
                    FoodCategoryRes.food.Add(FoodRes);
                }
                res.Add(FoodCategoryRes);
            }


            return res;
        }
    }
}
