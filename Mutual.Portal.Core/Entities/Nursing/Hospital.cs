

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual.Portal.Core.Entities.Nursing
{
    public class Hospital
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int District { get; set; }

        public int Category { get; set; }
    }
}
