using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChanApi.Models
{
	public class User
	{
		[Key]
		public Guid		UserId				{ get; set; }
		public string	UserName			{ get; set; }
		public string	UserMail			{ get; set; }
		public string	KeyHash				{ get; set; }
		public DateTime	RegisterationDate	{ get; set; }
		public Uri		Avatar				{ get; set; }
		
		public string InternalPostIds { get; set; }
		[NotMapped]
		public Guid[] PostIds 
		{
			get
			{
				return Array.ConvertAll(InternalPostIds.Split(';'), Guid.Parse);
			}
			set
			{
				InternalPostIds = String.Join(";", value.Select(p => p.ToString()).ToArray());
			}
		}

	}
}
