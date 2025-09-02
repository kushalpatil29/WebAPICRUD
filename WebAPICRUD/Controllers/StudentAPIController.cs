using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly CodeFirstContext context;
        public StudentAPIController(CodeFirstContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentbyId(int id)
        {
            var stud = await context.Students.FindAsync(id);
            if (stud == null)
            {
                return NotFound();
            }
            return stud;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student std)
        {
           if(id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> deletestud(int id)
        {
            var s1 = await context.Students.FindAsync(id);
            if(s1 == null)
            {
                return NotFound();
            }
            context.Students.Remove(s1);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
