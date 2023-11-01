using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Album.Data;
using Album.Model;

namespace EntityF.Pages_Blog
{
    public class IndexModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int currentPage {set;get;}
        public int Page_Item = 10;

        public int countPages {set;get;}
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString {set;get;} ="";
        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (currentPage < 1 ) {
                currentPage = 1;
            }
            
            if (_context.Article != null)
            {
                
                var articles = from a in _context.Article select a;
                int totalItem = articles.Count();
                countPages = (int)Math.Ceiling((double)totalItem / Page_Item);
                if (currentPage > countPages) {
                    currentPage = countPages;
                }

                Article = await articles.Skip((currentPage - 1 )*Page_Item ).Take(Page_Item).ToListAsync();
                if(!string.IsNullOrEmpty(SearchString) ) {
                    Console.WriteLine(SearchString);
                    articles = articles.Where(a => a.Content.Contains(SearchString));
                    Article = await articles.ToListAsync(); 
                }
            }


        }
    }
}
