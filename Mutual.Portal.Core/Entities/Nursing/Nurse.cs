using Mutual.Portal.Core.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mutual.Portal.Core.Entities.Nursing
{
    public class Nurse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public virtual Hospital Hospital { get; set; }

        public virtual ICollection<DreamHospital> DreamHospitalList { get; set; }
    }
}
