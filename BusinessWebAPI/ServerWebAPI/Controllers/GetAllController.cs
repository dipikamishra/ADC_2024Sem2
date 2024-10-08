using Microsoft.AspNetCore.Mvc;
using ServerWebAPI.Models;

namespace ServerWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GetAllController : Controller
    {

        [HttpGet("students")]
        public IEnumerable<Student> Details()
        {
            List<Student> students = StudentList.AllStudents();
            return students;
        }

        [HttpGet("student/{id}")]
        public IActionResult Detail(int id)
        {
            Student student = StudentList.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(student) { StatusCode = 200 };
            }

        }

        [HttpPost]
        public IActionResult Detail([FromBody] Student student)
        {
            StudentList.AddStudent(student);
            var response = new { Message = "Student created successfully" };
            return new ObjectResult(response)
            {
                StatusCode = 200,
                ContentTypes = { "application/json" }
            };
        }

    }
}
