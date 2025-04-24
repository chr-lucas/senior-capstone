using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: TEXTBOOK
Description: Represents a TEXTBOOK in the system.
    Each TEXTBOOK has a title, author, subject, and published date.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Textbook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Textbook ID")]
        public int TextbookID { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Author { get; set; }

        [Required]
        public String Subject { get; set; }

        [Required]
        [Display(Name = "Date Published")]
        public DateTime PublishedDate { get; set; }

    }
}
