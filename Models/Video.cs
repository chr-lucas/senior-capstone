using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: VIDEO
Description: Metadata and storage data for a VIDEO used in a CHAPTER.
    A VIDEO has a title, duration, file path, upload date, and uploaded by USER.
Limitations: Unknown usability. The original design document imagined every chapter
    pulling data from the database, but this has not been the case during development.
    Such metadata is widely unused, and filepathing can be hardcoded into the chapter in
    which a video appears.
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Video ID")]
        public int VideoID { get; set; }

        [Required]
        public float Duration { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        [Display(Name = "File Path")]
        public String FilePath { get; set; }

        [Required]
        [Display(Name = "Date Uploaded")]
        public DateTime UploadDate { get; set; }

        [Required]
        [Display(Name = "Uploaded By")]
        public int UploadedBy { get; set; }

        [Required]
        [Display(Name = "Thumbnail Path")]
        public String? ThumbnailPath { get; set; }
    }
}
