using Microsoft.AspNetCore.Mvc;
using BusinessWebAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using RestSharp;


namespace BusinessWebAPI.Controllers
{
    public class GetAllController : Controller
    {
        private readonly HttpClient _httpClient;

        public GetAllController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("student/detail/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5238/api/GetAll/student/{id}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Student student = JsonConvert.DeserializeObject<Student>(content);
                Console.WriteLine($"Response from DatabaseWebAPI: {response.StatusCode} - {content}");
                return Ok(student);
            }
            else
            {
                Console.WriteLine($"Failed to connect to DatabaseWebAPI: {response.StatusCode}");
                return NotFound(new { Message = "Student not found" });
            }
        }

        // Fetch all students from DatabaseAPI
        [HttpGet("students")]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5238/api/GetAll/students"); 
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Student>>(content);
        }

        // Fetch a student by ID from DatabaseAPI
        

    }
}
