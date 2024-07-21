using Trainify.Entities;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface ICountryRep
    {
        List<Country> GetAllCountries();

        List<State> GetAllStates();

        List<CountryAndStatesViewModel> GetCountriesAndStates();
    }
}
