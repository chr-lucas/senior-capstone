using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NebulaTextbook.Models;

namespace NebulaTextbook.Pages
{
    public class EmbedVideoModel(NebulaTextbook.Data.NebulaTextbookContext context) : PageModel
    {
        private readonly NebulaTextbook.Data.NebulaTextbookContext _context = context;
        public String filePath { get; set; } = "";
        public String thumbnailPath { get; set; } = "";

        public string GetVideo(string name) // Currently searches by name. 
        {
            foreach (Video item in _context.Video)
            {
                if (item.Title == name)
                {
                    filePath = item.FilePath;
                    if (item.ThumbnailPath != null)
                    {
                        thumbnailPath = item.ThumbnailPath;
                    }
                }
            }
            return "Wow";
        }
        //public void OnGet()
        //{
        //    foreach (Video item in _context.Video) {
        //        if(item.Title == "Bunny")
        //        {
        //            filePath = item.FilePath;
        //            thumbnailPath = item.ThumbnailPath;
        //        }
        //    }
        //}
    }
}
