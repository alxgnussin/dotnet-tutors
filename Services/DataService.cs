using System.Collections.Generic;
using System.Linq;
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
    }
    
}
