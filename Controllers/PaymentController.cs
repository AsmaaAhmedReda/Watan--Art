using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Models;
using WatanART.PaymentHelper;

namespace WatanART.Controllers
{
    public class PaymentController : BaseController
    {
        private VPCRequest conn;

        OrderBLL _OrderBLL = new OrderBLL();
        SettingBLL _SettingBLL = new SettingBLL();
        UsersBLL _UsersBLL = new UsersBLL();
        CountryBLL CountryBLLObj = new CountryBLL();
        PaidTypesBLL Obj = new PaidTypesBLL();
        // GET: Payment
        //public ActionResult PaymentOrder()
        //{
        //    return View();
        //}
        //
        // POST: /Payment/Pay
        [HttpGet]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult PaymentOrder(PaymentModel model)
        public ActionResult PaymentOrder(string OrderId,int lang)
        {
            PaymentModel model = new PaymentModel();
            OrderBLL _OrderBLL = new OrderBLL();
            TempData["ID"] = OrderId;
            TempData["lang"] = lang;
           

            OrderWithDetailsVM obj = _OrderBLL.GetOrderWithDetails(long.Parse(OrderId), lang);
            model.OrderInfo = OrderId;
            if (obj.Order!=null && obj.Order.ItemsCost>0)
            {
                model.PurchaseAmount = (obj.Order.ItemsCost * 100).ToString();
            }
            else
            {
                model.PurchaseAmount = (0).ToString();
            }

            var currency =(obj.Order!=null && obj.Order.InEgypt!=null)? obj.Order.InEgypt:false;
            TempData["currencykey"] = currency;
            if (ModelState.IsValid)
            {
                try
                {

                



          var payment = new PaymentModel { OrderInfo = model.OrderInfo, PurchaseAmount = model.PurchaseAmount };
                    // Connect to the Payment Client
                    VPCRequest conn = new VPCRequest();
                    // Add the Digital Order Fields for the functionality you wish to use
                    // Core Transaction Fields
                    var MerchantID = conn.MerchantID;
                    var crruncy = "EGP";
                    var AccessCode = conn.AccessCode;
                    //var AccessCode = conn.AccessCode;
                    //
                    if (!currency)//TRUE =EGYPT
                    {
                        MerchantID = "CIB700572USD";
                        crruncy = "USD";
                        AccessCode = "902AD98F";
                      
                       
                        conn.SetSecureSecret("9F48D9FEA4F65741D3C2F7E24207C30A");
                    }

                    conn.AddDigitalOrderField("vpc_Version", conn.Version);
                    conn.AddDigitalOrderField("vpc_Command", conn.Command);
                    conn.AddDigitalOrderField("vpc_AccessCode", AccessCode);
                    conn.AddDigitalOrderField("vpc_Merchant", MerchantID);
                    conn.AddDigitalOrderField("vpc_ReturnURL", conn.FormatReturnURL(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Request.ApplicationPath));
                    conn.AddDigitalOrderField("vpc_MerchTxnRef", payment.OrderInfo + "-1");
                    conn.AddDigitalOrderField("vpc_OrderInfo", payment.OrderInfo);
                    conn.AddDigitalOrderField("vpc_Amount", payment.PurchaseAmount);
                    conn.AddDigitalOrderField("vpc_Currency", crruncy);
                    conn.AddDigitalOrderField("vpc_Locale", "en_EG");
                    // Perform the transaction
                    String url = conn.Create3PartyQueryString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }
                    else
                    {
                        return View(model);
                    }
                    

                }
                catch (Exception ex)
                {
                    // Capture and Display the error information
                    //lblErrorMessage.Text = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
                    //pnlError.Visible = true;
                   
                }
               
            }
            else
            {
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: Payment receipt
        
        public ActionResult PaymentReceipt()
        {
            
            try
            {
                
               bool CURRENCY = Convert.ToBoolean(TempData["currencykey"]); //TRUE =EGYPT
                // Create a new VPCRequest object
                VPCRequest conn = new VPCRequest();

                if (!CURRENCY)
                {
                    conn.SetSecureSecret("9F48D9FEA4F65741D3C2F7E24207C30A");
                }
                    // Process the response received
                    conn.Process3PartyResponse(Request.QueryString);

                    // Check if the transaction was successful or if there was an error
                    String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");

                    // Set the display fields for the receipt with the result fields
                    // Core Fields
                    ViewBag.vpcTxnResponseCode = vpc_TxnResponseCode;
                    ViewBag.vpc_MerchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
                    ViewBag.vpc_OrderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
                    ViewBag.vpc_Merchant = conn.GetResultField("vpc_Merchant", "Unknown");
                    ViewBag.vpc_Amount = conn.GetResultField("vpc_Amount", "Unknown");
                    ViewBag.vpc_Message = conn.GetResultField("vpc_Message", "Unknown");
                    ViewBag.vpc_ReceiptNo = conn.GetResultField("vpc_ReceiptNo", "Unknown");
                    ViewBag.vpc_AcqResponseCode = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
                    ViewBag.vpc_AuthorizeId = conn.GetResultField("vpc_AuthorizeId", "Unknown");
                    ViewBag.vpc_BatchNo = conn.GetResultField("vpc_BatchNo", "Unknown");
                    ViewBag.vpc_TransactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
                    ViewBag.vpc_Card = conn.GetResultField("vpc_Card", "Unknown");
                    ViewBag.vpc_3DSECI = conn.GetResultField("vpc_3DSECI", "Unknown");
                    ViewBag.vpc_3DSXID = conn.GetResultField("vpc_3DSXID", "Unknown");
                    ViewBag.vpc_3DSenrolled = conn.GetResultField("vpc_3DSenrolled", "Unknown");
                    ViewBag.vpc_3DSstatus = conn.GetResultField("vpc_3DSstatus", "Unknown");
                    ViewBag.vpc_VerToken = conn.GetResultField("vpc_VerToken", "Unknown");
                    ViewBag.vpc_VerType = conn.GetResultField("vpc_VerType", "Unknown");
                    ViewBag.vpc_VerSecurityLevel = conn.GetResultField("vpc_VerSecurityLevel", "Unknown");
                    ViewBag.vpc_VerStatus = conn.GetResultField("vpc_VerStatus", "Unknown");
                    ViewBag.vpc_RiskOverallResult = conn.GetResultField("vpc_RiskOverallResult", "Unknown");
                    ViewBag.vpc_TxnReversalResult = conn.GetResultField("vpc_TxnReversalResult", "No Value Returned");
                    ViewBag.TxnResponseCodeDesc = PaymentCodesHelper.GetTxnResponseCodeDescription(vpc_TxnResponseCode);

                    // Card Security Code Fields
                    ViewBag.vpc_cscResultCode = conn.GetResultField("vpc_cscResultCode", "Unknown");
                    ViewBag.cscResultCodeDesc = PaymentCodesHelper.GetCSCDescription(ViewBag.vpc_cscResultCode);

                    int ID = Convert.ToInt32(TempData["ID"]);
                    int langID = Convert.ToInt32(TempData["lang"]);
                    ViewBag.lang = langID;
                    var OBJ = _OrderBLL.GetOrderWithDetails(ID, langID);
                    ViewBag.username = _UsersBLL.GetByUserID(OBJ.Order.UserID).FullName;
                    ViewBag.userPhone = _UsersBLL.GetByUserID(OBJ.Order.UserID).Phone;
                    ViewBag.buytype = Obj.SelectAllPaidTypes().Where(x => x.ID == OBJ.Order.buytype).FirstOrDefault().Title;
                    ViewBag.country = CountryBLLObj.SelectCountryById(OBJ.Order.CountryID, LanguageID).Title;
                    ViewBag.City = CountryBLLObj.SelectCountryCities(OBJ.Order.CountryID, LanguageID).CitiesLst.Where(x => x.ID == OBJ.Order.CityID).FirstOrDefault().Title;
                // Display the response fields
                //pnlResponse.Visible = true;
                if (ViewBag.vpcTxnResponseCode == "0" && ViewBag.vpc_Message == "Approved" && OBJ!=null && OBJ.Order != null && OBJ.Order.ID > 0)
                {
                    ViewBag.vpcTxnResponseCode = "0";
                    ViewBag.vpc_Message = "Approved";
                    _OrderBLL.updateConfiramed(ID);
                   
                    return View(OBJ);
                }
                else
                {
                   
                    _OrderBLL.DeleteOrder(ID);
                    return RedirectToAction("shipping_order","Home");
                }
            }
            catch (Exception ex)
            {
                int langID = Convert.ToInt32(TempData["lang"]);
                if (langID==0)
                {
                    langID = 2;

                }
                ViewBag.lang = langID;
                // Capture the error and display the error information
                //lblReceiptErrorMessage.Text = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
                //pnlReceiptError.Visible = true;
                int ID = Convert.ToInt32(TempData["ID"]);
                _OrderBLL.DeleteOrder(ID);
                return RedirectToAction("shipping_order", "Home");
            }
           
        }

    }
    }

