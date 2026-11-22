using Microsoft.AspNetCore.Mvc;
using JanrichDanteMVC_Project.Data;
using JanrichDanteMVC_Project.Models;
using System.Linq;

namespace JanrichDanteMVC_Project.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;

        public InstructorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Instructors.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Instructors.FirstOrDefault(i => i.Id == instructor.Id);
                if (existing != null)
                {
                    existing.FirstName = instructor.FirstName;
                    existing.LastName = instructor.LastName;
                    existing.Email = instructor.Email;
                    existing.Department = instructor.Department;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(instructor);
        }

        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}