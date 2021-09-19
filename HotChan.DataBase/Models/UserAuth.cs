using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotChan.DataBase.Models
{
	public class UserAuth
	{
		public string UserName { get; set; }

		[EmailAddress]
		public string UserMail { get; set; }

		[Required]
		public string Key { get; set; }
	}
}
