using HotChanBlazorServer.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace HotChanBlazorServer.Pages
{
	public partial class PostDialogue : ComponentBase
	{
		private readonly IHttpClientFactory _clientFactory;

		public PostDialogueDto postDialogueDto = new PostDialogueDto { name = "Anonymous"};
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
	}
}
