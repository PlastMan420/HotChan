using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChan.DataBase.Models
{
	public class Favorite
	{
		public Guid UserId { get; set; }
		public Guid PostID { get; set; }
		public User User { get; set; }
		public Post Post { get; set; }
	}
}
