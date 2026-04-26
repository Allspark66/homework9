using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models;
using Microsoft.Data.Sqlite;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<StudentCourse> list = new List<StudentCourse>();
            using (var con = new SqliteConnection(@"Data Source=C:\Users\Memmedov\Documents\university\c#\tapsirig9\database.db"))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT StudentName, CourseName FROM Student";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new StudentCourse
                        {
                            StudentName = dr["StudentName"].ToString(),
                            CourseName = dr["CourseName"].ToString(),
                            CurrentCount = 1,
                            Limit = 0
                        });
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Index(string studentName, string courseName)
        {
            using (var con = new SqliteConnection(@"Data Source=C:\Users\Memmedov\Documents\university\c#\tapsirig9\database.db"))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Student (StudentName, CourseName) VALUES (@student, @course)";

                cmd.Parameters.AddWithValue("@student", studentName);
                cmd.Parameters.AddWithValue("@course", courseName);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}