using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
 Model: ORGANIZATION
 Description: Represents an ORGANIZATION, such as a department or school.
    Each ORGANIZATION is associated with 1 owner (USER).
    An ORGANIZATION has a name, and an optional short name or abbreviation.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Organization ID")]
        public int OrganizationID {  get; set; }

        [Required]
        [Display(Name = "Owner ID")]
        public int OwnerID { get; set; }

        [Required]
        public String Name { get; set; }

        [Display(Name = "Name (Short)")]
        public String? NameShort { get; set; }
    }
}
