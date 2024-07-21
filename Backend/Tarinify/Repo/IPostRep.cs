using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IPostRep
    {
        ReturnPostViewModel AddPost(AddPostModel obj);

        bool DeleteMyPost(string UserId, string Token);
    }
}
