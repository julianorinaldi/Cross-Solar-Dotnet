using System;
using System.ComponentModel.DataAnnotations;

namespace CrossSolar.Models
{
    public class OneHourElectricityModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 10000000000)]
        public long KiloWatt { get; set; }

        public DateTime DateTime { get; set; }
    }
}