using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public class studentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbcontext;

        public studentRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Students Add(Students student)
        {
            _dbcontext.students.Add(student);
            _dbcontext.SaveChanges();
            return student;
        }

        public Students Delete(int id)
        {
            Students student = _dbcontext.students.Find(id);

            if (student != null)
            {
                _dbcontext.students.Remove(student);
                _dbcontext.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Students> GetAllStudents()
        {
            return _dbcontext.students;
        }

        public Students GetStudents(int id)
        {
            return _dbcontext.students.Find(id);
        }

        public Students Update(Students updateStudent)
        {
            var student = _dbcontext.students.Attach(updateStudent);

            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _dbcontext.SaveChanges();

            return updateStudent;
        }
    }
}
