using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/*
Model: STUDENTQUIZSCORE
Description: An intersectional table that links student (USER) score to a QUIZ.
Limitations: Does not account for multiple scores on the same QUIZ.
	Potential solution is to add a timestamp to the score to differentiate between scores.
    Another potential solution is to add a score history table that stores all scores for a student.
    Currently stores an INT when QUIZ has a total score using FLOAT. (Let unchanged to not break code for final demo. Sorry!)
Note: Foreign keys DO NOT use the [ForeignKey] attribute.
    This is because the foreign key is not a property of the class, but rather an int
    used as a navigation property.
 */
namespace NebulaTextbook.Models
{

	public class StudentQuizScore
	{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Score ID")]
        public int ScoreID { get; set; }

        [Required]
        [Display(Name = "Quiz ID")]
        public int QuizID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Quiz Score")]
        public int QuizScore { get; set; } // Should be a float, right?

	}
}
