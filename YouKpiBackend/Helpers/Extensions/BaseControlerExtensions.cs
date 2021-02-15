using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public static IEnumerable<string> GetRoles(this ControllerBase controller)
        {
            return ((ClaimsIdentity)controller.User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
        }
        public static bool isAdmin(this ControllerBase controller)
        {
            return GetRoles(controller).Any(p => p == "Administrator");
        }

    }
}
