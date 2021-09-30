using System.Collections.Generic;
using Tutors.Models;

namespace Tutors.Forms
{
    public class TeacherProfileForm
    {
        public Teacher Teacher { get; set; }
        public List<ScheduleForm> Schedules { get; set; }
        public List<Goal> Goals { get; set; }
    }
}
