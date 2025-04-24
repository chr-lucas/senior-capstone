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
    public class chapterModel : PageModel
    {

		private readonly NebulaTextbook.Data.NebulaTextbookContext _context;

		public chapterModel(NebulaTextbook.Data.NebulaTextbookContext context)
		{
			_context = context;
		}



		public IList<Question> Question { get; set; } = new List<Question>();
		


		public async Task OnGetAsync()
		{


			var query = from m in _context.Question
						where m.QuizID == 3
						select m;

			foreach (Question m in query)
			{
				Console.WriteLine(m.ToString());
			}

			Question = await query.ToListAsync();


			
		}


		public async Task<IActionResult> OnPostAsync()
		{

			int numCorrect = 0;
			var answers = from m in _context.Question
						  where m.QuizID == 3
						  select m.CorrectAnswer;



			var Question1 = Request.Form["1"];
			var Question2 = Request.Form["2"];
			var Question3 = Request.Form["3"];
			var Question4 = Request.Form["4"];
			var Question5 = Request.Form["5"];
			var Question6 = Request.Form["6"];

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

			if (answersArray[4] == Question5)
			{

				numCorrect++;
			}

			if (answersArray[5] == Question6)
			{

				numCorrect++;
			}

			double percent = ((double)numCorrect / 6) * 100;
			int finalScore = Convert.ToInt32(percent);
			int Integer = Convert.ToInt32(Request.Form["quizscore"]);

			StudentQuizScore score = new();
			{
				score.QuizID = 3;
				score.StudentID = 6;
				score.QuizScore = finalScore;

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

