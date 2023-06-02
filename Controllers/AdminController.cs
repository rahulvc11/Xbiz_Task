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
			EmployeeModel em = new EmployeeModel() { hobbies=GetHobbies()};

			return View(em);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(EmployeeModel st, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.detail = db.Emp_Details.ToList();
				EmployeeModel em = new EmployeeModel() { hobbies = GetHobbies() };

				return View(em);
			}
			else
			{

				if (file.ContentLength > 0)
				{
					string imgname = st.Employee_Name + Path.GetExtension(file.FileName);
					string imgpath = Server.MapPath("~/FileResume/" + imgname);
					file.SaveAs(imgpath);
					st.Employee_Resume = imgname;
				}
				string data = "";
				foreach(Hobby h in st.hobbies)
				{
					if (h.isSelected)
					{
						data += "," + h.hobby_name;
					}
				}
				data = data.Substring(1, data.Length - 1);
				Emp_Details ed = new Emp_Details() { Employee_Name=st.Employee_Name, Employee_City=st.Employee_City , Employee_Contact=st.Employee_Contact , Employee_Resume=st.Employee_Resume , Employee_State=st.Employee_State, Employee_Hobbies=data  };
				db.Emp_Details.Add(ed);
				db.SaveChanges();
				ViewBag.detail = db.Emp_Details.ToList();
				return RedirectToAction("Employee");


			}
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

		public List<Hobby> GetHobbies()
		{
			List<Hobby> hobbies = new List<Hobby>();
			hobbies.Add(new Hobby() { hobby_name = "Playing Cricket", isSelected = false });
			hobbies.Add(new Hobby() { hobby_name = "Watching Movies", isSelected = false });
			hobbies.Add(new Hobby() { hobby_name = "Walking", isSelected = false });
			hobbies.Add(new Hobby() { hobby_name = "Swimming", isSelected = false });
			hobbies.Add(new Hobby() { hobby_name = "Reading Books", isSelected = false });
			return hobbies;
		}
	}

}