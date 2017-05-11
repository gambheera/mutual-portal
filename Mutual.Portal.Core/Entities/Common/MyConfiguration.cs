using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mutual.Portal.Core.Entities.Common
{
    public class MyConfiguration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        public string ConfigCode { get; set; }

        public string Value { get; set; }
    }
}
