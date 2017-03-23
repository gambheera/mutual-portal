
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;

namespace Mutual.Portal.Service.BusinessLogic.UserManagement
{
    public interface IUserManager
    {
        ResponseObject CheckUseravailability(UserSocialAccountProviderType userAuthenticationType, string socialId);
        ResponseObject GetUserStatusByGuid(string userGuid);
        
    }
}
