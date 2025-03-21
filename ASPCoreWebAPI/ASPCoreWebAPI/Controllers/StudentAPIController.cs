using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly UsersDBContext conext;

        public StudentAPIController(UsersDBContext _Conext)
        {
            this.conext = _Conext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var studentdata = await conext.Students.ToListAsync();
            return Ok(studentdata);

        }

        [HttpGet("id")]
        public async Task<ActionResult<List<Student>>> GetStudentsById(int id)
        {
            var studentdata = await conext.Students.FindAsync(id);
            if(studentdata == null)
            {
                return NotFound();
            }
             return Ok(studentdata);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudent(Student studentss)
        {
            await conext.AddAsync(studentss);
            await conext.SaveChangesAsync();
            return Ok(studentss);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(int id, Student std)
        {
            if(id != std.Id)
            {
                return BadRequest();
            }
            conext.Entry(std).State = EntityState.Modified;
            await conext.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var std = await conext.Students.FindAsync(id)
;
            if (std ==null)
            {
                return BadRequest();
            }
            conext.Students.Remove(std);
            await conext.SaveChangesAsync();
            return Ok(std);
        }
    }
}
