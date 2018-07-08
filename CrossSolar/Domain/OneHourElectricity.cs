using System;
using System.ComponentModel.DataAnnotations;

namespace CrossSolar.Domain
{
    public class OneHourElectricity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string PanelId { get; set; }

        [Required]
        [Range(0, 10000000000)]
        public long KiloWatt { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}