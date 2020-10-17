using System.ComponentModel.DataAnnotations;

namespace HotChanBlazorServer.Models
{
	public class PostDialogueDto
	{
		public string board { get; set; }
		public string name { get; set; }
		[Required]
		public string title { get; set; }
		public string flags { get; set; }
		public string comment { get; set; }

		public string mediaUrl { get; set; }
	}
}
