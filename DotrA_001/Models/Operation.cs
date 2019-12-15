using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;



namespace DotrA_001.Models
{
    //購物車實際操作
    public static class Operation
    {
        [WebMethod(EnableSession = true)]//啟用Session
        public static Models.CartTestTest GetCurrentCart()//取得目前Session中的Cart物件
        {
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Session["CartTestTest"] == null)
                {
                    var order = new CartTestTest();
                    System.Web.HttpContext.Current.Session["CartTestTest"] = order;
                }
                return (CartTestTest)System.Web.HttpContext.Current.Session["CartTestTest"];
            }
            else
            {
                throw new InvalidOperationException("System.Web.HttpContext.Current為空");
            }
        }
    }
}