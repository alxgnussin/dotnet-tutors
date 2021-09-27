using System.Collections.Generic;
using Tutors.Models;

namespace Tutors.Forms
{
    public class ScheduleForm
    {
        public string Day { get; set; }
        public List<Schedule> Times { get; set; }
    }
}
