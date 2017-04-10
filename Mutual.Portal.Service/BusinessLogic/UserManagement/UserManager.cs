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
        private IOperationDbContext _operationContext;

        public UserManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject CheckUseravailability(UserSocialAccountProviderType userAuthenticationType, string socialId)
        {
            try
            {
                var userObj =  _operationContext.Set<User>().FirstOrDefault(u => u.SocialId == socialId && u.SocialAccountProvider == (int)userAuthenticationType);

                if (userObj == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0001", ResponseType.InternalServerError);
                    return errObj;
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

        public ResponseObject RegisterUser(UserDto userDto)
        {
            try
            {
                var existingUser = _operationContext.Set<User>().FirstOrDefault(u => u.SocialId == userDto.SocialId);

                if (existingUser != null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_ALREADY_EXIST, "ERR-0003", ResponseType.InternalServerError);
                    return errObj;
                }

                var userBo = UserDto.GetBo(userDto);
                _operationContext.Set<User>().Add(userBo);
                _operationContext.Save();

                var obj = ResponseManager.GetSuccessResponse(userDto, SuccessMessages.USER_CREATED_SUCCESSFULLY, ResponseType.Created);
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

                return ResponseManager.GetSuccessResponse(userDtoObj, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }
    }
}
