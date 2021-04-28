using HTMLprojectMVC.Models;
using HTMLprojectMVC.Models.Context;
using HTMLprojectMVC.Models.Context.Entities;
using HTMLprojectMVC.Models.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HTMLprojectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var context = new UniContext())
            {
                var groups = context.Groups.ToList();
                var students = context.Students.ToList();
                return View((groups: groups, students: students));
            }

        }

        public IActionResult Add(int studentId, bool error = false)
        {
            using (var context = new UniContext())
            {
                var groups = context.Groups.ToList();
                var students = context.Students.ToList();

                return View((studentId: studentId, groups: groups, students: students, error: error));
            }

        }

        public IActionResult Delete(int studentId)
        {
            UniProvider.RemoveStudent(studentId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddConfirm(int studentId, string names, int groupId)
        {
            if (string.IsNullOrEmpty(names))
            {
                return RedirectToAction("Add", "Home", routeValues: new { studentId = studentId, error = true });
            }
            string[] namesArr = names.Split(' ');
            if (namesArr.Length != 2 && namesArr.Length != 3)
            {
                return RedirectToAction("Add", "Home", routeValues: new { studentId = studentId, error = true });
            }
            else if (namesArr.Length == 2)
            {
                if (studentId == 0)
                {
                    UniProvider.AddStudent(groupId, namesArr[1], namesArr[0]);

                }
                else
                {
                    UniProvider.UpdateStudent(studentId, groupId, namesArr[1], namesArr[0]);
                }
            }
            else
            {
                if (studentId == 0)
                {

                    UniProvider.AddStudent(groupId, namesArr[1], namesArr[0], namesArr[2]);
                }
                else
                {
                    UniProvider.UpdateStudent(studentId, groupId, namesArr[1], namesArr[0], namesArr[2]);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
