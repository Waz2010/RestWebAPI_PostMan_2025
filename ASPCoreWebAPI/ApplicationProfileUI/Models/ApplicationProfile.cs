using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationProfileUI.Models
{
    public class ApplicationProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [Column("firstName")]
        public string FirstName { get; set; } = null!;

        [Column("middleName")]
        public string MiddleName { get; set; } = null!;

        [Required(ErrorMessage = "The Last Name field is required.")]
        [Column("lastName")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "The Phone field is required.")]
        [Column("phone")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "The Email field is required.")]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The Address Line 1 field is required.")]
        [Column("address1")]
        public string Address1 { get; set; } = null!;

        [Column("address2")]
        public string Address2 { get; set; } = null!;

        [Column("city")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "The State field is required.")]
        [Column("state")]
        public string State { get; set; } = null!;

        [Required(ErrorMessage = "The Zip Code field is required.")]
        [Column("zipcode")]
        public string Zipcode { get; set; } = null!;

        [Required(ErrorMessage = "The Country field is required.")]
        [Column("country")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "The Experience field is required.")]
        [Column("experience")]
        public string Experience { get; set; } = null!;

        [Required(ErrorMessage = "The Education field is required.")]
        [Column("education")]
        public string Education { get; set; } = null!;

        [Required(ErrorMessage = "The Education Subject field is required.")]
        [Column("educationSubject")]
        public string EducationSubject { get; set; } = null!;

        [Required(ErrorMessage = "The Worked For Us field is required.")]
        [Column("workedForUs")]
        public bool WorkedForUs { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        [Column("nationality")]
        public string Nationality { get; set; } = null!;

        [Required(ErrorMessage = "The Notice Period field is required.")]
        [Column("noticePeriod")]
        public string NoticePeriod { get; set; } = null!;

        [Required(ErrorMessage = "The Full Name field is required.")]
        [Column("writeFullName")]
        public string WriteFullName { get; set; } = null!;

        [Required(ErrorMessage = "The Resume URL field is required.")]
        [Column("attachedResumeUrl")]
        public string AttachedResumeUrl { get; set; } = null!;

        [Column("createdOn")]
        public DateTime CreatedOn { get; set; }

        [Column("createdBy")]
        public string CreatedBy { get; set; } = "SupperAdmin"; // Default value

        [Column("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [Column("modifiedBy")]
        public string? ModifiedBy { get; set; } = null!;

        [Column("deletedOn")]
        public DateTime? DeletedOn { get; set; }

        [Column("deletedBy")]
        public string? DeletedBy { get; set; }
    }
}