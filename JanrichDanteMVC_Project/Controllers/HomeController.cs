using Microsoft.AspNetCore.Mvc;
using JanrichDanteMVC_Project.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace JanrichDanteMVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private bool IsAuth() => !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));

        // --- AUTHENTICATION ---
        [HttpGet] public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (DataStore.Admins.Any(a => a.Username == admin.Username))
                {
                    ModelState.AddModelError("Username", "Username taken.");
                    return View(admin);
                }
                DataStore.Admins.Add(admin);
                TempData["Msg"] = "Registered! Please login.";
                return RedirectToAction("Login");
            }
            return View(admin);
        }

        [HttpGet] public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = DataStore.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("Username", admin.Username);
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Login"); }

        // --- DASHBOARD & CRUD ---
        public IActionResult Dashboard(string search)
        {
            if (!IsAuth()) return RedirectToAction("Login");

            ViewBag.Admin = HttpContext.Session.GetString("Username");
            ViewBag.Total = DataStore.Students.Count;
            ViewBag.Search = search;

            var students = DataStore.Students.AsEnumerable();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                students = students.Where(s => s.FirstName.ToLower().Contains(search) || s.StudentNumber.ToLower().Contains(search));
            }
            return View(students.ToList());
        }

        [HttpGet]
        public IActionResult StudentForm(int id = 0)
        {
            if (!IsAuth()) return RedirectToAction("Login");
            return View(id == 0 ? new Student() : DataStore.Students.FirstOrDefault(s => s.Id == id));
        }

        [HttpPost]
        public IActionResult StudentForm(Student student)
        {
            if (!IsAuth()) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    student.Id = DataStore.NextStudentId++;
                    DataStore.Students.Add(student);
                }
                else
                {
                    var existing = DataStore.Students.FirstOrDefault(s => s.Id == student.Id);
                    if (existing != null)
                    {
                        existing.StudentNumber = student.StudentNumber; existing.FirstName = student.FirstName;
                        existing.LastName = student.LastName; existing.Email = student.Email;
                        existing.Course = student.Course; existing.YearLevel = student.YearLevel;
                    }
                }
                return RedirectToAction("Dashboard");
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAuth()) return RedirectToAction("Login");
            var student = DataStore.Students.FirstOrDefault(s => s.Id == id);
            if (student != null) DataStore.Students.Remove(student);
            return RedirectToAction("Dashboard");
        }
    }
}