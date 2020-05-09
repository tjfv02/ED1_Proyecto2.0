using System.Web;
using System.Web.Mvc;

namespace ED1_ProyectoCOVID
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
