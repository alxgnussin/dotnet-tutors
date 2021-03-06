using System.ComponentModel.DataAnnotations;

namespace Tutors.Forms
{
    public class RequestForm
    {
        [Required(ErrorMessage = "Please select goal")]
        public string Goal { get; set; }
        public string Time { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        public string Phone { get; set; }

        // [BindNever]
        // public List<Goal> Goals { get; set; }
    }
}
