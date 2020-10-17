using HotChanBlazorServer.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotChanBlazorServer.Pages
{
	public partial class PostDialogue : ComponentBase
	{
		private readonly IHttpClientFactory _clientFactory;

		public PostDialogueDto postDialogueDto = new PostDialogueDto { name = "Anonymous"};
		public PostDialogue(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}
		public void Submit()
		{
			//var request = new HttpRequestMessage(HttpMethod.Post, );
		}
    }
}
