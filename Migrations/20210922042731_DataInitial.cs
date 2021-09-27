using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Tutors.Data;

namespace Tutors.Migrations
{
    public partial class DataInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            GetGoals(migrationBuilder);
            GetTeachers(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private void GetGoals(MigrationBuilder mBuilder)
        {
            string goalJson = File.ReadAllText("Data/Goals.json");
            Dictionary<string, string> goalDict = JsonSerializer.Deserialize<Dictionary<string, string>>(goalJson);
            foreach (string key in goalDict.Keys)
            {
                mBuilder.InsertData("Goals", new[] { "Id", "Description" }, new object[] { key, goalDict[key] });
            }
        }

        private void GetTeachers(MigrationBuilder mBuilder)
        {
            string teacherJson = File.ReadAllText("Data/Teachers.json");
            List<TeacherJson> teacherList = JsonSerializer.Deserialize<List<TeacherJson>>(teacherJson);
            foreach (TeacherJson teacher in teacherList)
            {
                mBuilder.InsertData("Teachers", new[] { "Id", "Name", "About", "Rating", "Picture", "Price" },
                    new object[]
                    {
                        teacher.Id,
                        teacher.Name,
                        teacher.About,
                        teacher.Rating,
                        teacher.Picture,
                        teacher.Price,
                    });

                foreach (string key in teacher.Goals)
                {
                    mBuilder.InsertData("TeacherGoals", new[] { "TeacherId", "GoalId" }, new object[] { teacher.Id, key });
                }

                foreach (string day in teacher.Free.Keys)
                {
                    foreach (string time in teacher.Free[day].Keys)
                    {
                        mBuilder.InsertData("Schedules", new[] { "TeacherId", "Day", "Time", "Available" }, new object[] { teacher.Id, day, time, teacher.Free[day][time] });
                    }
                }

            }
        }
    }
}
