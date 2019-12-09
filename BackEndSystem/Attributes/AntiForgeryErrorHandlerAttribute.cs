
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackEndSystem.Models
{
    public class AntiForgeryErrorHandlerAttribute : HandleErrorAttribute
    {
        //用來指定 redirect 的目標 controller
        public string Controller { get; set; }

        //覆寫預設發生 exception 時的動作
        public override void OnException(ExceptionContext filterContext)
        {
            //如果發生的 exception 是 HttpAntiForgeryException 就轉導至設定的 controller、action (action 在 base HandleErrorAttribute已宣告)
            if (filterContext.Exception is HttpAntiForgeryException)
            {
                //這個屬性要設定為 true 才能接手處理 exception 也才可以 redirect
                filterContext.ExceptionHandled = true;
                //指定 redirect 的 controller 偶 action
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    { "Index", View },
                    { "Home", Controller},
                    });
            }
            else
                base.OnException(filterContext);// exception 不是 HttpAntiForgeryException 就照 mvc 預設流程
        }
    }
}