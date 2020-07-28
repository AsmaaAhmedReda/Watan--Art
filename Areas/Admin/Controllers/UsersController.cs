using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using WatanART.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        OrderBLL _OrderBLL = new OrderBLL();
        SettingBLL _SettingBLL = new SettingBLL();
        UsersBLL _UsersBLL = new UsersBLL();
        CountryBLL CountryBLLObj = new CountryBLL();
        CityBLL _CityBLL = new CityBLL();
        public ActionResult Index()
        {
            ListsVM listObj = _SettingBLL.GetLists_new(LanguageID);
            ViewBag.coutry = listObj.CountryCitiesLst;
            ViewBag.users = _UsersBLL.GetUsers(new FilterationVM());
            return View();
        }


        public ActionResult GetList(JQueryDataTableParams form 
, string Name = null, int? country = null, int? city = null)
        {

            ObjectParameter totalCount = new ObjectParameter("TotalCount", 0);

            if (string.IsNullOrEmpty(Name))
            {
                Name = null;
            }

            List<UserObjVM> lst = _UsersBLL.GetUsersNew(form.iDisplayStart, form.iDisplayLength, totalCount, Name,country, city);


            var COUNT = form.iDisplayStart;
            List<string[]> str = new List<string[]>();
            //StringBuilder buttons = new StringBuilder();
            //buttons.Append("<div class='btn-group'><button class='btn btn-xs green dropdown-toggle' type='button' data-toggle='dropdown' aria-expanded='false'>");
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

            //buttons.Append("</ul></div>");
            if (lst != null)
            {
                foreach (var obj in lst)
                {
                    string x = null;
                    if (!(obj.IsLockedOut.Value))
                    {
                        x = "<div><input checked='checked' id='Currentstate'  name='Currentstate' onchange='ActiveDective(this)' type='checkbox' value='false'/><input type='hidden' value="+obj.UserID+" class='username'></div> ";
                    }
                    else
                    {
                        x = "<div><input  id='Currentstate' name='Currentstate' onchange='ActiveDective(this)' type='checkbox' value='true'/><input type='hidden' value=" + obj.UserID + " class='username'></div>";
                    }

                    str.Add(new string[] {
                        (++COUNT).ToString(),
                        obj.FullName,//0
                        obj.Email,//1
                       
                        obj.Phone,//3
                        obj.CountryID.Value!=0?CountryBLLObj.SelectCountryById(obj.CountryID.Value,LanguageID).Title:"",
                        obj.cityID.Value!=0? _CityBLL.SelectCitYbYid(obj.cityID.Value,LanguageID).Title:"",//,//8
                        //7
                        x,
                          //x,
                          "<div><a class='btn btn-primary btn-xs' href='/Admin/Users/GetUser/"+obj.UserID+"'><i class='fa fa-edit'></i>"+WatanART.Localization.Resource.Edit+" </a><input type='hidden' value=" + obj.UserID + " class='username'></div>"
        //obj.CityName,//9
        //"<a class='btn btn-success btn' onclick='ActiveDective(" + obj.ID + ")'><i class='fas fa - edit'></i></a>",


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


            var newList2 = CountryBLLObj.SelectCountryCities(ID, LanguageID);
            newList2.CitiesLst.Add(new CityVM { ID = 0, NameAR = "--", NameEN = "--" });
            //var ItemSizesLst = new SelectList(ItemSizesLsts.Items, "SubID", "TitleEN");


            //var ItemSizesLst = new SelectList(newList2.CitiesLst, "SubID", "TitleAR");
            //result = ItemSizesLst;




            return Json(newList2.CitiesLst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser(string ID)
        {
           
            //ViewBag.coutry = listObj.CountryCitiesLst;
            //ViewBag.coutry = listObj.C;
          
            var Obj = _UsersBLL.GetByUserID(ID);
            ViewBag.country = CountryBLLObj.SelectAllCountries(LanguageID).ToList();
            ViewBag.City = CountryBLLObj.SelectCountryCities(Obj.CountryID.Value, LanguageID).CitiesLst.ToList() ;




            return View(Obj);
        }


       

    public JsonResult UpdateUser(UserObjVM Obj)
    {
        try
        {

         var dbuser = _UsersBLL.updateUser(Obj);
              
            if (dbuser > 0)
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

    public JsonResult UpdateState(string UserId)
        {
            try
            {
                
                int retValue = _UsersBLL.ChangeStatus(UserId);
                if (retValue > 0)
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