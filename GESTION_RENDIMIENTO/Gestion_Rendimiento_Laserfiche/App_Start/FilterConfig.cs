using System.Web;
using System.Web.Mvc;

namespace Gestion_Rendimiento_Laserfiche
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
