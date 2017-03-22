using System;
using Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Core.Persistence;

namespace Mutual.Portal.Service.BusinessLogic.NurseManagement
{
    public class NurseManager : INurseManager
    {
        private IOperationDbContext _operationContext;

        public NurseManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject GetIndividualNurse(string requesteeGuid, string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject GetNurseListByCurrentHospital(int hospitalId, string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject GetNurseListByDreamHospital(int hospitalId, string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject SaveNurse(NurseDto nurseDto)
        {
            throw new NotImplementedException();
        }
    }
}
