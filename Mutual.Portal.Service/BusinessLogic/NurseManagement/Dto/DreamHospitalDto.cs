using Mutual.Portal.Core.Entities.Nursing;



namespace Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto
{
    public class DreamHospitalDto
    {
        #region Public Properties

        public int Id { get; set; }
        public NurseDto Nurse { get; set; }
        public HospitalDto Hospital { get; set; }
        public bool IsActive { get; set; }

        #endregion

        public static DreamHospitalDto GetDto(DreamHospital dreamHospital)
        {
            var obj = new DreamHospitalDto()
            {
                Id = dreamHospital.Id,
                Hospital = dreamHospital.Hospital != null ? HospitalDto.GetDto(dreamHospital.Hospital) : null,
                Nurse = dreamHospital.Nurse != null ? NurseDto.GetDto(dreamHospital.Nurse) : null,
                IsActive = dreamHospital.IsActive
            };

            return obj;
        }

        public static DreamHospital GetBo(DreamHospitalDto dreamHospitalDto, Hospital hospital, Nurse nurse)
        {
            var obj = new DreamHospital()
            {
                Id = dreamHospitalDto.Id,
                Hospital = hospital,
                Nurse = nurse,
                IsActive = dreamHospitalDto.IsActive
            };

            return obj;
        }
    }
}
