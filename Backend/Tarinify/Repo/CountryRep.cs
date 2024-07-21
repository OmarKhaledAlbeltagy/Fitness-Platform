using Trainify.Migrations;
using Trainify.Context;
using Trainify.Entities;
using Trainify.ViewModel;
namespace Trainify.Repo
{
    public class CountryRep : ICountryRep
    {

        private readonly DbContainer db;
        public CountryRep(DbContainer db)
        {
            this.db = db;
        }

        public List<Country> GetAllCountries()
        {
           return db.country.ToList();
        }

        public List<State> GetAllStates()
        {
            List<State> res = db.state.ToList();
            return res;
        }

        public List<CountryAndStatesViewModel> GetCountriesAndStates()
        {
            var countriesAndStates = db.country
                .Select(country => new CountryAndStatesViewModel
                {
                    Id = country.Id,
                    CountryName = country.CountryName,
                    Emoji = country.Emoji,
                    states = db.state.Where(state => state.CountryId == country.Id)
                                     .Select(state => new StatesViewModel
                                     {
                                         Id = state.Id,
                                         StateName = state.StateName,
                                         CountryId = state.CountryId
                                     })
                                     .ToList()
                })
                .ToList();

            return countriesAndStates;
        }
    }
}
