using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tutors.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public string Time { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [ForeignKey("GoalId")]
        [Required]
        public Goal Goal { get; set; }
        public string GoalId { get; set; }
    }
}
