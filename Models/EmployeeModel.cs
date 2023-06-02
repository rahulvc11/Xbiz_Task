using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Xbiz_Task.Models
{
	public class EmployeeModel
	{
		public int Employee_id { get; set; }

		[Required(ErrorMessage = "PLEASE ENTER NAME OF EMPLOYEE")]
		public string Employee_Name { get; set; }
		public string Employee_State { get; set; }
		public string Employee_City { get; set; }

		public string Employee_Contact { get; set; }
		//public string Employee_Hobbies { get; set; }
		public string Employee_Resume { get; set; }
		public List<Hobby> hobbies { get; set; }
	}

	public class Hobby
	{
		public string hobby_name { get; set; }
		public bool isSelected { get; set; }
	}
}