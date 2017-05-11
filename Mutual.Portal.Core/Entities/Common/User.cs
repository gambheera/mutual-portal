
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

        [Required]
        public string SocialId { get; set; }

        [Required]
        public int SocialAccountProvider { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        [Required]
        public DateTime LastLoginOn { get; set; }

        public string ContactNumber1 { get; set; }

        public string ContactNumber2 { get; set; }

        public string Email { get; set; }

        public string Code { get; set; }

        [Required]
        public int EmploymentType { get; set; } // Referring EmploymentTypes enum

        [Required]
        public int State { get; set; } // Referring UserStates enum

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRegistrationConfirmed { get; set; }

        public bool IsEmployeeDetailesProvided { get; set; }

        public bool IsWorkingPlaceDetailsProvided { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public int MyCurrentViewCount { get; set; }

        public int MyTotalViewCount { get; set; }

        public int CountViwedByMe { get; set; }

        public int MyRemainingViewCount { get; set; }
    }
}
