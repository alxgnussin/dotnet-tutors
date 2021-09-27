using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tutors.Forms;
using Tutors.Models;
using Tutors.Services;

namespace Tutors.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private DataService _dataService;
        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        [Route("/")]
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

        [HttpGet]
        [Route("profile/{id}")]
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

        [HttpGet]
        [Route("all")]
        public IActionResult All([FromQuery] int? sorting)
        {
            List<Teacher> teachers = _dataService.GetTeachers();

            if (sorting == 1)
            {
                teachers = teachers.OrderByDescending(x => x.Price).ToList();
            }
            else if (sorting == 2)
            {
                teachers = teachers.OrderBy(x => x.Price).ToList();
            }
            else if (sorting == 3)
            {
                teachers = teachers.OrderByDescending(x => x.Rating).ToList();
            }
            else
            {
                Random rng = new Random();
                teachers = teachers.OrderBy(a => rng.Next()).ToList();
            }


            return View(teachers);
        }

        [HttpGet]
        [Route("goal/{id}")]
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

        [HttpGet]
        [Route("request")]
        public IActionResult CreateRequest()
        {
            var goals = _dataService.AllGoals();
            return View(goals);
        }

        [HttpPost]
        [Route("request/submit")]
        public IActionResult RequestSubmit([FromForm] RequestForm requestForm)
        {
            var request = new Request
            {
                GoalId = requestForm.Goal,
                Time = requestForm.Time,
                Name = requestForm.Name,
                Phone = requestForm.Phone
            };
            _dataService.RequestCreate(request);

            var result = new RequestDoneForm
            {
                GoalDescription = _dataService.GetGoal(requestForm.Goal)?.Description,
                Time = requestForm.Time,
                Name = requestForm.Name,
                Phone = requestForm.Phone
            };

            return View("RequestDone", result);
        }

        [HttpGet]
        [Route("booking")]
        public IActionResult Booking(int scheduleId)
        {
            var result = new BookingForm
            {
                Schedule = _dataService.GetSchedule(scheduleId),
                Teacher = _dataService.GetTeacher(_dataService.GetSchedule(scheduleId).TeacherId),
            };
            return View(result);
        }

        [HttpPost]
        [Route("booking/submit")]
        public IActionResult BookingSubmit([FromForm] BookingForm bookingForm)
        {

        }


    }
}
