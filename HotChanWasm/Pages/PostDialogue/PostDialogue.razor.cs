using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using HotChanWasm.Data;
using HotChanWasm.Models;
using System.IO;

namespace HotChanWasm.Pages.PostDialogue
{
	public partial class PostDialogue : ComponentBase
	{
		[Parameter]
		public string mediaData { get; set; }
		[Parameter]
		public EventCallback<string> OnChange { get; set; }


		private IList<string> imageData = new List<string>();

		public PostDialogueDto postDialogueDto = new PostDialogueDto { name = "Anonymous"};
		string status;
		string[] format = { "image/png", "image/jpeg", "image/heif", "image/avif" , "image/webp", 
			"image/gif", "image/apng", "video/mp4", "video/webm", "video/mkv" };
		public PostDialogue()
		{
			
		}

		public async Task Submit()
		{
			HttpClient httpClient = new HttpClient();
			// Serialize the request into a json
			//string postDialogueDtoJson = JsonConvert.SerializeObject(postDialogueDto);
			string postDialogueDtoJson = JsonSerializer.Serialize<PostDialogueDto>(postDialogueDto);

			var data = new StringContent(postDialogueDtoJson, Encoding.UTF8, "application/json");

			string url = "loalhost:5000/api/chan/new";
			// send POST and store result
			var response = await httpClient.PostAsync(url, data);

			string result = response.Content.ReadAsStringAsync().Result;
			
		}

		private async Task OnInputFile(InputFileChangeEventArgs e)
		{
			
			var extension = Array.Exists(format, element => element == e.File.ContentType);





		}

	}
}
