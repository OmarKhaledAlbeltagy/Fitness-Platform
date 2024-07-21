using Trainify.Models;

namespace Trainify.Repo
{
    public interface IDevRep
    {
        string GetRedirectingUrl(string FirstUrl);

        DateTime GetDateTime();


        bool AddBodyType(AddBodyTypeModel bodyType);

        bool AddActivityLevel(AddActivityLevelModel activityLevel);
    }
}
