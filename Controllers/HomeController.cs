using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting.Internal;
using StudentManagement.Models;
using StudentManagement.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagement.Controllers
{

    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _hostingEnviroment;

        // GET: /<controller>/
        // 使用构造函数注入的方式注入 IStudentRepository
        public HomeController(IStudentRepository studentRepository,IWebHostEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnviroment = hostingEnvironment;
        }


        public ViewResult Index()
        {
            var student = _studentRepository.GetAllStudents();
            return View(student);
        }


        public ViewResult Details(int? id)
        {
            //Students model = _studentRepository.GetStudents(2);

            //ViewBag.PageTitle = "Student Details";
            //ViewBag.student = model;
            //ViewData["Student"] = model;

            HomeDetailViewModels homeDetailViewModels = new HomeDetailViewModels()
            //homeDetailViewModels.Students = _studentRepository.GetStudents(2);
            //homeDetailViewModels.PageTitle = "Student Details";
            {
                Students = _studentRepository.GetStudents(id ?? 1),
                PageTitle = "Student Details"
            };

            return View(homeDetailViewModels);
        }


        public ViewResult Edit(int? id)
        {

            HomeDetailViewModels homeDetailViewModels = new HomeDetailViewModels()
            //homeDetailViewModels.Students = _studentRepository.GetStudents(2);
            //homeDetailViewModels.PageTitle = "Student Details";
            {
                Students = _studentRepository.GetStudents(id ?? 1),
                PageTitle = "Student Details"
            };

            return View(homeDetailViewModels);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = null;
                if (model.Photopath != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "images");
                    uniqueFilename = Guid.NewGuid().ToString() + "_" + model.Photopath.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFilename);
                    model.Photopath.CopyTo(new FileStream(filepath, FileMode.Create));
                }




                Students newstudent = new Students
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    Photopath = uniqueFilename
                };
                _studentRepository.Add(newstudent);
                return RedirectToAction("Details", new { id = newstudent.Id });
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            if (_studentRepository.GetStudents(id) != null) {
                _studentRepository.Delete(id);
            }
            var student = _studentRepository.GetAllStudents();
            return View(student);
        }
    }
}
