using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResultChecker.Models;
using ResultChecker.Services;

namespace ResultChecker.Pages
{
    public class GetLgaModel : PageModel
    {
        private readonly AppDbContext dbContext;
        private readonly IResultRepository resultRepository;
        private readonly SignInManager<ApplicationUser> signInManager;

        public GetLgaModel(AppDbContext dbContext, IResultRepository resultRepository, SignInManager<ApplicationUser> signInManager)
        {
            this.dbContext = dbContext;
            this.resultRepository = resultRepository;
            this.signInManager = signInManager;
        }
        [BindProperty(SupportsGet = true)]
        public string state { get; set; }
        public async Task<JsonResult> OnGet()
        {
            return await resultRepository.GetLga(state);
        }
        public async Task<IActionResult> OnPost()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("index");
        }
    }
}
