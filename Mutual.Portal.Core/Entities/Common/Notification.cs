using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual.Portal.Core.Entities.Common
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }

        public bool IsRead { get; set; }

        public int Type { get; set; }

        public DateTime Time { get; set; }

        public User User { get; set; }
    }
}
