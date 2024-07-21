using Trainify.Context;
using Trainify.Entities;

namespace Trainify.Repo
{
    public class LanguageRep:ILanguageRep
    {
        private readonly DbContainer db;

        public LanguageRep(DbContainer db)
        {
            this.db = db;
        }

        public List<Language> GetAllLanguages()
        {
            return db.language.OrderBy(a=>a.LanguageName).ToList();
        }
    }
}
