using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
 Model: CHAPTER
 Description: Represents a CHAPTER from a TEXTBOOK.
    Each CHAPTER is associated with 1 TEXTBOOK.
    A CHAPTER has a name for the chapter.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Chapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Chapter ID")]
        public int ChapterID { get; set; }

        [Required]
        [Display(Name = "Textbook ID")]
        public int TextbookID { get; set; }

        [Required]
        [Display(Name = "Chapter Name")]
        public String ChapterName { get; set; }
    }
}
