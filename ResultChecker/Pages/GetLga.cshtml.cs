using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Services;

namespace ResultChecker.Pages
{
    public class GetLgaModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly IResultRepository resultRepository;

        public GetLgaModel(AppDbContext dbContext, IResultRepository resultRepository)
        {
            this.dbContext = dbContext;
            this.resultRepository = resultRepository;
        }
        [BindProperty(SupportsGet = true)]
        public string state { get; set; }
        public async Task<JsonResult> OnGet()
        {
            return await resultRepository.GetLga(state);
        }
    }
}
