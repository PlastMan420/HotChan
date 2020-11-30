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

		public PostDialogueDto postDialogueDto = new PostDialogueDto { name = "Anonymous"};
		string status;
		enum mimes
		{
			apng,
			bmp,
			gif,
			jpg,
			jpeg,
			jfif,
			png,
			webp,
			heic,
			avif,
			mp4,
			webm
		}
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

		async Task HandleSelection(IFileListEntry[] files)
		{
			var file = files.FirstOrDefault();
			if (file != null)
			{
				// Just load into .NET memory to show it can be done
				// Alternatively it could be saved to disk, or parsed in memory, or similar
				var ms = new MemoryStream();
				await file.Data.CopyToAsync(ms);

				status = $"Finished loading {file.Size} bytes from {file.Name}";
			}
		}
	}
}
