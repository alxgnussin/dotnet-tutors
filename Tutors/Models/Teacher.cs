using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutors.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public decimal Rating { get; set; }
        public string Picture { get; set; }
        public int Price { get; set; }

        [InverseProperty("Teacher")]
        public List<TeacherGoal> Goals { get; set; }

        [InverseProperty("Teacher")]
        public List<Schedule> Schedules { get; set; }
    }
}
