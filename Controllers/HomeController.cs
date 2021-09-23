using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutors.Forms;
using Tutors.Models;
using Tutors.Services;

namespace Tutors.Controllers
{
    public class HomeController : Controller
    {
        private DataService _dataService;
        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }
        public IActionResult Index()
        {
            Random rng = new Random();
            List<Teacher> teachers = _dataService.GetTeachers().OrderBy(a => rng.Next()).Take(5).ToList();
            IndexDataForm result = new IndexDataForm
            {
                Goals = _dataService.AllGoals(),
                Teachers = teachers,
            };
            return View(result);
        }

        [Route("home/profile/{id}")]
        public IActionResult Profile(int id)
        {
            Teacher teacher = _dataService.GetTeacher(id);
            List<ScheduleForm> schedules = _dataService.GetSchedules(id);
            List<Goal> goals = _dataService.GetGoals(id);
            TeacherProfileForm teacherForm = new TeacherProfileForm
            {
                Teacher = teacher,
                Schedules = schedules,
                Goals = goals,
            };
            return View(teacherForm);
        }

        [Route("home/all")]
        public IActionResult All()
        {
            List<Teacher> teachers = _dataService.GetTeachers();
            return View(teachers);
        }

        [Route("home/goal/{id}")]
        public IActionResult Goal(string id)
        {
            var goal = _dataService.GetGoal(id);
            GoalTeachersForm result = new GoalTeachersForm
            {
                Goal = goal.Description,
                Teachers = _dataService.GetGoalTeacher(id),
            };
            return View(result);
        }
        
    }
}
