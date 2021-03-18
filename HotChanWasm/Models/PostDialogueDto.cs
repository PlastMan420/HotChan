using System.ComponentModel.DataAnnotations;

namespace HotChanWasm.Models
{
	public class PostDialogueDto
	{
		public string name { get; set; }
		public string title { get; set; }
		public string tags { get; set; }
		public string comment { get; set; }
		[Required]
		public string file { get; set; }
	}
}
