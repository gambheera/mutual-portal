using Mutual.Portal.Core.Entities.Nursing;
using Mutual.Portal.Utility.Operations;
using Mutual.Portal.Utility.Enums;

namespace Mutual.Portal.Service.BusinessLogic.NurseManagement.Dto
{
    public class HospitalDto
    {
        #region Public Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public int District { get; set; }
        public int Category { get; set; }
        public string DistrictString => EnumConverter.StringToEnumInt<District>(District);
        public string CategoryString => EnumConverter.StringToEnumInt<HospitalType>(Category);

        #endregion

        public static HospitalDto GetDto(Hospital hospital)
        {
            var obj = new HospitalDto()
            {
                Id = hospital.Id,
                Category = hospital.Category,
                District = hospital.District,
                Name = hospital.Name
            };

            return obj;
        }

        public static Hospital GetBo(HospitalDto hospitalDto)
        {
            var obj = new Hospital()
            {
                Id = hospitalDto.Id,
                Category = hospitalDto.Category,
                District = hospitalDto.District,
                Name = hospitalDto.Name
            };

            return obj;
        }
    }
}
