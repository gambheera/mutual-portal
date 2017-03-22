using System;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Core.Persistence;

namespace Mutual.Portal.Service.BusinessLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private IOperationDbContext _operationContext;

        public UserManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject CheckUseravailability(UserAuthenticationType userAuthenticationType, string socialId)
        {
            throw new NotImplementedException();
        }


        public ResponseObject GetUserStatusByGuid(string userGuid)
        {
            throw new NotImplementedException();
        }
    }
}
