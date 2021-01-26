using System.Web;
using System.Web.Mvc;

namespace HHI_InspectionSoftware
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
