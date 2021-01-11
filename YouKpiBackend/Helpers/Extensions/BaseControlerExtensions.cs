using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.Helpers.Extensions
{
    public static class BaseControlerExtensions
    {

            public static int GetUserId(this ControllerBase controller)
            {
                var idStr = controller.User.Claims.Where(x => x.Type == "sid").FirstOrDefault()?.Value;
                int.TryParse(idStr, out var userId);
                return userId;
            }

    }
}
