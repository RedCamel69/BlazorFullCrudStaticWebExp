using Api.Services.HelperService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHelperService _helperService;

        public StudentService(
            ApplicationDbContext context, IHelperService helperService
            )
        {
            _context = context;
            _helperService = helperService;
        }

        public async Task<ServiceResponse<Student>> CreateStudent(Student student)
        {
            var response = new ServiceResponse<Student>();

            try
            {
                student.Language = null; //ef workaround to stop new language being created
                _context.Students.Add(student);

                await _context.SaveChangesAsync();

                response.Data = student;
                response.Message = "Successfully added new tutor";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Failed to add new tutor. Error {ex.Message}";
            }

            return response;
        }

        public ServiceResponse<bool> DeleteStudent(int Id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var dbStudent = _context.Students.FirstOrDefault(x => x.StudentId == Id);

                if (dbStudent == null)
                {
                    response.Success = false;
                    response.Data = false;
                    response.Message = "Student not found.";
                }
                else
                {
                    //dbTutor.Deleted = true;
                    _context.Students.Remove(dbStudent);
                    _context.SaveChanges();

                    response.Success = true;
                    response.Data = true;
                    response.Message = $"Student {dbStudent.StudentId} {dbStudent.FirstName + " " + dbStudent.LastName} deleted.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = $"Error deleting student {Id} : {ex.Message} ";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteStudentAsync(int Id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var dbStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == Id);
                if (dbStudent == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Student not found."
                    };
                }

                //dbTutor.Deleted = true;
                _context.Students.Remove(dbStudent);

                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = true;
                response.Message = $"Student {dbStudent.StudentId} {dbStudent.FirstName + " " + dbStudent.LastName} deleted.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = $"Error deleting student {Id} : {ex.Message} ";
            }
            return response;
        }

        public ServiceResponse<Student> GetStudent(int Id)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                var student = _context.Students.FirstOrDefault(x => x.StudentId == Id);

                if (student == null)
                {
                    response.Data = student;
                    response.Success = false;
                    response.Message = $"Student with id of {Id} could not be found.";
                }
                else
                {
                    response.Data = student;
                    response.Success = true;
                    response.Message = "Students successfully retrieved";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Students could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<Student>> GetStudentAsync(int Id)
        {
            var response = new ServiceResponse<Student>();
            try
            {

                if (_helperService.GetAppSetting("StudentService-GetStudents"))
                    throw new Exception("Test Exception");

                var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == Id);

                if (student == null)
                {
                    response.Data = student;
                    response.Success = false;
                    response.Message = $"Student with id of {Id} could not be found.";
                }
                else
                {
                    response.Data = student;
                    response.Success = true;
                    response.Message = "Students successfully retrieved";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Students could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public ServiceResponse<List<Student>> GetStudents()
        {
            var response = new ServiceResponse<List<Student>>();
            try
            {
                response.Data = _context.Students
                    .Include(x => x.Language)
                    .ToList();
                response.Success = true;
                response.Message = "Students successfully retrieved";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Students could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;

        }

        public async Task<ServiceResponse<List<Student>>> GetStudentsAsync()
        {
            var response = new ServiceResponse<List<Student>>();
            try
            {
                response.Data = await _context.Students
                    .Include(x => x.Language)
                    .ToListAsync();
                response.Success = true;
                response.Message = "Students successfully retrieved";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Students could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;

        }

        public async Task<ServiceResponse<Student>> UpdateStudent(Student student)
        {
            var response = new ServiceResponse<Student>();

            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                response.Data = student;
                response.Message = "Successfully updated student";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Failed to update student. Error {ex.Message}";
            }

            return response;

        }
    }
}
