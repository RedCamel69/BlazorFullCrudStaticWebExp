using Api.Services.HelperService;
using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHelperService _helperService;

        public CourseService(
            ApplicationDbContext context, IHelperService helperService
            )
        {
            _context = context;
            _helperService = helperService;
        }

        public async Task<ServiceResponse<Course>> CreateCourseAsync(Course course)
        {
            var response = new ServiceResponse<Course>();

            try
            {
                course.Language = null; //ef workaround to stop new language being created
                _context.Courses.Add(course);

                await _context.SaveChangesAsync();

                response.Data = course;
                response.Message = "Successfully added new course";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Failed to add new course. Error {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCourseAsync(int Id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var dbCourse = _context.Courses.FirstOrDefault(x => x.CourseId== Id);

                if (dbCourse == null)
                {
                    response.Success = false;
                    response.Data = false;
                    response.Message = "Course not found.";
                }
                else
                {
                    //dbTutor.Deleted = true;
                    _context.Courses.Remove(dbCourse);
                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Data = true;
                    response.Message = $"Course {dbCourse.CourseId} {dbCourse.Name} deleted.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = $"Error deleting course {Id} : {ex.Message} ";
            }

            return response;

        }


        public ServiceResponse<bool> DeleteCourse(int Id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var dbCourse = _context.Courses.FirstOrDefault(x => x.CourseId == Id);

                if (dbCourse == null)
                {
                    response.Success = false;
                    response.Data = false;
                    response.Message = "Course not found.";
                }
                else
                {
                    //dbTutor.Deleted = true;
                    _context.Courses.Remove(dbCourse);
                    _context.SaveChanges();

                    response.Success = true;
                    response.Data = true;
                    response.Message = $"Course {dbCourse.CourseId} {dbCourse.Name} deleted.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = $"Error deleting course {Id} : {ex.Message} ";
            }

            return response;

        }

        public ServiceResponse<Course> GetCourse(int Id)
        {
            var response = new ServiceResponse<Course>();
            try
            {
                var course = _context.Courses.FirstOrDefault(x => x.CourseId == Id);

                if (course == null)
                {
                    response.Data = course;
                    response.Success = false;
                    response.Message = $"Course with id of {Id} could not be found.";
                }
                else
                {
                    response.Data = course;
                    response.Success = true;
                    response.Message = "Course successfully retrieved";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Course could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<Course>> GetCourseAsync(int Id)
        {
            var response = new ServiceResponse<Course>();
            try
            {

                if (_helperService.GetAppSetting("CourseService_GetCourse_Throw_Test_Exception"))
                    throw new Exception("Test Exception");

                var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == Id);

                if (course == null)
                {
                    response.Data = course;
                    response.Success = false;
                    response.Message = $"Course with id of {Id} could not be found.";
                }
                else
                {
                    response.Data = course;
                    response.Success = true;
                    response.Message = "Courses successfully retrieved";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Courses could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public ServiceResponse<List<Course>> GetCourses()
        {
            var response = new ServiceResponse<List<Course>>();
            try
            {
                response.Data = _context.Courses
                    .Include(x => x.Language)
                    .ToList();
                response.Success = true;
                response.Message = "Courses successfully retrieved";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Courses could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Course>>> GetCoursesAsync()
        {
            var response = new ServiceResponse<List<Course>>();
            try
            {
                response.Data = await _context.Courses
                    .Include(x => x.Language)
                    .ToListAsync();
                response.Success = true;
                response.Message = "Courses successfully retrieved";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = "Courses could not be retrieved : " + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public ServiceResponse<Course> UpdateCourse(Course course)
        {
            var response = new ServiceResponse<Course>();

            try
            {
                _context.Courses.Update(course);
                _context.SaveChanges();

                response.Data = course;
                response.Message = "Successfully updated student";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Failed to update course. Error {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<Course>> UpdateCourseAsync(Course course)
        {
            var response = new ServiceResponse<Course>();

            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                response.Data = course;
                response.Message = "Successfully updated student";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = $"Failed to update course. Error {ex.Message}";
            }

            return response;
        }

       
    }
}
