using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutors.Forms;
using Tutors.Models;

namespace Tutors.Services
{
    public interface IDataService
    {
        List<Teacher> GetTeachers();
        Teacher GetTeacher(int id);
        List<ScheduleForm> GetSchedules(int teacherId);
        List<Goal> GetGoals(int teacherId);
        List<Teacher> GetGoalTeacher(string id);
        Goal GetGoal(string id);
        List<Goal> AllGoals();
        void RequestCreate(Request request);
        Schedule GetSchedule(int id);
        void BookingCreate(Booking booking);
    }
}
