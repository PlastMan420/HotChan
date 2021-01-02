using HotChanBlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace HotChanBlazorServer.Pages
{
	public partial class Catalog : ComponentBase
	{
		static readonly HttpClient client = new HttpClient();
		private Post[] PostSet;
		public Catalog()
		{
		}

		public async Task GetRecentSubmissions() 
		{
			string uri = "loalhost:5000/api/chan/";
			
			try	
  			{
				string postSetJson = await client.GetStringAsync(uri);
            	PostSet = await client.GetFromJsonAsync<Post[]>(postSetJson);

				//Console.WriteLine(responseBody);
			}
			catch(HttpRequestException e)
			{
				Console.WriteLine("\nException Caught!");	
				Console.WriteLine("Message :{0} ",e.Message);
			}
		}
	}
}