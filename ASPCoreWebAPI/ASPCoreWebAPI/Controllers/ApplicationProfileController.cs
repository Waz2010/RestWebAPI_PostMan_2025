using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Uncommented to enable authorization
    public class ApplicationProfileController : ControllerBase
    {
        private readonly UsersDBContext _context;
        private readonly ILogger<ApplicationProfileController> _logger;

        public ApplicationProfileController(UsersDBContext context, ILogger<ApplicationProfileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationProfile>>> GetApplicationProfiles()
        {
            return Ok(await _context.ApplicationProfiles.Where(x => x.DeletedOn == null).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationProfile>> GetApplicationProfilesById(int id)
        {
            var profile = await _context.ApplicationProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationProfile>> CreateApplicationProfile([FromBody] ApplicationProfile applicationProfile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            applicationProfile.CreatedOn = DateTime.UtcNow;
            await _context.AddAsync(applicationProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApplicationProfilesById), new { id = applicationProfile.Id }, applicationProfile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationProfile>> UpdateApplicationProfile(int id, [FromBody] ApplicationProfile updatedProfile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = await _context.ApplicationProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            // Update fields
            profile.FirstName = updatedProfile.FirstName;
            profile.MiddleName = updatedProfile.MiddleName;
            profile.LastName = updatedProfile.LastName;
            profile.Phone = updatedProfile.Phone;
            profile.Email = updatedProfile.Email;
            profile.Address1 = updatedProfile.Address1;
            profile.Address2 = updatedProfile.Address2;
            profile.City = updatedProfile.City;
            profile.State = updatedProfile.State;
            profile.Zipcode = updatedProfile.Zipcode;
            profile.Country = updatedProfile.Country;
            profile.Experience = updatedProfile.Experience;
            profile.Education = updatedProfile.Education;
            profile.EducationSubject = updatedProfile.EducationSubject;
            profile.WorkedForUs = updatedProfile.WorkedForUs;
            profile.Nationality = updatedProfile.Nationality;
            profile.NoticePeriod = updatedProfile.NoticePeriod;
            profile.WriteFullName = updatedProfile.WriteFullName;
            profile.AttachedResumeUrl = updatedProfile.AttachedResumeUrl;
            profile.ModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "ApplicationProfile updated successfully",
                applicationProfile = profile
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ApplicationProfile>>> DeleteApplicationProfile(int id)
        {
            var profile = await _context.ApplicationProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(profile);
        }
    }
}
