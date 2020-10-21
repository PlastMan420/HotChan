using System;
using Xunit;
using HotChanBlazorServer;
using Microsoft.AspNetCore.Components.Testing;
using HotChanBlazorServer.Pages;

namespace HotChanBlazorServerTest
{
	public class UnitTest1
	{
		private TestHost _host = new TestHost();

		[Fact]
		public void Test1()
		{

			var component = _host.AddComponent<PostDialogue>();

		}
	}
}
