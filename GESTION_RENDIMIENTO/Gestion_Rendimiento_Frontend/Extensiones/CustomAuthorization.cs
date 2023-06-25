using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Seguridad
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {

        // https://www.c-sharpcorner.com/article/how-to-override-customauthorization-class-in-net-core/

        /// <summary>  
        /// This will Authorize User  
        /// </summary>  
        /// <returns></returns>  
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext != null)
            {

                var user = filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;

                if (string.IsNullOrEmpty(user))
                {
                    filterContext.Result = new RedirectToRouteResult(
   new RouteValueDictionary(new { controller = "Login", action = "index" })
);

                }


            }
        }

        public bool IsValidToken(string authToken)
        {
            //validate Token here  
            return true;
        }
    }
}
