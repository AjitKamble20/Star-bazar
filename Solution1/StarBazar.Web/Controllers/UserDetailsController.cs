using StarBazar.Entities;
using StarBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarBazar.Web.Controllers
{
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        public ActionResult Index(string search)
        {
            BindState();
            var locations = UserDetailsService.Instance.GetLocations();
            //if (string.IsNullOrEmpty(search) == false)
            //{
            //    locations = locations.Where(p => p.Pincode.ToString() != null).ToList();
            //}
            return View(locations);
            
        }

        public void BindState()
        {
            var state = UserDetailsService.Instance.GetStates();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select State--", Value = "0" });

            foreach (var m in state)
            {
                li.Add(new SelectListItem { Text = m.StateName, Value = m.StateId.ToString() });
                ViewBag.state = li;
            }
        }

        public JsonResult GetDistrict(int id)
        {
            var ddlDistrict =UserDetailsService.Instance.GetDistrictByState(id);
            List<SelectListItem> licities = new List<SelectListItem>();

            licities.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
            if (ddlDistrict != null)
            {
                foreach (var x in ddlDistrict)
                {
                    licities.Add(new SelectListItem { Text = x.DistrictName, Value = x.DistrictId.ToString() });
                }
            }
            return Json(new SelectList(licities, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        
        public JsonResult GetPincode(int id)
        {
            var ddlPincode = UserDetailsService.Instance.GetPincodeByDistrict(id);
            List<SelectListItem> licities = new List<SelectListItem>();

            licities.Add(new SelectListItem { Text = "--Select District--", Value = "0" });
            if (ddlPincode != null)
            {
                foreach (var x in ddlPincode)
                {
                    licities.Add(new SelectListItem { Text = x.Pincode.ToString(), Value = x.PincodetId.ToString() });
                }
            }
            return Json(new SelectList(licities, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
       
    }
}