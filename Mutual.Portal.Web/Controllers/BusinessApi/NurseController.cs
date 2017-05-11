using Mutual.Portal.Service.BusinessLogic.NurseManagement;
using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers.BusinessApi
{
    [RoutePrefix("api/nurse")]
    public class NurseController : ApiController
    {
        private readonly INurseManager _nurseManager;

        public NurseController(INurseManager nurseManager)
        {
            _nurseManager = nurseManager;
        }

        [HttpGet]
        [Authorize]
        [Route("get-hospital-list")]
        public IHttpActionResult GetHospitalList()
        {
            var obj = _nurseManager.GetHospitalList();
            return _getHttpClientResponse(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("search-nurses")]
        public IHttpActionResult SearchNurses(int currentHospitalId, int dreamHospitalId, int pageNumber)
        {
            var obj = _nurseManager.SearchNurses(currentHospitalId, dreamHospitalId, pageNumber);
            return _getHttpClientResponse(obj);
        }

        [HttpPost]
        [Authorize]
        [Route("save-nurse")]
        public IHttpActionResult SaveNurse(NurseDto nurseDto)
        {
            string guid = _getUserGuid();
            var obj = _nurseManager.SaveNurse(nurseDto, guid);
            return _getHttpClientResponse(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-individual-nurse")]
        public IHttpActionResult GetIndividualNurse(string requesteeGuid)
        {
            string requesterGuid = _getUserGuid();
            var obj = _nurseManager.GetIndividualNurse(requesteeGuid, requesterGuid);
            return _getHttpClientResponse(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-nurse-list-by-current-hospital")]
        public IHttpActionResult GetNurseListByCurrentHospital(int hospitalId)
        {
            var obj = _nurseManager.GetNurseListByCurrentHospital(hospitalId, "");
            return Ok(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-nurse-list-by-dream-hospital")]
        public IHttpActionResult GetNurseListByDreamHospital(int hospitalId)
        {
            var obj = _nurseManager.GetNurseListByDreamHospital(hospitalId, "");
            return Ok(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-individual-profile-details")]
        public IHttpActionResult GetIndividualProfileDetails(string requesteeGuid)
        {
            var requesterGuid = _getUserGuid();
            var obj = _nurseManager.GetIndividualProfileDetails(requesteeGuid, requesterGuid);
            return Ok(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-contact-details")]
        public IHttpActionResult GetContactDetails(string requesteeGuid)
        {
            var requesterGuid = _getUserGuid();
            var obj = _nurseManager.GetContactDetails(requesteeGuid, requesterGuid);
            return Ok(obj);
        }

        #region Helpers

        private IHttpActionResult _getHttpClientResponse(ResponseObject responseObject)
        {
            if (responseObject.MetaData.HttpResponse == ResponseType.InternalServerError)
            {
                // Then this is an exception. return 500
                var exp = responseObject.MetaData.Exception;
                var message = responseObject.MetaData.Message + "\nError Code: " + responseObject.MetaData.ErrorCode;

                if (exp == null) exp = new Exception(message);

                exp.Source = message;
                var err = InternalServerError(exp);

                return err;
            }

            if (responseObject.MetaData.HttpResponse == ResponseType.Created)
            {
                return Created("", responseObject);
            }

            return Ok(responseObject);
        }

        public string _getUserGuid()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.Claims.Where(c => c.Type == ClaimTypes.SerialNumber).Select(c => c.Value).SingleOrDefault();
            return userEmail;
        }

        #endregion

    }
}
