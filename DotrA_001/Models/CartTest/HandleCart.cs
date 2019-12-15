using System;
using System.Web.Services;

namespace DotrA_001.Models
{
    public static class HandleCart
    {
        //需引用system.web.services
        //使用session
        [WebMethod(EnableSession = true)]
        public static Carttest GetCurrentCart()
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Carttest();
                    System.Web.HttpContext.Current.Session["Cart"] = order;
                }

                return (Carttest)System.Web.HttpContext.Current.Session["Cart"];
            }

            else
            {
                throw new InvalidOperationException("購物車是空的,請檢查");
            }
        }
    }
}
