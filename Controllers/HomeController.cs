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
            var form = new RequestForm()
            {
                // Goals = goals
            };

            ViewBag.Goals = goals;

            return View("Request", form);
        }

        [HttpPost]
        [Route("request/submit")]
        public IActionResult RequestSubmit([FromForm] RequestForm requestForm)
        {
            if (!ModelState.IsValid)
            {
                var goals = _dataService.AllGoals();
                ViewBag.Goals = goals;
                return View("Request", requestForm);
            }

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
        [Route("booking/{scheduleId}")]
        public IActionResult CreateBooking(int scheduleId)
        {
            var form = new BookingSubmitForm() { };
            ViewBag.Schedule = _dataService.GetSchedule(scheduleId);
            ViewBag.Teacher = _dataService.GetTeacher(_dataService.GetSchedule(scheduleId).TeacherId);
            return View("Booking", form);
        }

        [HttpPost]
        [Route("booking/{scheduleId}/submit")]
        public IActionResult BookingSubmit(int scheduleId, [FromForm] BookingSubmitForm bookingSubmitForm)
        {
            if (!ModelState.IsValid)
            {
                var form = new BookingSubmitForm() { };
                ViewBag.Schedule = _dataService.GetSchedule(scheduleId);
                ViewBag.Teacher = _dataService.GetTeacher(_dataService.GetSchedule(scheduleId).TeacherId);
                return View("Booking", form);
            }
            
            var booking = new Booking
            {
                Name = bookingSubmitForm.ClientName,
                Phone = bookingSubmitForm.ClientPhone,
                ScheduleId = bookingSubmitForm.ScheduleId
            };
            _dataService.BookingCreate(booking);

            var schedule = _dataService.GetSchedule(booking.ScheduleId);

            var result = new BookingDoneForm
            {
                Day = schedule.Day,
                Time = schedule.Time,
                Name = booking.Name,
                Phone = booking.Phone
            };

            return View("BookingDone", result);
        }
    }
}
