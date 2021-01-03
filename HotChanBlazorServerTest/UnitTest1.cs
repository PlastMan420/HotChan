using System;
using Xunit;
using HotChanBlazorWasm;
using Microsoft.AspNetCore.Components.Testing;
using HotChanBlazorWasm.Pages;

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
