using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using Mutual.Portal.Utility.Models;


namespace Mutual.Portal.Service.BusinessLogic.NurseManagement
{
    public interface INurseManager
    {
        ResponseObject GetHospitalList();
        ResponseObject SaveNurse(NurseDto nurseDto, string userGuid);
        ResponseObject SearchNurses(int currentHospitalId, int dreamHospitalId, int pageNumber);
        ResponseObject GetIndividualNurse(string requesteeGuid, string requesterGuid);
        ResponseObject GetNurseListByCurrentHospital(int hospitalId, string requesterGuid);
        ResponseObject GetNurseListByDreamHospital(int hospitalId, string requesterGuid);
    }
}
