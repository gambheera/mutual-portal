using System;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Core.Persistence;
using Mutual.Portal.Core.Entities.Common;
using System.Linq;
using Mutual.Portal.Utility.Operations;
using Mutual.Portal.Service._helpers;
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using Mutual.Portal.Utility.Constants.Messages;

namespace Mutual.Portal.Service.BusinessLogic.UserManagement
{
    public class UserManager : IUserManager
    {
        private readonly IOperationDbContext _operationContext;

        public UserManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject CheckUseravailability(UserSocialAccountProviderType userAuthenticationType, string socialId, string name, string email)
        {
            try
            {
                var userObj =  _operationContext.Set<User>().FirstOrDefault(u => u.SocialId == socialId && u.SocialAccountProvider == (int)userAuthenticationType);

                if (userObj == null)
                {
                    //var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0001", ResponseType.InternalServerError);
                    //return errObj;

                    var returnedObj = _registerWhileLogin(userAuthenticationType, socialId, name, email);
                    return returnedObj;
                }

                var obj = ResponseManager.GetSuccessResponse(UserDto.GetDto(userObj), SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                ContextHandler.FlushAttachedObjects(_operationContext);
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EX-0001", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetUserEmployeeType(string userGuid)
        {
            try
            {
                var existingEmployee = _operationContext.Set<User>().FirstOrDefault(u=>u.Guid.ToString() == userGuid);

                if (existingEmployee == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0003", ResponseType.InternalServerError);
                    return errObj;
                }

                var obj = ResponseManager.GetSuccessResponse(existingEmployee.EmploymentType, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetUserStatusByGuid(string userGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject ConfirmRegistration(UserDto userDto)
        {
            try
            {
                var existingUser = _operationContext.Set<User>().FirstOrDefault(u => u.Guid == userDto.Guid);

                if (existingUser == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0003", ResponseType.InternalServerError);
                    return errObj;
                }

                existingUser.EmploymentType = userDto.EmploymentType;
                existingUser.Code = _generateUserCode(userDto.EmploymentType, existingUser.SocialAccountProvider);
                existingUser.ContactNumber1 = userDto.ContactNumber1;
                existingUser.ContactNumber2 = userDto.ContactNumber2;
                existingUser.Name = userDto.Name;
                existingUser.IsRegistrationConfirmed = true;
                existingUser.IsEmployeeDetailesProvided = false;
                existingUser.LastLoginOn = DateTime.Now;
                
                _operationContext.Save();

                var returningDtoObj = UserDto.GetDto(existingUser);

                var obj = ResponseManager.GetSuccessResponse(returningDtoObj, SuccessMessages.USER_CREATED_SUCCESSFULLY, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }
        
        public ResponseObject GetUserInfo(string userGuid)
        {
            try
            {
                var userObj = _operationContext.Set<User>().FirstOrDefault(u => u.Guid.ToString() == userGuid);

                if (userObj == null) return ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-8897", ResponseType.InternalServerError);

                var userDtoObj = UserDto.GetDto(userObj);

                var obj = ResponseManager.GetSuccessResponse(userDtoObj, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetUserSimpleInfo(string userGuid)
        {
            try
            {
                var userObj = _operationContext.Set<User>().FirstOrDefault(u => u.Guid.ToString() == userGuid);
                if (userObj == null)
                { 
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-8897", ResponseType.InternalServerError);
                    return errObj;
                }

                var simpleInfo = new UserSimpleInfo()
                {
                    MyRemainingViewCount = userObj.MyRemainingViewCount
                };

                var obj = ResponseManager.GetSuccessResponse(simpleInfo, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        #region Private Methods

        private string _generateUserCode(int employeeType, int socialAccountType)
        {
            int maxId = _operationContext.Set<User>().Max(u => u.Id);
            var theUser = _operationContext.Set<User>().FirstOrDefault(u => u.Id == maxId);

            if (theUser == null)
            {
                return "00000000";
            }

            string maxCode = theUser.Code.Substring(2, theUser.Code.Length - 2);

            int maxNumber = int.Parse(maxCode);

            string tail = (maxNumber + 1).ToString("D6");

            string userCode = employeeType + "" + socialAccountType + tail;

            return userCode;
        }

        private ResponseObject _registerWhileLogin(UserSocialAccountProviderType userAuthenticationType, string socialId, string name, string email)
        {
            try
            {
                var obj = new User()
                {
                    Guid = Guid.NewGuid(),
                    Email = email,
                    Name = name,
                    IsActive = false,
                    IsDeleted = false,
                    LastLoginOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    IsRegistrationConfirmed = false,
                    EmploymentType = 0,
                    SocialAccountProvider = (int)userAuthenticationType,
                    SocialId = socialId
                };

                _operationContext.Set<User>().Add(obj);
                _operationContext.Save();

                return ResponseManager.GetSuccessResponse(null, SuccessMessages.USER_CREATED_SUCCESSFULLY, ResponseType.Created);
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        #endregion
    }
}
