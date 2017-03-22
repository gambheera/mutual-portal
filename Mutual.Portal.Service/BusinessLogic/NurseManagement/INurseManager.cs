using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using Mutual.Portal.Utility.Models;


namespace Mutual.Portal.Service.BusinessLogic.NurseManagement
{
    public interface INurseManager
    {
        ResponseObject SaveNurse(NurseDto nurseDto);
        ResponseObject GetIndividualNurse(string requesteeGuid, string requesterGuid);
        ResponseObject GetNurseListByCurrentHospital(int hospitalId, string requesterGuid);
        ResponseObject GetNurseListByDreamHospital(int hospitalId, string requesterGuid);
    }
}
