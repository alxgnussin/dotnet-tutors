using System.ComponentModel.DataAnnotations;

namespace Tutors.Forms
{
    public class BookingSubmitForm
    {
        public int ScheduleId { get; set; }

        [Required(ErrorMessage="Please enter your name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        public string ClientPhone { get; set; }
    }
}
