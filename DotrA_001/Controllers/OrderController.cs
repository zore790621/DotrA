﻿using Database.Models;
using DotrA_001.Models;
using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace DotrA_001.Controllers
{
    public class OrderController : Controller
    {

        private DotrADb db = new DotrADb();
        // GET: Order
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if(Operation.GetCurrentCart().Count == 0)
                {
                    return RedirectToAction("Index", "Shop");
                }
                bool toint = int.TryParse(((System.Security.Claims.ClaimsIdentity)User.Identity).RoleClaimType, out int UID);
                if (toint == false)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var source = db.Members.FirstOrDefault(x => x.MemberID == UID);

                var result = new MemderOrderViewModel()
                {
                    RecipientName = source.Name,
                    RecipientAddress = source.Address,
                    RecipientPhone = source.Phone
                };

                ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "ShipperName");
                ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentMethod");

                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Members");
            }
            
        }

        [WebMethod(EnableSession = true)]
        [HttpPost]
        public ActionResult Index(MemderOrderViewModel Alllist)
        {
            bool toint = int.TryParse(((System.Security.Claims.ClaimsIdentity)User.Identity).RoleClaimType, out int UID);
            if (toint == false)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var source = db.Members.FirstOrDefault(x => x.MemberID == UID);
            
            var odtest = new Order();

            if (this.ModelState.IsValid)
            {   //取得目前購物車
                using (var transaction = db.Database.BeginTransaction())
                {
                    var currentcart = Models.Operation.GetCurrentCart();

                    //取得目前登入使用者Id
                    var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).RoleClaimType;
                    try
                    {
                        //建立Order物件
                        var order = new Order()
                        {
                            MemberID = source.MemberID,
                            RecipientName = Alllist.RecipientName,
                            RecipientAddress = Alllist.RecipientAddress,
                            RecipientPhone = Alllist.RecipientPhone,
                            ShipperID = Alllist.ShipperID,
                            PaymentID = Alllist.PaymentID,
                            OrderDate = DateTime.Now
                        };
                        //加其入Orders資料表後，儲存變更
                        db.Orders.Add(order);
                        db.SaveChanges();

                        odtest = (from o in db.Orders
                                  where o.OrderID == order.OrderID
                                  select o).ToList().FirstOrDefault();

                        //取得購物車中OrderDetai物件
                        var orderDetails = currentcart.ToOrderDetailList(odtest.OrderID);

                        //將其加入OrderDetails資料表後，儲存變更
                        db.OrderDetails.AddRange(orderDetails);
                        db.SaveChanges();
                        currentcart.ClearCart();
                        transaction.Commit();
                        return RedirectToAction("Payment", new { id = odtest.OrderID });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return Content("訂購失敗");
                    }
                }
            }
            return View();
        }

        public ActionResult GetResult(ECPayModel request)
        {
            if (request.RtnCode == 1)
            {
                TempData["SuccessPayment"] = "您已付款成功，我們會即刻開始準備出貨";
                return RedirectToAction("Index", "Home");
            }
           
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Payment(int? id)
        {
            var o = db.Orders.Find(id);

            string test = "test";
            List<string> enErrors = new List<string>();
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                    oPayment.MerchantID = "2000132";//ECPay提供的特店編號

                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "https://dotrabackend.azurewebsites.net/Orders/GetPaymentResult";//付款完成通知回傳的網址
                    oPayment.Send.ClientBackURL = "https://dotrawebsite.azurewebsites.net/";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = "https://dotrawebsite.azurewebsites.net/Order/GetResult";//瀏覽器端回傳付款結果網址
                    oPayment.Send.MerchantTradeNo = "Dotra"+new Random().Next(0, 99999).ToString();//廠商的交易編號
                    oPayment.Send.MerchantTradeDate = DateTime.Now;//廠商的交易時間
                    oPayment.Send.TotalAmount = Convert.ToInt32(o.OrderDetails.Sum(y => y.SubTotal));//交易總金額
                    oPayment.Send.TradeDesc = "交易描述";//交易描述
                    oPayment.Send.ChoosePayment = (PaymentMethod)o.PaymentID;//使用的付款方式
                    oPayment.Send.Remark = "";//備註欄位
                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.Yes;//是否需要額外的付款資訊
                    oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                    oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
                    oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.CustomField1 = id.ToString();
                    oPayment.Send.CustomField2 = "";
                    oPayment.Send.CustomField3 = "";
                    oPayment.Send.CustomField4 = "";
                    oPayment.Send.EncryptType = 1;

                 
                    foreach (var item in o.OrderDetails)
                    {
                        oPayment.Send.Items.Add(new Item()
                        {
                            Name = item.Product.ProductName,//商品名稱
                            Price = item.Product.SalesPrice,//商品單價
                            Currency = "新台幣",//幣別單位
                            Quantity = item.Quantity,//購買數量
                            URL = "https://dotrawebsite.azurewebsites.net/Shop",//商品的說明網址
                        });

                    }
                    //訂單的商品資料
                    

                    /*************************非即時性付款:ATM、CVS 額外功能參數**************/

                    #region ATM 額外功能參數

                    //oPayment.SendExtend.ExpireDate = 3;//允許繳費的有效天數
                    //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                    //oPayment.SendExtend.ClientRedirectURL = "";//Client 端回傳付款相關資訊

                    #endregion


                    #region CVS 額外功能參數

                    //oPayment.SendExtend.StoreExpireDate = 3; //超商繳費截止時間 CVS:以分鐘為單位 BARCODE:以天為單位
                    //oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
                    //oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
                    //oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
                    //oPayment.SendExtend.Desc_4 = "";//交易描述 4
                    //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
                    //oPayment.SendExtend.ClientRedirectURL = "";///Client 端回傳付款相關資訊

                    #endregion

                    /***************************信用卡額外功能參數***************************/

                    #region Credit 功能參數

                    //oPayment.SendExtend.BindingCard = BindingCardType.No; //記憶卡號
                    //oPayment.SendExtend.MerchantMemberID = ""; //記憶卡號識別碼
                    //oPayment.SendExtend.Language = ""; //語系設定

                    #endregion Credit 功能參數

                    #region 一次付清

                    //oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
                    //oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

                    #endregion

                    #region 分期付款

                    //oPayment.SendExtend.CreditInstallment = "3,6";//刷卡分期期數

                    #endregion 分期付款

                    #region 定期定額

                    //oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
                    //oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
                    //oPayment.SendExtend.Frequency = 1;//執行頻率
                    //oPayment.SendExtend.ExecTimes = 2;//執行次數
                    //oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

                    #endregion
                  
                    /* 產生訂單 */
                    enErrors.AddRange(oPayment.CheckOutString(ref test));
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                // 顯示錯誤訊息。
                if (enErrors.Count() > 0)
                {
                    // string szErrorMessage = String.Join("\\r\\n", enErrors);
                }
            }
            return Content(test);
        }
    }
}