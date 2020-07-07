using System;
using System.Collections.Generic;
namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Get a student info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Students GetStudents(int id);

        /// <summary>
        /// Get all students info
        /// </summary>
        /// <returns></returns>
        IEnumerable<Students> GetAllStudents();

        /// <summary>
        /// add a new student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Students Add(Students student);

        /// <summary>
        /// update a student info
        /// </summary>
        /// <param name="updataStudent"></param>
        /// <returns></returns>
        Students Update(Students updateStudent);

        /// <summary>
        /// delete a student
        /// </summary>
        /// <param name="deleteStudent"></param>
        /// <returns></returns>
        Students Delete(int id);
    }
}
