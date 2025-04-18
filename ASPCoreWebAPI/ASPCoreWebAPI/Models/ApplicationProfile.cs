using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models
{
    public partial class ApplicationProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Experience { get; set; } = null!;
        public string Education { get; set; } = null!;
        public string EducationSubject { get; set; } = null!;
        public bool WorkedForUs { get; set; }
        public string Nationality { get; set; } = null!;
        public string NoticePeriod { get; set; } = null!;
        public string WriteFullName { get; set; } = null!;
        public string AttachedResumeUrl { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
