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

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsById(int id)
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
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)
        {
            var existingStudent = await conext.Students.FindAsync(id);
            if (existingStudent == null) {return NotFound();}

            // Update only the necessary fields, keeping the ID unchanged
            existingStudent.StudentName = std.StudentName;
            existingStudent.StudentGender = std.StudentGender;
            existingStudent.Age = std.Age;
            existingStudent.Standard = std.Standard;
            existingStudent.FatherName = std.FatherName;


            await conext.SaveChangesAsync();

            return Ok(new
            {
                message = "Student updated successfully",
                student = existingStudent
            });
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
