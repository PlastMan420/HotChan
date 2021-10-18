using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HotChan.DataBase.Models;
using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Components;

namespace HotChanWasm.Pages.Catalog;

public partial class Catalog : ComponentBase
{
	static readonly HttpClient httpClient = new HttpClient();
	private Post[] PostSet;
	public Catalog()
	{
	}

	public async Task GetRecentSubmissions() 
	{
		string uri = "loalhost:5000/api/chan/";
			
		try	
  		{
			string postSetJson = await httpClient.GetStringAsync(uri);
            PostSet = await httpClient.GetFromJsonAsync<Post[]>(postSetJson);

			//Console.WriteLine(responseBody);
		}
		catch(HttpRequestException e)
		{
			Console.WriteLine("\nException Caught!");	
			Console.WriteLine("Message :{0} ",e.Message);
		}
	}
}
