using Microsoft.EntityFrameworkCore;

namespace Tutors.Models
{
    public class DataBase: DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {           
        }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherGoal> TeacherGoals { get; set; }
    }
}
