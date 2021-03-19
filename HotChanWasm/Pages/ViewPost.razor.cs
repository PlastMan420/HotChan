using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using HotChanWasm.Data;
using HotChanWasm.Models;

namespace HotChanWasm.Pages
{
	public partial class ViewPost : ComponentBase
	{
		private string Uri = "http://localhost:5000";
		private Post post;
		private string errorMessage;

		public ViewPost() {}

		public async Task GetPost() {
			// https://jasonwatmore.com/post/2020/09/20/blazor-webassembly-http-get-request-examples
			HttpClient httpClient = new HttpClient();

			var p = await httpClient.GetAsync(this.Uri);

			if (!p.IsSuccessStatusCode)
    		{
				// set error message for display, log to console and return
				errorMessage = p.ReasonPhrase;
				Console.WriteLine($"HTTP GET ERROR: {errorMessage}");
				return;
			}

			this.post = await p.Content.ReadFromJsonAsync<Post>();
		}
	}
}