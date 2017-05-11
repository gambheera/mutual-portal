
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;

namespace Mutual.Portal.Service.BusinessLogic.UserManagement
{
    public interface IUserManager
    {
        ResponseObject CheckUseravailability(UserSocialAccountProviderType userAuthenticationType, string socialId, string name, string email);
        ResponseObject GetUserStatusByGuid(string userGuid);
        ResponseObject ConfirmRegistration(UserDto userDto);
        ResponseObject GetUserEmployeeType(string userGuid);
        ResponseObject GetUserInfo(string userGuid);
        ResponseObject GetUserSimpleInfo(string userGuid);
    }
}
