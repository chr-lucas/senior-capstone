using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
 Model: COURSE
 Description: Represents a course in the system.
    Each COURSE is associated with 1 TEXTBOOK, 1 instructor (USER), and 1 ORGANIZATION.
    A COURSE has a unique access code, a start date, and an end date.
Limitations: Does not account for a case where a course is created without an instructor assigned
    or a course with multiple instructors. Revisit this later as needed.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "CourseID ID")]
        public int CourseID { get; set; }

        // Does not account for a case where a course is created without an instructor assigned
        // or a course with multiple instructors. Revisit this later as needed.
        [Display(Name = "Intructor ID")]
        public int InstructorID { get; set; }

        [Required]
        [Display(Name = "Access Code")]
        public String AccessCode { get; set; }

        [Required]
        [Display(Name = "Textbook ID")]
        public int TextbookID { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Organization ID")]
        public int OrganizationID { get; set; }
    }
}
