using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: USER
Description: Represents a USER of the app.
    A USER has a username, password, email, first name, and last name.
Limitations: Currently unimplemented.
    Current design stores USER passwords in plaintext.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        // CURRENTLY PLAINTEXT PASSWORD!!!!
        public String Password { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
    }
}
