using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xbiz_Task.Models;

namespace Xbiz_Task.Controllers
{
	public class AdminController : Controller
	{
		assignmentEntities db = new assignmentEntities();
		public ActionResult Index()
		{
			ViewBag.detail = db.Emp_Details.ToList();
			return View();
		}
		[HttpPost]
		public ActionResult Index(Emp_Details st, HttpPostedFileBase file)
		{
			if (file.ContentLength > 0)
			{
				string imgname = st.Employee_Name + Path.GetExtension(file.FileName);
				string imgpath = Server.MapPath("~/FileResume/" + imgname);
				file.SaveAs(imgpath);
				st.Employee_Resume = imgname;
			}

			db.Emp_Details.Add(st);
			db.SaveChanges();
			ViewBag.detail = db.Emp_Details.ToList();
			return RedirectToAction("Employee");
		}
		
	
		public ActionResult Employee()
		{
			ViewBag.employee = GetEmployee();
			ViewBag.EmpData = db.Tbl_Emp.ToList();
			return View();
		}
		[HttpPost]
		public ActionResult Employee(Tbl_Emp st)
		{
			ViewBag.employee = GetEmployee();
			db.Tbl_Emp.Add(st);
			db.SaveChanges();
			ViewBag.EmpData = db.Tbl_Emp.ToList();
			return View();
		}
		public List<SelectListItem> GetEmployee()
		{
			List<SelectListItem> lst = new List<SelectListItem>();
			foreach (Emp_Details t in db.Emp_Details.ToList())
			{
				SelectListItem s = new SelectListItem() { Text = t.Employee_Name, Value = t.Employee_id.ToString() };
				lst.Add(s);
			}
			return lst;
		}
	}

}