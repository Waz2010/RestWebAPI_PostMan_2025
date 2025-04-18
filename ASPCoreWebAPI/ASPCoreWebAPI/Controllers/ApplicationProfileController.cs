using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationProfileController : ControllerBase
    {
        private readonly UsersDBContext _context;
        public ApplicationProfileController(UsersDBContext _Conext)
        {
            _context = _Conext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationProfile>>> GetApplicationProfiles()
        {
            var applicationProfiledata = await _context.ApplicationProfiles.Where(x => x.DeletedOn == null).ToListAsync();
            return Ok(applicationProfiledata);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationProfile>> GetApplicationProfilesById(int id)
        {
            var applicationProfiledata = await _context.ApplicationProfiles.FindAsync(id);
            if (applicationProfiledata == null)
            {
                return NotFound();
            }
            return Ok(applicationProfiledata);
        }

        [HttpPost]
        public async Task<ActionResult<List<ApplicationProfile>>> CreateApplicationProfile(ApplicationProfile applicationProfiless)
        {
            applicationProfiless.CreatedOn = DateTime.Now;
            //applicationProfiless.CreatedBy = DateTime. TO DO
            await _context.AddAsync(applicationProfiless);
            await _context.SaveChangesAsync();
            return Ok(applicationProfiless);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationProfile>> UpdateApplicationProfile(int id, ApplicationProfile std)
        {
            var existingApplicationProfile = await _context.ApplicationProfiles.FindAsync(id);
            if (existingApplicationProfile == null) { return NotFound(); }

            // Update only the necessary fields, keeping the ID unchanged
            existingApplicationProfile.FirstName = std.FirstName;
            existingApplicationProfile.MiddleName = std.MiddleName;
            existingApplicationProfile.LastName = std.LastName;
            existingApplicationProfile.Phone = std.Phone;
            existingApplicationProfile.Email = std.Email;
            existingApplicationProfile.Address1 = std.Address1;
            existingApplicationProfile.Address2 = std.Address2;
            existingApplicationProfile.City = std.City;
            existingApplicationProfile.State = std.State;
            existingApplicationProfile.Zipcode = std.Zipcode;
            existingApplicationProfile.Country = std.Country;
            existingApplicationProfile.Experience = std.Experience;
            existingApplicationProfile.Education = std.Education;
            existingApplicationProfile.EducationSubject = std.EducationSubject;
            existingApplicationProfile.WorkedForUs = std.WorkedForUs;
            existingApplicationProfile.Nationality = std.Nationality;
            existingApplicationProfile.NoticePeriod = std.NoticePeriod;
            existingApplicationProfile.WriteFullName = std.WriteFullName;
            existingApplicationProfile.AttachedResumeUrl = std.AttachedResumeUrl;
            existingApplicationProfile.ModifiedOn = DateTime.Now;
            //existingApplicationProfile.ModifiedBy = std.ModifiedBy; TO DO



            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "ApplicationProfile updated successfully",
                applicationProfile = existingApplicationProfile
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ApplicationProfile>>> DeleteApplicationProfile(int id)
        {
            var std = await _context.ApplicationProfiles.FindAsync(id)

;
            if (std == null)
            {
                return BadRequest();
            }
            std.DeletedOn = DateTime.Now;
           //std.DeletedBy = std.DeletedBy;
            //_context.ApplicationProfiles.Remove(std);
            await _context.SaveChangesAsync();
            return Ok(std);
        }
    }
}
