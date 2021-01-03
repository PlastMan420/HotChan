using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HotChanBlazorWasm.Models
{
	public class PostDialogueDto
	{
		public string name { get; set; }
		public string title { get; set; }
		public string tags { get; set; }
		public string comment { get; set; }
		[Required]
		public IFormFile file { get; set; }
	}
}
