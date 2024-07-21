namespace Trainify.Entities
{
    public class Language
    {
        public int Id { get; set; }

        public string LanguageName { get; set; }

        public IEnumerable<UserLanguage> userLanguage { get; set; }
    }
}
