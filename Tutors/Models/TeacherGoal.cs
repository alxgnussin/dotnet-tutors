using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutors.Models
{
    public class TeacherGoal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        [ForeignKey("GoalId")]
        [Required]
        public Goal Goal { get; set; }
        public string GoalId { get; set; }
    }
}
