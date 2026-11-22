using Microsoft.AspNetCore.Mvc;
using JanrichDanteMVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace JanrichDanteMVC_Project.Controllers
{
    public class InstructorController : Controller
    {
        private static List<Instructor> instructorList = new List<Instructor>();

        public IActionResult Index()
        {
            return View(instructorList);
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
                instructor.Id = instructorList.Any() ? instructorList.Max(i => i.Id) + 1 : 1;
                instructorList.Add(instructor);
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        public IActionResult Edit(int id)
        {
            var instructor = instructorList.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                var existing = instructorList.FirstOrDefault(i => i.Id == instructor.Id);
                if (existing != null)
                {
                    existing.FirstName = instructor.FirstName;
                    existing.LastName = instructor.LastName;
                    existing.Email = instructor.Email;
                    existing.Department = instructor.Department;
                    return RedirectToAction("Index");
                }
            }
            return View(instructor);
        }

        public IActionResult Details(int id)
        {
            var instructor = instructorList.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult Delete(int id)
        {
            var instructor = instructorList.FirstOrDefault(i => i.Id == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var instructor = instructorList.FirstOrDefault(i => i.Id == id);
            if (instructor != null)
            {
                instructorList.Remove(instructor);
            }
            return RedirectToAction("Index");
        }
    }
}