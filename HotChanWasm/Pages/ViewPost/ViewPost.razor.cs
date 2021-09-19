using Microsoft.AspNetCore.Components;
using HotChanWasmClient.GraphQL;
using System.Reactive.Linq;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace HotChanWasm.Pages.ViewPost;

public partial class ViewPost : ComponentBase, IDisposable
{
    [Parameter] public Guid postid { get; set; }
    [Inject] private IHotChanWasmClient HotChanWasmClient { get; set;  }
    private IDisposable storeSession;

    // post
    string postTitle;
    string postDescription;
    Uri postMediaUri;


    protected override async Task OnInitializedAsync()
    {
        var result = await HotChanWasmClient.GetPostById.ExecuteAsync();
        postTitle = result.Data.PostById.PostTitle;
    }

    protected override void OnInitialized()
    {
        storeSession =
            HotChanWasmClient
                .GetPostById
                .Watch(StrawberryShake.ExecutionStrategy.CacheFirst)
                .Where(t => !t.Errors.Any())
                .Select(t => t.Data.PostById.PostTitle)
                .Subscribe(result =>
                {
                    postTitle = result;
                    StateHasChanged();
                });
    }

	public void Dispose()
	{
        storeSession?.Dispose();
    }
}