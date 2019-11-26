using System;
using System.Web.Services;

namespace DotrA_001_Clone.Models
{
    public static class HandleCart
    {
        //需引用system.web.services
        //使用session
        [WebMethod(EnableSession = true)]
        public static Cart GetCurrentCart()
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    System.Web.HttpContext.Current.Session["Cart"] = order;
                }

                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }

            else
            {
                throw new InvalidOperationException("購物車是空的,請檢查");
            }
        }
    }
}
