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

using Google.Protobuf;

using HotChanShared.Models;
using static HotChanShared.Models.ChanPostView;

namespace HotChanWasm.Pages.ViewPost
{
	public partial class ViewPost : ComponentBase
	{
		private string _uri; 
		private Post _post;
		private string _errorMessage;
		private DateTime _time = DateTime.Now;
		private HttpClient _httpClient;


		public ViewPost() {
			_uri = "https://localhost:5001";
		}

	}
}