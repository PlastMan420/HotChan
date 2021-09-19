using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataBase.Models
{
	public class UserAddedPayload
	{
		public DateTimeOffset CreationTimeOffset{ get; set; }
		public string UserName { get; set; }
	}
}
