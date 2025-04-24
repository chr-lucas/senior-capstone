using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NebulaTextbook.Data;
using NebulaTextbook.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.CookiePolicy;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
using System.Net;
using Microsoft.EntityFrameworkCore.Storage;


namespace NebulaTextbook.Pages
{
    public class SortingQuizModel : PageModel
    {
		private readonly NebulaTextbook.Data.NebulaTextbookContext _context;

		public SortingQuizModel(NebulaTextbook.Data.NebulaTextbookContext context)
		{
			_context = context;
		}

		public IList<Question> Question = new List<Question>();
		//public IList<Question> Question { get; set; } = default!;

		public async Task OnGetAsync()
        {
			
			var question = from m in _context.Question
						where m.QuizID == 2
						   select m;


			foreach (Question m in question) { 
				Console.WriteLine(m.ToString());
			}


			Question = await question.ToListAsync();
		}

		/*public async Task OnPostAsync()
		{
			int numCorrect = 0;
			var answers = from m in _context.Question
						  where m.QuizID == 2
						  select m.CorrectAnswer;



			var Question1 = Request.Form["1"];
			var Question2 = Request.Form["2"];
			var Question3 = Request.Form["3"];
			var Question4 = Request.Form["4"];

			string[] answersArray = answers.ToArray();


			if (answersArray[0] == Question1)
			{
				numCorrect++;
			}


			if (answersArray[1] == Question2)
			{

				numCorrect++;
			}


			if (answersArray[2] == Question3)
			{

				numCorrect++;
			}



			if (answersArray[3] == Question4)
			{

				numCorrect++;
			}


			double percent = ((double)numCorrect / 4) * 100;
			int finalScore = Convert.ToInt32(percent);
			int Integer = Convert.ToInt32(Request.Form["quizscore"]);

			StudentQuizScore score = new();
			{
				score.QuizID = 2;
				score.StudentID = 2;
				score.QuizScore = finalScore;

			}

			_context.StudentQuizScore.Add(score);
			await _context.SaveChangesAsync();
			//correct = "You got a score of " + finalScore;

			


		}*/

		

		public async Task<IActionResult> OnPostAsync()
		//public async Task OnPostAsync()
		{
			
			int numCorrect = 0;
			var answers = from m in _context.Question
						   where m.QuizID == 2
						   select m.CorrectAnswer;


			
			var Question1 = Request.Form["1"];
			var Question2 = Request.Form["2"];
			var Question3 = Request.Form["3"];	
			var Question4 = Request.Form["4"];

			string[] answersArray = answers.ToArray();
			
			
				if (answersArray[0] == Question1) { 
					numCorrect++;
				}


				if (answersArray[1] == Question2)
				{

					numCorrect++;
				}

			
				if (answersArray[2] == Question3)
				{

					numCorrect++;
				}
			

			
				if (answersArray[3] == Question4)
				{

					numCorrect++;
				}
			

			double percent = ((double)numCorrect/5)*100;
			int finalScore = Convert.ToInt32(percent);
			int Integer = Convert.ToInt32(Request.Form["quizscore"]);
				
			StudentQuizScore score = new();
			{
				score.QuizID = 2;
				score.StudentID = 2;
				score.QuizScore= finalScore;

			}
			
			_context.StudentQuizScore.Add(score);
			await _context.SaveChangesAsync();
		
			return NoContent();
		}

		

		private IActionResult NoContent()
		{
			return StatusCode(204);
		}

	}
}
