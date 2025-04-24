using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: QUIZ
Description: Represents a QUIZ.
    A QUIZ has a description, title, and score.
Limitations: Current implmentation only supports 4 answer multiple choice.
    QUESTION objects do not have an individual score, so a QUIZ is scored as num_correct / num_questions
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Quiz ID")]
        public int QuizID { get; set; }

        [Required]
        [Display(Name = "Max Score")]
        public float Score { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Description { get; set; }
    }
}
