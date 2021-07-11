using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChan.DataBase.Models
{
	public class Tag
	{
		public string TagText { get; set; }
		public Guid PostId { get; set; }
		public Post Post { get; set; }
	}
}
