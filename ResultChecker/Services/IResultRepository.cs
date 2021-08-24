using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultChecker.Services
{
    public interface IResultRepository
    {
        public Task<JsonResult> GetLga(string state);
    }
}
