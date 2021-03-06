﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Core.Entities.Nursing;
using Mutual.Portal.Core.Persistence;
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using Mutual.Portal.Utility.Constants.Messages;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Operations;

namespace Mutual.Portal.Service.BusinessLogic.NurseManagement
{
    public class NurseManager : INurseManager
    {
        private IOperationDbContext _operationContext;

        public NurseManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject GetHospitalList()
        {
            try
            {
                var hospitalBoList = _operationContext.Set<Hospital>().ToList();
                var hospitalDtoList = hospitalBoList.Select(HospitalDto.GetDto).ToList();

                var obj = ResponseManager.GetSuccessResponse(hospitalDtoList, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetIndividualNurse(string requesteeGuid, string requesterGuid)
        {
            try
            {
                if (requesteeGuid.Equals(requesterGuid))
                {
                    // this request is asking my details
                    var returnedObj = _getMyNursingDetails(requesterGuid);
                    return returnedObj;
                }

                // Now a request from another party.
                return null; // TODO: implement the logic here.
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetNurseListByCurrentHospital(int hospitalId, string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject GetNurseListByDreamHospital(int hospitalId, string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject SaveNurse(NurseDto nurseDto, string userGuid)
        {
            try
            {
                var existingNurse = _operationContext.Set<Nurse>()
                    .Include(n=>n.User)
                    .FirstOrDefault(n => n.User.Guid.ToString() == userGuid);

                var existingUser = _operationContext.Set<User>().FirstOrDefault(u => u.Guid.ToString() == userGuid);

                if (existingUser == null)
                {
                    // this operation is invalid. send a 500 response
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                if (existingNurse == null)
                {
                    // This is a new user needs to insert the details and return
                   var savedObj = _internalNurseSave(nurseDto, userGuid);
                    return savedObj;
                }

                // Time to edit details
                var editedObj = _intrenalNurseEdit(nurseDto, userGuid);
                return editedObj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject SearchNurses(int currentHospitalId, int dreamHospitalId, int pageNumber)
        {
            try
            {
                if (pageNumber < 0)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.INVALID__PAGE_NUMBER, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                List<Nurse> filteredNurseBoList;

                const int numberOfItems = 20;
                int startPosition = numberOfItems*(pageNumber - 1);
                

                if (currentHospitalId == 0 && dreamHospitalId == 0)
                {
                    filteredNurseBoList = _operationContext.Set<Nurse>()
                        .Include(n => n.User)
                        .Include(n => n.Hospital)
                        .Include(n => n.DreamHospitalList)
                        .OrderByDescending(n => n.User.LastLoginOn)
                        .Skip(startPosition).Take(numberOfItems)
                        .ToList();
                }else if (currentHospitalId == 0 && dreamHospitalId != 0)
                {
                    filteredNurseBoList = _operationContext.Set<Nurse>()
                        .Include(n => n.User)
                        .Include(n => n.Hospital)
                        .Include(n => n.DreamHospitalList)
                        .Where(n => n.DreamHospitalList.Any(d => d.Hospital.Id == dreamHospitalId))
                        .OrderByDescending(n => n.User.LastLoginOn)
                        .Skip(startPosition).Take(numberOfItems)
                        .ToList();
                }else if (currentHospitalId != 0 && dreamHospitalId == 0)
                {
                    filteredNurseBoList = _operationContext.Set<Nurse>()
                        .Include(n => n.User)
                        .Include(n => n.Hospital)
                        .Include(n => n.DreamHospitalList)
                        .Where(n => n.Hospital.Id == currentHospitalId)
                        .OrderByDescending(n => n.User.LastLoginOn)
                        .Skip(startPosition).Take(numberOfItems)
                        .ToList();
                }
                else
                {
                    filteredNurseBoList = _operationContext.Set<Nurse>()
                        .Include(n => n.User)
                        .Include(n => n.Hospital)
                        .Include(n => n.DreamHospitalList)
                        .Where(
                            n =>
                                n.Hospital.Id == currentHospitalId &&
                                n.DreamHospitalList.Any(d => d.Hospital.Id == dreamHospitalId))
                        .OrderByDescending(n => n.User.LastLoginOn)
                        .Skip(startPosition).Take(numberOfItems)
                        .ToList();
                }

                var filteredNurseDtoList = new List<NurseDto>();

                foreach (var nurseBo in filteredNurseBoList)
                {
                    var tempNurseDto = NurseDto.GetDto(nurseBo);
                    var dreamHospitalDtoList = (from dreamHospitalBo in nurseBo.DreamHospitalList
                        where dreamHospitalBo.IsActive
                        select DreamHospitalDto.GetDto(dreamHospitalBo))
                        .ToList();

                    tempNurseDto.DreamHospitalList = dreamHospitalDtoList;

                    // Removing contact and sensitive data
                    const string tempContact1FirstThreeCharacteres = "071";
                    tempNurseDto.User.ContactNumber1?.Substring(0, 3);
                    tempNurseDto.User.ContactNumber1 = tempContact1FirstThreeCharacteres + "-XXXXXXX";

                    const string tempContact2FirstThreeCharacteres = "077";
                    tempNurseDto.User.ContactNumber2?.Substring(0, 3);
                    tempNurseDto.User.ContactNumber2 = tempContact2FirstThreeCharacteres + "-XXXXXXX";
                    
                    tempNurseDto.User.Name = "XXXX XXXXXXXXXX";
                    tempNurseDto.User.Email = "xxxxx@xxxx.com";
                    tempNurseDto.User.SocialAccountProvider = 0;
                    tempNurseDto.User.SocialId = "************";

                    filteredNurseDtoList.Add(tempNurseDto);
                }

                var expObj = ResponseManager.GetSuccessResponse(filteredNurseDtoList, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return expObj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetIndividualProfileDetails(string requesteeGuid, string requesterGuid)
        {
            try
            {
                var requestee = _operationContext.Set<Nurse>()
                    .Include(r => r.DreamHospitalList)
                    .Include(r => r.DreamHospitalList.Select(h => h.Hospital))
                    .Include(r => r.Hospital)
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.User.Guid.ToString().Equals(requesteeGuid) && r.User.IsActive && !r.User.IsDeleted);

                if (requestee == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.REQUESTEE_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                var requesteeDto = NurseDto.GetDto(requestee);

                if (requesteeGuid != requesterGuid)
                {
                    // Removing contact and sensitive data
                    const string tempContact1FirstThreeCharacteres = "071";
                    requesteeDto.User.ContactNumber1?.Substring(0, 3);
                    requesteeDto.User.ContactNumber1 = tempContact1FirstThreeCharacteres + "-XXXXXXX";

                    const string tempContact2FirstThreeCharacteres = "077";
                    requesteeDto.User.ContactNumber2?.Substring(0, 3);
                    requesteeDto.User.ContactNumber2 = tempContact2FirstThreeCharacteres + "-XXXXXXX";


                    requesteeDto.User.Name = "XXXX XXXXXXXXXX";
                    requesteeDto.User.Email = "xxxxx@xxxx.com";
                    requesteeDto.User.SocialAccountProvider = 0;
                    requesteeDto.User.SocialId = "************";

                    requestee.User.MyCurrentViewCount = requesteeDto.User.MyCurrentViewCount + 1;
                    _operationContext.Save();
                }
                else
                {
                    requesteeDto.User.Name = requesteeDto.User.Name + "  (This is you)";
                }

                var dreamHospitalDtoList = (from dreamHospitalBo in requestee.DreamHospitalList
                    where dreamHospitalBo.IsActive
                    select DreamHospitalDto.GetDto(dreamHospitalBo))
                    .ToList();

                requesteeDto.DreamHospitalList = dreamHospitalDtoList;

                var obj = ResponseManager.GetSuccessResponse(requesteeDto, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        public ResponseObject GetContactDetails(string requesteeGuid, string requesterGuid)
        {
            try
            {
                var requester = _operationContext.Set<Nurse>()
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.User.Guid.ToString() == requesterGuid && r.User.IsActive && !r.User.IsDeleted);

                if (requester == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                var requestee = _operationContext.Set<Nurse>()
                    .Include(r => r.DreamHospitalList)
                    .Include(r => r.DreamHospitalList.Select(h => h.Hospital))
                    .Include(r => r.Hospital)
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.User.Guid.ToString().Equals(requesteeGuid) && r.User.IsActive && !r.User.IsDeleted);

                if (requestee == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.REQUESTEE_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                // check the eligibility of both parties
                // update remaining and view count by me
                // change register type if remaining view count is over
                if (requester.User.MyRemainingViewCount > 0 && (requester.User.State ==(int)UserStates.Paid || requester.User.State ==(int)UserStates.Promotion))
                {
                    //This one is eligible
                    if (requestee.User.State == (int)UserStates.Paid || requestee.User.State == (int)UserStates.Promotion)
                    {
                        // requestee is also eligible. No problem at all
                        var requesteeContactDetails = new NurseContactDetails()
                        {
                            Name = requestee.User.Name,
                            Contact1 = requestee.User.ContactNumber1,
                            Contact2 = requestee.User.ContactNumber2,
                            Email = requestee.User.Email
                        };

                        if (requesteeGuid != requesterGuid)
                        {
                            int shouldRemain = requestee.User.MyRemainingViewCount - 1;
                            requestee.User.MyRemainingViewCount = shouldRemain;

                            if (shouldRemain <= 0) requestee.User.State = (int) UserStates.Free;
                        }
                        else
                        {
                            requesteeContactDetails.Name = requesteeContactDetails.Name + "  (This is you)";
                        }

                        _operationContext.Save();

                        var obj = ResponseManager.GetSuccessResponse(requesteeContactDetails, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                        return obj;
                    }
                        // notify to requestee to upgrade the account
                        var objElse = ResponseManager.GetSuccessResponse(null, SuccessMessages.REQUESTEE_NOT_ELIGIBLE, ResponseType.Ok);
                        return objElse;
                }
                
                    // This one has no permission to view details
                if (!(requester.User.State == (int)UserStates.Paid || requester.User.State == (int)UserStates.Promotion))
                {
                    var objElse = ResponseManager.GetSuccessResponse(null, SuccessMessages.REQUESTER_NOT_PAID, ResponseType.Ok);
                    return objElse;
                }

                var objViewCount = ResponseManager.GetSuccessResponse(null, SuccessMessages.VIEW_COUNT_OVER, ResponseType.Ok);
                return objViewCount;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        #region Private Methods

        private ResponseObject _internalNurseSave(NurseDto nurseDto, string userGuid)
        {
            try
            {
                var userBo = _operationContext.Set<User>().FirstOrDefault(u => u.Guid.ToString() == userGuid);

                if (userBo == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                userBo.Name = nurseDto.User.Name;
                userBo.ContactNumber1 = nurseDto.User.ContactNumber1;
                userBo.ContactNumber2 = nurseDto.User.ContactNumber2;

                var hospitalBo = HospitalDto.GetBo(nurseDto.Hospital);
                var nurseBo = NurseDto.GetBo(nurseDto, hospitalBo, userBo);

                nurseBo.DreamHospitalList = new List<DreamHospital>();

                foreach (var dreamHospital in nurseDto.DreamHospitalList)
                {
                    var dreamHospitalHospitalBo = HospitalDto.GetBo(dreamHospital.Hospital);
                    var dreamHospitalBo = DreamHospitalDto.GetBo(dreamHospital, dreamHospitalHospitalBo, nurseBo);
                    dreamHospitalBo.IsActive = true;
                    nurseBo.DreamHospitalList.Add(dreamHospitalBo);
                }

                nurseBo.User.IsEmployeeDetailesProvided = true;

                _operationContext.Set<Nurse>().Add(nurseBo);
                _operationContext.Save();

                var obj = ResponseManager.GetSuccessResponse(null, SuccessMessages.NURSE_CREATED_SUCCESSFULLY, ResponseType.Created);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        private ResponseObject _intrenalNurseEdit(NurseDto nurseDto, string userGuid)
        {
            try
            {
                if (nurseDto.DreamHospitalList.Count <= 0)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.DREAM_HOSPITAL_LIST_LENGTH_INVALID, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                var existingNurse = _operationContext.Set<Nurse>()
                    .Include(n=>n.User)
                    .Include(n=>n.Hospital)
                    .Include(n=>n.DreamHospitalList)
                    .FirstOrDefault(n => n.User.Guid.ToString() == userGuid);

                if (existingNurse == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.USER_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

               

                var existingHospital = _operationContext.Set<Hospital>().FirstOrDefault(h => h.Id == nurseDto.Hospital.Id);

                if (existingHospital == null)
                {
                    var errObj = ResponseManager.GetLogicalErrorResponse(ErrorMessages.HOSPITAL_NOT_FOUND, "ERR-0008", ResponseType.InternalServerError);
                    return errObj;
                }

                existingNurse.User.ContactNumber1 = nurseDto.User.ContactNumber1;
                existingNurse.User.ContactNumber2 = nurseDto.User.ContactNumber2;
                existingNurse.User.Name = nurseDto.User.Name;

                existingNurse.Hospital = existingHospital;

                foreach (var dreamHospitalBo in existingNurse.DreamHospitalList.ToList())
                {
                    dreamHospitalBo.IsActive = false;
                }



                foreach (var dreamHospitalDto in nurseDto.DreamHospitalList)
                {
                    var existingDreamHospitalHospital = _operationContext.Set<Hospital>().FirstOrDefault(h => h.Id == dreamHospitalDto.Hospital.Id);

                    if (existingDreamHospitalHospital == null) continue;

                    var dreamHospitalBo = DreamHospitalDto.GetBo(dreamHospitalDto, existingDreamHospitalHospital, existingNurse);

                    dreamHospitalBo.IsActive = true;

                    existingNurse.DreamHospitalList.Add(dreamHospitalBo);
                    //_operationContext.Set<DreamHospital>().Add(dreamHospitalBo);
                }
                existingNurse.User.IsEmployeeDetailesProvided = true;
                _operationContext.Save();

                var obj = ResponseManager.GetSuccessResponse(null, SuccessMessages.NURSE_UPDATED_SUCCESSFULLY, ResponseType.Ok);
                return obj;
            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }
        }

        private ResponseObject _getMyNursingDetails(string myGuid)
        {
            try
            {
                var existingNurse = _operationContext.Set<Nurse>()
                    .Include(n => n.User)
                    .Include(n => n.Hospital)
                    .Include(n => n.DreamHospitalList)
                    // .Where(d=>d.DreamHospitalList)
                    .FirstOrDefault(n => n.User.Guid.ToString() == myGuid);



                if (existingNurse != null)
                {
                    var nurseDto = NurseDto.GetDto(existingNurse);
                    var hospitalDto = HospitalDto.GetDto(existingNurse.Hospital);
                    var userDto = UserDto.GetDto(existingNurse.User);
                    var dreamHospitalDtoList = existingNurse.DreamHospitalList.Select(DreamHospitalDto.GetDto).ToList();

                    foreach (var dreamHospital in dreamHospitalDtoList.ToList().Where(dreamHospital => !dreamHospital.IsActive))
                    {
                        dreamHospitalDtoList.Remove(dreamHospital);
                    }

                    nurseDto.User = userDto;
                    nurseDto.Hospital = hospitalDto;
                    nurseDto.DreamHospitalList = dreamHospitalDtoList;

                    var obj = ResponseManager.GetSuccessResponse(nurseDto, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                    return obj;
                }

                // if existing user is null means, he is being registered.
                var existingUser = _operationContext.Set<User>().FirstOrDefault(n => n.Guid.ToString() == myGuid);
                var virtualNurse = new NurseDto
                {
                    User = UserDto.GetDto(existingUser),
                    Hospital = new HospitalDto(),
                    DreamHospitalList = new List<DreamHospitalDto>()
                };

                var returningObj = ResponseManager.GetSuccessResponse(virtualNurse, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return returningObj;
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
