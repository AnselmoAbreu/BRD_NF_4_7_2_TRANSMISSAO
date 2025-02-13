using System.Web;
using System.Web.Mvc;

namespace BRD_NF_4_7_2_TRANSMISSAO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
