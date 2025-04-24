//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using NebulaTextbook.Data;
//using NebulaTextbook.Models;

//namespace NebulaTextbook.Pages
//{
//    public class IndexModel : PageModel
//    {
//        private readonly NebulaTextbook.Data.NebulaTextbookContext _context;

//        public IndexModel(NebulaTextbook.Data.NebulaTextbookContext context)
//        {
//            _context = context;
//        }

//        public IList<User> User { get;set; } = default!;

//        public async Task OnGetAsync()
//        {
//            User = await _context.User.ToListAsync();
//        }
//    }
//}
