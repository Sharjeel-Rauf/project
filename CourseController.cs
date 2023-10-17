using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using The_ultimate_stress.Model;
using System.Collections.Generic;

namespace The_ultimate_stress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly string _connectionString;

        public CourseController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //PERFORMING CRUD OPERATIONS//


        //READING OPERATION//

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            List<CourseModel> Courses = new List<CourseModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CourseModel emp = new CourseModel();
                        emp.CourseID = (int)reader["CourseID"];
                        emp.CourseName = reader["CourseName"].ToString();
                        Courses.Add(emp);
                    }
                }
            }
            return Ok(Courses);
        }
        // CREATING OPERATION

        [HttpPut]
        public IActionResult AddCourses(CourseModel Courses)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", Courses.CourseID);
                    command.Parameters.AddWithValue("@CourseName", Courses.CourseName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        //UPDATING OPERATION
        [HttpPut]
        [Route("courseupdate")]
        public IActionResult UpdateCoursesinfo(int CourseID, CourseModel Courses)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateCoursesinfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", Courses.CourseID);
                    command.Parameters.AddWithValue("@CourseName", Courses.CourseName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok(Courses);
        }

        [HttpDelete("{CourseID}")]
        public IActionResult DeleteCourses(int CourseID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteCourses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", CourseID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }



    }
}
