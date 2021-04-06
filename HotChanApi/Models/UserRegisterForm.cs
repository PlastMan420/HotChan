using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class UserRegisterForm
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public string EmailAddress { get; set; }

		// Password must not be sent over network. it has to be hashed on the client program and the hash itself gets sent
		[Required]
		public string Key { get; set; }

	}
}
