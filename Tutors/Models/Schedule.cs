using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutors.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public bool Available { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        [InverseProperty("Schedule")]
        public List<Booking> Booking { get; set; }
    }
}
