using Microsoft.EntityFrameworkCore;
using System;
using Tutors.Models;

namespace TutorsTest
{
    public static class TestData
    {
        public static DataBase GetTestDataBase()
        {
            var options = new DbContextOptionsBuilder<DataBase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataBase(options);

            context.Goals.Add(new Goal { Id = "g1", Description = "Goal 1" });
            context.Teachers.Add(new Teacher
            {
                Id = 1,
                Name = "Billy",
                About = "always hungry",
                Rating = 9.5M,
                Picture = "pics",
                Price = 100
            });
            context.Schedules.Add(new Schedule { Id = 1, Day = "mon", Time = "12:00", Available = true, TeacherId = 1 });
            context.TeacherGoals.Add(new TeacherGoal { Id = 1, GoalId = "g1", TeacherId = 1 });
            context.SaveChanges();

            return context;
        }
    }
}
