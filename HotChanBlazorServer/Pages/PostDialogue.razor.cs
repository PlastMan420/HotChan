using HotChan.Controllers;
using HotChan.Data;
using HotChan.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotChan.Pages
{
	public partial class PostDialogue : ComponentBase
	{
		private readonly IHttpClientFactory _clientFactory;

		PostDialogueDto postDialogueDto = new PostDialogueDto();
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
