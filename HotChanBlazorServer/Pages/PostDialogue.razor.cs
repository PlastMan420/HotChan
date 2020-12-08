using HotChanBlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace HotChanBlazorServer.Pages
{
	public partial class PostDialogue : ComponentBase
	{
		private readonly IHttpClientFactory _clientFactory;
		private IList<string> imageDataUrls = new List<string>();

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
			string postDialogueDtoJson = JsonConvert.SerializeObject(postDialogueDto);
			var data = new StringContent(postDialogueDtoJson, Encoding.UTF8, "application/json");

			string url = "loalhost:5000/api/chan/new";
			
			var response = await httpClient.PostAsync(url, data);

			string result = response.Content.ReadAsStringAsync().Result;
			
		}

		private async Task OnInputFileChange(InputFileChangeEventArgs e)
		{
			
			var extension = Array.Exists(format, element => element == e.File.ContentType);

			var buffer = new byte[e.File.Size];
			await e.File.OpenReadStream().ReadAsync(buffer);
			var imageDataUrl =
				$"data:{format};base64,{Convert.ToBase64String(buffer)}";
			imageDataUrls.Add(imageDataUrl);
			
		}
	}
}
