using HotChanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Data
{
	public interface IThreadBox
	{
		void InitPost(ref Post post);
		Task<Post> NewPost(Post post);
		Task<Post> ReplyPost(long headGet, Post post);
		void Prune(long getId);
		void Archiver(long getId);

	}
}
