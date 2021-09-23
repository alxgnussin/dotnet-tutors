using System.Collections.Generic;
using System.Linq;
using Tutors.Forms;
using Tutors.Models;

namespace Tutors.Services
{
    public class DataService
    {
        private DataBase _db;

        public DataService(DataBase db)
        {
            _db = db;
        }

        public List<Teacher> GetTeachers()
        {
            List<Teacher> result = _db.Teachers.ToList();
            return result;
        }

        public Teacher GetTeacher(int id)
        {
            Teacher teacher = _db.Teachers.Where(x => x.Id == id).FirstOrDefault();
            return teacher;
        }

        public List<ScheduleForm> GetSchedules(int teacherId)
        {
            List<Schedule> records = _db.Schedules.Where(x => x.TeacherId == teacherId).OrderBy(x => x.Time).ToList();
            List<ScheduleForm> result = new List<ScheduleForm>()
            {
                new ScheduleForm { Day = "Понедельник", Times = records.Where(x => x.Day == "mon" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Вторник", Times = records.Where(x => x.Day == "tue" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Среда", Times = records.Where(x => x.Day == "wed" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Четверг", Times = records.Where(x => x.Day == "thu" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Пятница", Times = records.Where(x => x.Day == "fri" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Суббота", Times = records.Where(x => x.Day == "sat" && x.Available).Select(x => x.Time).ToList() },
                new ScheduleForm { Day = "Воскресение", Times = records.Where(x => x.Day == "sun" && x.Available).Select(x => x.Time).ToList() },
            };
            return result;
        }

        public List<Goal> GetGoals(int teacherId)
        {
            List<Goal> result = _db.TeacherGoals.Where(x => x.TeacherId == teacherId).Select(x => x.Goal).ToList();
            return result;
        }

        public List<Teacher> GetGoalTeacher(string id)
        {
            List<Teacher> result = _db.TeacherGoals.Where(x => x.GoalId == id).Select(x => x.Teacher).OrderBy(x => x.Price).ToList();
            return result;
        }

        public Goal GetGoal(string id)
        {
            Goal result = _db.Goals.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public List<Goal> AllGoals()
        {
            List<Goal> result = _db.Goals.ToList();
            return result;
        }
    }
    
}
