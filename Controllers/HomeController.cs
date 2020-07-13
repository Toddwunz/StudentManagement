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


        public ViewResult Details(int id)
        {
            
            var Student = _studentRepository.GetStudents(id);
            //create a viewmode instance and assign vaule to property
            if (Student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound",id);
            }

            HomeDetailViewModels homeDetailViewModels = new HomeDetailViewModels()
            {
                Students = Student,
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
                uniqueFilename = ProcessUploadFile(model);

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


        [HttpGet]
        public ViewResult Edit(int id)
        {
            var student = _studentRepository.GetStudents(id);
            
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.Photopath
            };
            return View(studentEditViewModel);

        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _studentRepository.GetStudents(model.Id);
                student.Name = model.Name;
                student.Email = model.Email;
                student.ClassName = model.ClassName;
                if (model.Photopath != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath= Path.Combine(_hostingEnviroment.WebRootPath, "images",model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                }

                student.Photopath = ProcessUploadFile(model);

                var updatestudent = _studentRepository.Update(student);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (_studentRepository.GetStudents(id) != null) {
                _studentRepository.Delete(id);
            }
            var student = _studentRepository.GetAllStudents();
            return View(student);
        }
        /// <summary>
        /// upload chosed files
        /// </summary>
        /// <returns>new file path</returns>
        public string ProcessUploadFile(StudentCreateViewModel model)
        {
            string uniqueFilename = null;
            if (model.Photopath.Count > 0)
            {
                foreach (var photo in model.Photopath)
                {
                    string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "images");
                    uniqueFilename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFilename);
                    using (var fileStream= new FileStream(filepath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                        
                }
            }

            return uniqueFilename;
        }
   
    }
}
