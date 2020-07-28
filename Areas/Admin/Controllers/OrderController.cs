using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using WatanART.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        OrderBLL _OrderBLL = new OrderBLL();
        SettingBLL _SettingBLL = new SettingBLL();
        UsersBLL _UsersBLL = new UsersBLL();
        CountryBLL CountryBLLObj = new CountryBLL();
        PaidTypesBLL Obj = new PaidTypesBLL();
        public ActionResult Index()
        {
            ListsVM listObj = _SettingBLL.GetLists_new(LanguageID);
            ViewBag.state = listObj.OrderState;
            ViewBag.type = listObj.PatternType;
            ViewBag.coutry = listObj.CountryCitiesLst;
            ViewBag.users = _UsersBLL.GetUsers(new FilterationVM());
            return View();
        }

        public ActionResult OrderbyCategory(int Cat_ID)
        {
            ListsCatVM listObj = _SettingBLL.GetListsbycatid(Cat_ID,LanguageID);
            ViewBag.state = listObj.OrderState;
            ViewBag.products = listObj.Product;
            ViewBag.coutry = listObj.CountryCitiesLst;
            ViewBag.users = _UsersBLL.GetUsers(new FilterationVM());
            return View();
        }
        public ActionResult Order2()
        {
            ListsCatVM listObj = _SettingBLL.GetListsbycatid2( LanguageID);
            ViewBag.state = listObj.OrderState;
            ViewBag.products = listObj.Product;
            ViewBag.coutry = listObj.CountryCitiesLst;
            ViewBag.users = _UsersBLL.GetUsers(new FilterationVM());
            return View();
        }
        public ActionResult OrderDetails(long ID)
        {
            
            var OBJ = _OrderBLL.GetOrderWithDetails(ID,LanguageID);
            ViewBag.username = _UsersBLL.GetByUserID(OBJ.Order.UserID).FullName;
            ViewBag.userPhone = _UsersBLL.GetByUserID(OBJ.Order.UserID).Phone;
            ViewBag.buytype = Obj.SelectAllPaidTypes().Where(x=>x.ID== OBJ.Order.buytype).FirstOrDefault().Title;
            ViewBag.country = CountryBLLObj.SelectCountryById(OBJ.Order.CountryID,LanguageID).Title;
            ViewBag.City = CountryBLLObj.SelectCountryCities(OBJ.Order.CountryID, LanguageID).CitiesLst.Where(x=>x.ID==OBJ.Order.CityID).FirstOrDefault().Title;
            return View(OBJ);
        }


        public ActionResult GetList(JQueryDataTableParams form, Nullable<System.DateTime> from = null
,Nullable<System.DateTime> to = null, string  userid = null, int ? typeid = null, int? stateid = null, int? country = null, int? city = null)
        {
          
            ObjectParameter totalCount = new ObjectParameter("TotalCountlist", 0);
          
            if (string.IsNullOrEmpty(userid))
            {
                userid = null;
            }
           
            List<OrderVM> lst = _OrderBLL.GetOrderWitFilteration_new(form.iDisplayStart, form.iDisplayLength, totalCount,userid, stateid,typeid, country,city,from,to);
           


            List<string[]> str = new List<string[]>();
            //StringBuilder buttons = new StringBuilder();
           // buttons.Append("<div class='btn-group'><button class='btn btn-xs green dropdown-toggle' type='button' data-toggle='dropdown' aria-expanded='false'>");
            //buttons.Append("Actions<i class='fa fa-angle-down'></i></button><ul class='dropdown-menu' role='menu'>");
            //buttons.Append("<li><a target='_blank' href='/reservation/Bokkingdetails/$ID'><i class='icon-docs'></i> $name</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/EditBokking/$ID' ><i class='fa fa-hand-pointer-o'></i> Edit</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/confrimation/$ID' target='_blank'><i class='fa fa-print'></i> print</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,1);'><i class='fa fa-book'></i> Booked  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,2);'><i class='fa fa-hourglass-start'></i> pending  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,3);'><i class='icon-check'></i> Confirm</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='CancelBooking($ID);'><i class='icon-close'></i> Cancel</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,9);'><i class='fa fa-sign-in'></i> Check In</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,10);'><i class='fa fa-sign-out'></i> Check Out</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/Payments/pay?bookingId=$ID' ><i class='fa fa-hand-pointer-o'></i> Pay</a></li>");
            var ORDERLIST = _SettingBLL.SelectALLState(LanguageID);
            //buttons.Append("</ul></div>");
            if (lst != null)
            {
                foreach (var obj in lst)
                {
                    //string x = null;
                    //if (obj.State>1)
                    //{
                    StringBuilder buttons = new StringBuilder();
                    buttons.Append("<select class='selectpicker' onchange='Updateval(this,"+obj.ID+");'>");
                    foreach (var item in ORDERLIST)
                    {
                        if (item.Value==obj.State)
                        {
                            buttons.Append(" <option selected value=" + item.Value + ">" + item.Title + "</option>");
                        }
                        else
                        {
                            buttons.Append(" <option  value=" + item.Value + ">" + item.Title + "</option>");
                        }
                      
                    }
                    buttons.Append("</select>");
   
                        //x = "<input checked='checked' id='Currentstate' name='Currentstate' onchange='ActiveDective("+ obj.ID +", this)' type='checkbox' value='true'/>";
                    //}
                    //else
                    //{
                    //    //x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //    x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //}
                  
                    str.Add(new string[] {
                        obj.ID.ToString(),//0
                        obj.Username,//1
                       
                        obj.CreatedDate.ToString("dd/MM/yyyy"),//3
                         Math.Round(obj.ItemsCost,2).ToString(),//8
                        obj.ItemCount.ToString(),//7
                          buttons.ToString(),
                        //obj.CityName,//9
                        "<a class='btn btn-success btn' href='/Admin/Order/OrderDetails/"+obj.ID+"'><i class='fas fa - edit'></i>"+WatanART.Localization.Resource.OrderDetails+"</a>"
                      

        //btndetails.Replace("$ID", obj.BookingID.ToString()).Replace("$name", "Details"),

        //Math.Round(obj.paymenttotal,2).ToString(),
        // Math.Round(obj.Repaymenttotal,2).ToString(),
        //  obj.GuestName,//11
        // obj.HotelReferenceNum//2
        ////10
    });
                }
            }

            return Json(new
            {
                sEcho = form.sEcho,
                iTotalRecords = totalCount.Value,
                iTotalDisplayRecords = totalCount.Value,
                aaData = str
            },

            JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCatList(int CatID,JQueryDataTableParams form, Nullable<System.DateTime> from = null
, Nullable<System.DateTime> to = null, string userid = null, int? Productid = null, int? stateid = null, int? country = null, int? city = null)
        {

            ObjectParameter totalCount = new ObjectParameter("TotalCountlist", 0);

            if (string.IsNullOrEmpty(userid))
            {
                userid = null;
            }

            List<OrderVM> lst = _OrderBLL.GetOrderWitFilterationCat_new(CatID, form.iDisplayStart, form.iDisplayLength, totalCount, userid, stateid, Productid, country, city, from, to);



            List<string[]> str = new List<string[]>();
            //StringBuilder buttons = new StringBuilder();
            // buttons.Append("<div class='btn-group'><button class='btn btn-xs green dropdown-toggle' type='button' data-toggle='dropdown' aria-expanded='false'>");
            //buttons.Append("Actions<i class='fa fa-angle-down'></i></button><ul class='dropdown-menu' role='menu'>");
            //buttons.Append("<li><a target='_blank' href='/reservation/Bokkingdetails/$ID'><i class='icon-docs'></i> $name</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/EditBokking/$ID' ><i class='fa fa-hand-pointer-o'></i> Edit</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/confrimation/$ID' target='_blank'><i class='fa fa-print'></i> print</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,1);'><i class='fa fa-book'></i> Booked  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,2);'><i class='fa fa-hourglass-start'></i> pending  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,3);'><i class='icon-check'></i> Confirm</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='CancelBooking($ID);'><i class='icon-close'></i> Cancel</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,9);'><i class='fa fa-sign-in'></i> Check In</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,10);'><i class='fa fa-sign-out'></i> Check Out</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/Payments/pay?bookingId=$ID' ><i class='fa fa-hand-pointer-o'></i> Pay</a></li>");
            var ORDERLIST = _SettingBLL.SelectALLState(LanguageID);
            //buttons.Append("</ul></div>");
            if (lst != null)
            {
                foreach (var obj in lst)
                {
                    //string x = null;
                    //if (obj.State>1)
                    //{
                    StringBuilder buttons = new StringBuilder();
                    buttons.Append("<select class='selectpicker' onchange='Updateval(this," + obj.ID + ");'>");
                    foreach (var item in ORDERLIST)
                    {
                        if (item.Value == obj.State)
                        {
                            buttons.Append(" <option selected value=" + item.Value + ">" + item.Title + "</option>");
                        }
                        else
                        {
                            buttons.Append(" <option  value=" + item.Value + ">" + item.Title + "</option>");
                        }

                    }
                    buttons.Append("</select>");

                    //x = "<input checked='checked' id='Currentstate' name='Currentstate' onchange='ActiveDective("+ obj.ID +", this)' type='checkbox' value='true'/>";
                    //}
                    //else
                    //{
                    //    //x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //    x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //}
                    
                    str.Add(new string[] {
                        obj.ID.ToString(),//0
                        obj.Username,//1
                       
                        obj.CreatedDate.ToString("dd/MM/yyyy"),//3
                         Math.Round(obj.ItemsCost+obj.ChargePrice,2).ToString(),//8
                        obj.TotalCount.ToString(),//7
                          buttons.ToString(),
                        //obj.CityName,//9
                        "<a class='btn btn-success btn' href='/Admin/Order/OrderDetails/"+obj.ID+"'><i class='fas fa - edit'></i>"+WatanART.Localization.Resource.OrderDetails+"</a>"
                      

        //btndetails.Replace("$ID", obj.BookingID.ToString()).Replace("$name", "Details"),

        //Math.Round(obj.paymenttotal,2).ToString(),
        // Math.Round(obj.Repaymenttotal,2).ToString(),
        //  obj.GuestName,//11
        // obj.HotelReferenceNum//2
        ////10
    });
                }
            }

            return Json(new
            {
                sEcho = form.sEcho,
                iTotalRecords = totalCount.Value,
                iTotalDisplayRecords = totalCount.Value,
                aaData = str
            },

            JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCatList2( JQueryDataTableParams form, Nullable<System.DateTime> from = null
, Nullable<System.DateTime> to = null, string userid = null, int? Productid = null, int? stateid = null, int? country = null, int? city = null)
        {

            ObjectParameter totalCount = new ObjectParameter("TotalCountlist", 0);

            if (string.IsNullOrEmpty(userid))
            {
                userid = null;
            }

            List<OrderVM> lst = _OrderBLL.GetOrderWitFilterationCat_new2( form.iDisplayStart, form.iDisplayLength, totalCount, userid, stateid, Productid, country, city, from, to);



            List<string[]> str = new List<string[]>();
            //StringBuilder buttons = new StringBuilder();
            // buttons.Append("<div class='btn-group'><button class='btn btn-xs green dropdown-toggle' type='button' data-toggle='dropdown' aria-expanded='false'>");
            //buttons.Append("Actions<i class='fa fa-angle-down'></i></button><ul class='dropdown-menu' role='menu'>");
            //buttons.Append("<li><a target='_blank' href='/reservation/Bokkingdetails/$ID'><i class='icon-docs'></i> $name</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/EditBokking/$ID' ><i class='fa fa-hand-pointer-o'></i> Edit</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/reservation/confrimation/$ID' target='_blank'><i class='fa fa-print'></i> print</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,1);'><i class='fa fa-book'></i> Booked  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,2);'><i class='fa fa-hourglass-start'></i> pending  </a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,3);'><i class='icon-check'></i> Confirm</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='CancelBooking($ID);'><i class='icon-close'></i> Cancel</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,9);'><i class='fa fa-sign-in'></i> Check In</a></li>");
            //buttons.Append("<li><a target='_blank'  href='javascript:;' onclick='ChangeBookingStatus($ID,10);'><i class='fa fa-sign-out'></i> Check Out</a></li>");
            //buttons.Append("<li><a target='_blank'  href='/Payments/pay?bookingId=$ID' ><i class='fa fa-hand-pointer-o'></i> Pay</a></li>");
            var ORDERLIST = _SettingBLL.SelectALLState(LanguageID);
            //buttons.Append("</ul></div>");
            if (lst != null)
            {
                foreach (var obj in lst)
                {
                    //string x = null;
                    //if (obj.State>1)
                    //{
                    StringBuilder buttons = new StringBuilder();
                    buttons.Append("<select class='selectpicker' onchange='Updateval(this," + obj.ID + ");'>");
                    foreach (var item in ORDERLIST)
                    {
                        if (item.Value == obj.State)
                        {
                            buttons.Append(" <option selected value=" + item.Value + ">" + item.Title + "</option>");
                        }
                        else
                        {
                            buttons.Append(" <option  value=" + item.Value + ">" + item.Title + "</option>");
                        }

                    }
                    buttons.Append("</select>");

                    //x = "<input checked='checked' id='Currentstate' name='Currentstate' onchange='ActiveDective("+ obj.ID +", this)' type='checkbox' value='true'/>";
                    //}
                    //else
                    //{
                    //    //x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //    x = "<input  id='Currentstate' name='Currentstate' onchange='ActiveDective(" + obj.ID + ", this)' type='checkbox' value='true'/>";
                    //}

                    str.Add(new string[] {
                        obj.ID.ToString(),//0
                        obj.Username,//1
                       
                        obj.CreatedDate.ToString("dd/MM/yyyy"),//3
                         Math.Round(obj.ItemsCost+obj.ChargePrice,2).ToString(),//8
                        obj.TotalCount.ToString(),//7
                          buttons.ToString(),
                        //obj.CityName,//9
                        "<a class='btn btn-success btn' href='/Admin/Order/OrderDetails/"+obj.ID+"'><i class='fas fa - edit'></i>"+WatanART.Localization.Resource.OrderDetails+"</a>"
                      

        //btndetails.Replace("$ID", obj.BookingID.ToString()).Replace("$name", "Details"),

        //Math.Round(obj.paymenttotal,2).ToString(),
        // Math.Round(obj.Repaymenttotal,2).ToString(),
        //  obj.GuestName,//11
        // obj.HotelReferenceNum//2
        ////10
    });
                }
            }

            return Json(new
            {
                sEcho = form.sEcho,
                iTotalRecords = totalCount.Value,
                iTotalDisplayRecords = totalCount.Value,
                aaData = str
            },

            JsonRequestBehavior.AllowGet);
        }

        public JsonResult cityList(int ID)
        {


          var newList2=  CountryBLLObj.SelectCountryCities(ID, LanguageID);
            newList2.CitiesLst.Add(new CityVM { ID = 0, NameAR = "--", NameEN = "--" });

            //var ItemSizesLst = new SelectList(ItemSizesLsts.Items, "SubID", "TitleEN");


            //var ItemSizesLst = new SelectList(newList2.CitiesLst, "SubID", "TitleAR");
            //result = ItemSizesLst;




            return Json(newList2.CitiesLst, JsonRequestBehavior.AllowGet);
        }



        public JsonResult UpdateState(long orderid,int state)
        {
            try
            {
                OrderVM _OrderVM = new OrderVM();
                _OrderVM.ID= orderid;
                _OrderVM.State = state;
                int retValue = _OrderBLL.UpdateOrder(_OrderVM);
                if (retValue >0)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}