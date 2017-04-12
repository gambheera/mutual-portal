using System;
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
                   var savedObj = _internalNurseSave(nurseDto);
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

        #region

        private ResponseObject _internalNurseSave(NurseDto nurseDto)
        {
            try
            {
                var userBo = UserDto.GetBo(nurseDto.User);
                var hospitalBo = HospitalDto.GetBo(nurseDto.Hospital);
                var nurseBo = NurseDto.GetBo(nurseDto, hospitalBo, userBo);

                nurseBo.DreamHospitalList = new List<DreamHospital>();

                foreach (var dreamHospital in nurseDto.DreamHospitalList)
                {
                    var dreamHospitalHospitalBo = HospitalDto.GetBo(dreamHospital.Hospital);
                    var dreamHospitalBo = DreamHospitalDto.GetBo(dreamHospital, dreamHospitalHospitalBo, nurseBo);
                    nurseBo.DreamHospitalList.Add(dreamHospitalBo);
                }

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

                existingNurse.DreamHospitalList.Clear();
                existingNurse.DreamHospitalList = new List<DreamHospital>();

                foreach (var dreamHospitalDto in nurseDto.DreamHospitalList)
                {
                    var existingDreamHospitalHospital = _operationContext.Set<Hospital>().FirstOrDefault(h => h.Id == dreamHospitalDto.Hospital.Id);

                    if (existingDreamHospitalHospital == null) continue;

                    var dreamHospitalBo = DreamHospitalDto.GetBo(dreamHospitalDto, existingDreamHospitalHospital, existingNurse);

                    existingNurse.DreamHospitalList.Add(dreamHospitalBo);
                }

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
                    .FirstOrDefault(n => n.User.Guid.ToString() == myGuid);

                if (existingNurse != null)
                {
                    var nurseDto = NurseDto.GetDto(existingNurse);
                    var hospitalDto = HospitalDto.GetDto(existingNurse.Hospital);
                    var userDto = UserDto.GetDto(existingNurse.User);
                    var dreamHospitalDtoList = existingNurse.DreamHospitalList.Select(DreamHospitalDto.GetDto).ToList();

                    nurseDto.User = userDto;
                    nurseDto.Hospital = hospitalDto;
                    nurseDto.DreamHospitalList = dreamHospitalDtoList;

                    var obj = ResponseManager.GetSuccessResponse(nurseDto, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                    return obj;
                }

                // if existing user is null means, he is being registered.
                var existingUser = _operationContext.Set<User>().FirstOrDefault(n => n.Guid.ToString() == myGuid);
                var virtualNurse = new NurseDto();
                virtualNurse.User = UserDto.GetDto(existingUser);
                virtualNurse.Hospital = new HospitalDto();
                virtualNurse.DreamHospitalList = new List<DreamHospitalDto>();

                var returningObj = ResponseManager.GetSuccessResponse(virtualNurse, SuccessMessages.SUCCESSFULLY_FECHED, ResponseType.Ok);
                return returningObj;

            }
            catch (Exception ex)
            {
                var expObj = ResponseManager.GetExceptionResponse(ExceptionMessages.OPERATION_FAILED, ex, "EXP-0004", ResponseType.InternalServerError);
                return expObj;
            }

            return null;
        }

        #endregion
    }
}
