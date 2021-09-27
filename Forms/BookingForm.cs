using Tutors.Models;

namespace Tutors.Forms
{
    public class BookingForm
    {
        public Teacher Teacher { get; set; }
        public Schedule Schedule { get; set; }
        public string Day { get; set; }
    }
}
