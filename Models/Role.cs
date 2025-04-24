using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: ROLE
Description: Represents a ROLE. Intended to be used for USER access control.
Limitations: Currently unimplemented.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public String RoleName{ get; set; }
    }
}
