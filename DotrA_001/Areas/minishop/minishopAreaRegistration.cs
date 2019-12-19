using System.Web.Mvc;

namespace DotrA.Areas.minishop
{
    public class minishopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "minishop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "minishop_default",
                url: "minishop/{action}",
               defaults: new { controller = "minishop", action = "Index" },
                new string[] { "DotrA.Areas.minishop.Controllers" }
            );
        }
    }
}