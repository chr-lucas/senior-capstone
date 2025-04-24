using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: USER_ROLE
Description: An intersectional table that links USER and ROLE. Intended to be used for USER access control.
Limitations: Currently unimplemented.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User Role ID")]
        public int UserRoleID { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }
    }
}
