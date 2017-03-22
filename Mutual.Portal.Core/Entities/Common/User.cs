
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual.Portal.Core.Entities.Common
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string FacebookId { get; set; }

        public string GoogleId { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        [Required]
        public DateTime LastLoginOn { get; set; }

        public string ContactNumber1 { get; set; }

        public string ContactNumber2 { get; set; }

        public string Email { get; set; }

        [Required]
        public int EmploymentType { get; set; } // Referring EmploymentTypes enum

        [Required]
        public int State { get; set; } // Referring UserStates enum

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public int MyCurrentViewCount { get; set; }

        public int MyTotalViewCount { get; set; }


    }
}
