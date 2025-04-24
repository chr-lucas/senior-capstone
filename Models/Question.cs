using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: QUESTION
Description: Represents a QUESTION from a QUIZ.
    Each QUESTION is associated with 1 QUIZ.
    A QUESTION has a the question text, four possible answers, and the correct answer.
Limitations: Current implmentation only supports 4 answer multiple choice.
    QUESTION does not have an individual score, so a QUIZ is scored as num_correct / num_questions

Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Question ID")]
        public int QuestionID { get; set; }

        [Required]
        [Display(Name = "Quiz ID")]
        public int QuizID { get; set; }

        [Required]
        [Display(Name = "Question Text")]
        public String QuestionText { get; set; } = null!;

        [Required]
        [Display(Name = "Answer 1")]
        public String Answer1 { get; set; } = null!;

        [Required]
        [Display(Name = "Answer 2")]
        public String Answer2 { get; set; } = null!;

        [Required]
        [Display(Name = "Answer 3")]
        public String Answer3 { get; set; } = null!;

        [Required]
        [Display(Name = "Answer 4")]
        public String Answer4 { get; set; } = null!;

        [Required]
        [Display(Name = "Answer Text")]
        public String CorrectAnswer { get; set; } = null!;
    } 
}
