using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChanShared.Models
{
	[Index(nameof(UserId), IsUnique = true)]
	public class User
	{
		[Key]
		public ulong UserId				{ get; set; }
		[Required]
		public string	UserName			{ get; set; }
		[Required]
		public string	UserMail			{ get; set; }
		[Required]
		public string	KeyHash				{ get; set; }
		[Required]
		public DateTime	RegisterationDate	{ get; set; }
		[Required]
		public Uri		Avatar				{ get; set; }
		
		//public string InternalPostIds { get; set; }
		//[NotMapped]
		//public BigInteger[] PostIds 
		//{
		//	get
		//	{
		//	}
		//	set
		//	{
		//	}
		//}

	}
}
