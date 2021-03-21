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

namespace HotChanWasm.Pages.PostDialogue
{
	public partial class PostDialogue : ComponentBase
	{
		[Parameter]
		public string ImgUrl { get; set; }
		[Parameter]
		public EventCallback<string> OnChange { get; set; }
		//[Inject]
		//public IProductHttpRepository Repository { get; set; }

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
			//string postDialogueDtoJson = JsonConvert.SerializeObject(postDialogueDto);
			string postDialogueDtoJson = JsonSerializer.Serialize<PostDialogueDto>(postDialogueDto);

			var data = new StringContent(postDialogueDtoJson, Encoding.UTF8, "application/json");

			string url = "loalhost:5000/api/chan/new";
			// send POST and store result
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

		//private async Task HandleSelected(InputFileChangeEventArgs e)
		//{
		//	var imageFiles = e.GetMultipleFiles();
		//	foreach (var imageFile in imageFiles)
		//	{
		//		if (imageFile != null)
		//		{
		//			var resizedFile = await imageFile.RequestImageFileAsync("image/png", 300, 500);

		//			using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
		//			{
		//				var content = new MultipartFormDataContent();
		//				content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
		//				content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
		//				ImgUrl = await Repository.UploadProductImage(content);
		//				await OnChange.InvokeAsync(ImgUrl);
		//			}
		//		}
		//	}
		//}
	}
}
