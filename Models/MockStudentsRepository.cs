using System;
using System.Linq;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public class MockStudentsRepository:IStudentRepository
    {
        private List<Students> _studentList;
        public MockStudentsRepository()
        {
            _studentList = new List<Students>()
          {
              new Students(){Id = 1,Name="Tang San",ClassName=ClassNameEnum.FirsGrade,Email = "TangSan@gmail.com"},
              new Students(){Id = 2,Name="Xiao Wu",ClassName=ClassNameEnum.SecondGrade,Email = "XiaoWu@gmail.com"},
              new Students(){Id = 3,Name="Rong Rong",ClassName=ClassNameEnum.GradeThree,Email = "RongRong@gmail.com"}

          };
        }

        public Students Add(Students student)
        {
            student.Id = _studentList.Max(s => s.Id) + 1;
            _studentList.Add(student);
            return student;
        }

        public Students Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Students> GetAllStudents()
        {
            return _studentList;
        }

        public Students GetStudents(int id)
        {
            return _studentList.FirstOrDefault(a => a.Id == id);
        }

        public Students Update(Students updateStudent)
        {
            Students student = _studentList.FirstOrDefault(s => s.Id == updateStudent.Id);
            if (student != null)
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.ClassName = updateStudent.ClassName;
            }
            return student;

        }

    }
}
