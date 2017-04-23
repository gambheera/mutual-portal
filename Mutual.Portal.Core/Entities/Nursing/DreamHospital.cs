using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutual.Portal.Core.Entities.Nursing
{
    public class DreamHospital
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required]
        [Index("DreamHospital_CandidateKey_1", 1, IsUnique = true)]
        public Nurse Nurse { get; set; }

        [Required]
        [Index("DreamHospital_CandidateKey_1", 2, IsUnique = true)]
        public Hospital Hospital { get; set; }

        public bool IsActive { get; set; }
    }
}
