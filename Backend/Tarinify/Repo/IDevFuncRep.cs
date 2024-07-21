using Microsoft.AspNetCore.Http;

namespace Trainify.Repo
{
    public interface IDevFuncRep
    {
        bool IsVideo(IFormFile file);

        void ConvertVideoToGif(IFormFile file);

        FormFile CreateEmptyFile();



    }
}
