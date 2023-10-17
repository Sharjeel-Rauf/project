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
    public class StudentsController : ControllerBase
    {
        private readonly string _connectionString;

        public StudentsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //PERFORMING CRUD OPERATIONS//


        //READING OPERATION//

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            List<StudentsModel> Students = new List<StudentsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentsModel emp = new StudentsModel();
                        emp.StudentID = (int)reader["StudentID"];
                        emp.FirstName = reader["FirstName"].ToString();
                        emp.LastName = reader["LastName"].ToString();
                        emp.Age = (int)reader["Age"];
                        emp.CourseID = (int)reader["CourseID"];

                        Students.Add(emp);
                    }
                }
            }
            return Ok(Students);
        }

        // CREATING OPERATION

        [HttpPut]
        public IActionResult StudentsAdd(StudentsModel Student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("StudentsAdd", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", Student.StudentID);
                    command.Parameters.AddWithValue("@FirstName", Student.FirstName);
                    command.Parameters.AddWithValue("@LastName", Student.LastName);
                    command.Parameters.AddWithValue("@Age", Student.Age);
                    command.Parameters.AddWithValue("@CourseID", Student.CourseID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        //UPDATING OPERATION

        [HttpPut]
        [Route("studentupdate")]

        public IActionResult UpdateStudentinfo(int StudentID, StudentsModel Student)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateStudentinfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", Student.StudentID);
                    command.Parameters.AddWithValue("@FirstName", Student.FirstName);
                    command.Parameters.AddWithValue("@LastName", Student.LastName);
                    command.Parameters.AddWithValue("@Age", Student.Age);
                    command.Parameters.AddWithValue("@CourseID", Student.CourseID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok(Student);
        }

        [HttpDelete("{StudentID}")]
        public IActionResult DeleteStudent(int StudentID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }
    }
}
