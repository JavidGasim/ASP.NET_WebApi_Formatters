using Microsoft.AspNetCore.Mvc;
using WebApiDemo3_22_10.Dtos;
using WebApiDemo3_22_10.Entities;
using WebApiDemo3_22_10.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo3_22_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            var items = await _studentService.GetAllAsync();
            var dataToReturn = items.Select(s =>
            {
                return new StudentDto
                {
                    Id = s.Id,
                    Age = s.Age,
                    Fullname = s.Fullname,
                    Score = s.Score,
                    SeriaNo = s.SeriaNo,
                };
            });
            return dataToReturn;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _studentService.GetAsync(s => s.Id == id);
            if (item == null) return NotFound();
            var dto = new StudentDto
            {
                Id = item.Id,
                Age = item.Age,
                Fullname = item.Fullname,
                Score = item.Score,
                SeriaNo = item.SeriaNo,
            };
            return Ok(dto);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentAddDto value)
        {
            if (value == null)
            {
                return NotFound();
            }

            var student = new Student
            {
                Fullname = value.Fullname,
                SeriaNo = value.SeriaNo,
                Age = value.Age,
                Score = value.Score
            };

            await _studentService.AddAsync(student);
            return Ok(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentAddDto value)
        {
            var student = await _studentService.GetAsync(s => s.Id == id);
            if (student == null || value == null)
            {
                return NotFound();
            }

            student.Fullname = value.Fullname;
            student.SeriaNo = value.SeriaNo;
            student.Age = value.Age;
            student.Score = value.Score;

            await _studentService.UpdateAsync(student);

            return Ok(value);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentService.DeleteAsync(student);
            return NoContent();
        }
    }
}

