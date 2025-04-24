using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NebulaTextbook.Data;
using NebulaTextbook.Models;

namespace NebulaTextbook.Pages.UserScaffold
{
    public class CreateModel : PageModel
    {
        private readonly NebulaTextbookContext _context;

        public CreateModel(NebulaTextbookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User UserAdd { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.User.Add(new Models.User("Braxton"));
            
           await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
