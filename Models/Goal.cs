using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutors.Models
{
   
    public class Goal
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }

        [InverseProperty("Goal")]
        public List<TeacherGoal> Teachers { get; set; }
    }
}
