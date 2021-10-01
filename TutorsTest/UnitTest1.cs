using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Tutors.Controllers;
using Tutors.Models;
using Tutors.Services;
using Xunit;

namespace TutorsTest
{
    public class UnitTest1
    {
        /*        private ITestOutputHelper output;

                public UnitTest1(ITestOutputHelper output)
                {
                    this.output = output;
                }*/

        [Fact]
        public void TestCreateRequest()
        {
            // using var db = TestData.GetTestDataBase();
            // var dataService = new DataService(db);
            // using var controller = new HomeController(dataService);

            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(x => x.AllGoals()).Returns(new List<Goal>() { new Goal { Id = "g1", Description = "G1" } });

            using var controller = new HomeController(dataServiceMock.Object);

            var result = controller.CreateRequest() as ViewResult;
            Assert.NotNull(result);
            Assert.Equal("Request", result.ViewName);

            // dataServiceMock.Verify(x => x.AllGoals(), Times.Once);

            var goals = controller.ViewBag.Goals as List<Goal>;
            Assert.NotNull(goals);
            Assert.Single(goals); // Assert.Equal(1, goals.Count);
            Assert.Equal("g1", goals[0].Id);
        }

        [Fact]
        public void TestGetTeachers()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);

            var teachers = dataService.GetTeachers();
            Assert.NotNull(teachers);
            Assert.Single(teachers);
            Assert.Equal(1, teachers[0].Id);
        }

        [Fact]
        public void TestGetTeacher()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            int TestId = 1;

            var teacher = dataService.GetTeacher(TestId);
            Assert.NotNull(teacher);
            Assert.Equal(TestId, teacher.Id);
        }

        [Fact]
        public void TestGetSchedules()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            int TestId = 1;

            var result = dataService.GetSchedules(TestId);
            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
            Assert.Equal("Понедельник", result[0].Day);
            Assert.Equal("12:00", result[0].Times[0].Time);
        }

        [Fact]
        public void TestGetGoals()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            int TestId = 1;

            var result = dataService.GetGoals(TestId);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("g1", result[0].Id);
        }

        [Fact]
        public void TestGetGoalTeacher()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            string goalId = "g1";

            var result = dataService.GetGoalTeacher(goalId);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void TestAllGoals()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);

            var result = dataService.AllGoals();
            Assert.NotNull(result);
            Assert.Single(result); // Assert.Equal(1, goals.Count);
            Assert.Equal("g1", result[0].Id);
        }

        [Fact]
        public void TestGetGoal()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            string goalId = "g1";

            var result = dataService.GetGoal(goalId);
            Assert.NotNull(result);
            Assert.Equal(goalId, result.Id);
        }

        [Fact]
        public void TestRequestCreate()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);

            var result = db.Requests.ToList();
            Assert.Empty(result);

            var request = new Request { Id = 1, Time = "1-2", Name = "Any", Phone = "123456", GoalId = "g1" };
            dataService.RequestCreate(request);
            result = db.Requests.ToList();
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("1-2", result[0].Time);
            Assert.Equal("Any", result[0].Name);
            Assert.Equal("123456", result[0].Phone);
            Assert.Equal("g1", result[0].GoalId);
        }

        [Fact]
        public void TestGetSchedule()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);
            int schedId = 1;

            var result = dataService.GetSchedule(schedId);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestBookingCreate()
        {
            using var db = TestData.GetTestDataBase();
            var dataService = new DataService(db);

            var result = db.Bookings.ToList();
            Assert.Empty(result);

            var booking = new Booking { Id = 1, Name = "One", Phone = "321654", ScheduleId = 1 };
            dataService.BookingCreate(booking);
            result = db.Bookings.ToList();
            var sched = dataService.GetSchedule(1);
            Assert.False(sched.Available);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("One", result[0].Name);
            Assert.Equal("321654", result[0].Phone);
            Assert.Equal(1, result[0].ScheduleId);
        }
    }
}
