using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IMyProfileRep
    {
        bool ChangeProfilePicture(UploadImageModel obj);

        bool ChangeCover(UploadImageModel obj);

        bool UploadGalleryImage(UploadImageModel obj);

        MyProfileViewModel1 GetMyProfile1(string Id);

        MyProfileViewModel2 GetMyProfile2(string Id);

        List<PostsViewModel> GetMyPosts(string Id);

        MyProfileViewModel GetMyProfile(string Id);

        bool UpdateAccountSetting(AccountSettingModel obj);

        bool UpdatePassword(UpdatePasswordModel obj);

        bool UpdateContactInfo(ContactInfoModel obj);

        bool UpdateLocationInfo(LocationInfoModel obj);

        bool UpdateLanguages(LanguagesModel obj);

        bool UpdateBio(BioModel obj);

        bool UpdateMyCertificates(EditTrainerCertificateModel obj);

        List<ProfileSocialMediaViewModel> GetMySocialMedia(string Id);
    }
}
