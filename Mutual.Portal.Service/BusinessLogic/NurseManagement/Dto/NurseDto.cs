using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Core.Entities.Nursing;
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using System.Collections.Generic;

namespace Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto
{
    public class NurseDto
    {
        #region Public Properties

        public int Id { get; set; }
        public UserDto User { get; set; }
        public virtual HospitalDto Hospital { get; set; }

        public List<DreamHospitalDto> DreamHospitalList { get; set; }

        #endregion

        public static NurseDto GetDto(Nurse nurse)
        {
            var obj = new NurseDto()
            {
                Id = nurse.Id,
                Hospital = nurse.Hospital != null ? HospitalDto.GetDto(nurse.Hospital) : null,
                User = nurse.User != null ? UserDto.GetDto(nurse.User) : null
            };

            return obj;
        }

        public static Nurse GetBo(NurseDto nurseDto, Hospital hospital, User user)
        {
            var obj = new Nurse()
            {
                Id = nurseDto.Id,
                Hospital = hospital,
                User = user
            };

            return obj;
        }
    }
}
