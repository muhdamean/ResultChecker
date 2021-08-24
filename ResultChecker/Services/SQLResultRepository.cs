using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Services
{
    public class SQLResultRepository : IResultRepository
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<SQLResultRepository> logger;
        private readonly IMailService mailService;

        public SQLResultRepository(AppDbContext dbContext, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, ILogger<SQLResultRepository> logger, IMailService mailService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
            this.mailService = mailService;
        }
        public async Task<JsonResult> GetLga(string state)
        {
            var lgaList = new List<SelectListItem>();
            lgaList = await dbContext.StateLgas.Where(x => x.State == state).Select(x => new SelectListItem() { Text = x.LGA, Value = x.LGA }).Distinct().OrderBy(x => x.Value).ToListAsync(); //call repository
            return new JsonResult(lgaList);
        }
    }
}
