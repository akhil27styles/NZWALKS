using Microsoft.AspNetCore.Mvc;

namespace NZwalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students = {"Alice", "Bob", "Charlie" };
            return Ok(students);
        }
    }
}
