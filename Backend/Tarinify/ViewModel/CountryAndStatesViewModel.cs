namespace Trainify.ViewModel
{
    public class CountryAndStatesViewModel
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public string Emoji { get; set; }

        public List<StatesViewModel>? states { get; set; }
    }
}
