using Mutual.Portal.Service.BusinessLogic.NurseManagement;
using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers.BusinessApi
{
    [RoutePrefix("api/nurse")]
    public class NurseController : ApiController
    {
        private INurseManager _nurseManager;

        public NurseController(INurseManager nurseManager)
        {
            _nurseManager = nurseManager;
        }

        [HttpPost]
        [Authorize]
        [Route("save-nurse")]
        public IHttpActionResult SaveNurse(NurseDto nurseDto)
        {
            var obj = _nurseManager.SaveNurse(nurseDto);
            return Ok(obj);
        }

        [HttpGet]
        [Authorize]
        [Route("get-individual-nurse")]
        public IHttpActionResult GetIndividualNurse(string requesteeGuid)
        {
            var obj = _nurseManager.GetIndividualNurse(requesteeGuid, "");
            return Ok(obj);
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

    }
}
