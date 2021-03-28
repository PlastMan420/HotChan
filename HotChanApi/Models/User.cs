using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class User
	{
		[Key]
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string UserMail { get; set; }
		public DateTime	RegisterationDate { get; set; }
		public Uri		Avatar { get; set; }

	}
}
