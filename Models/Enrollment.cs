using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
 Model: ENROLLMENT
 Description: Represents an ENROLLMENT of a student (USER) in a COURSE.
    Each ENROLLMENT is associated with 1 student (USER) and 1 COURSE.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Enrollment ID")]
        public int EnrollmentID {  get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }

    }
}
