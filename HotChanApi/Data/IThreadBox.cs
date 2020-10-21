using HotChanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Data
{
	public interface IThreadBox
	{
		Task<Post> NewPost(Post post);
		Task<Reply> ReplyPost(long headGet, Reply reply);
		void Prune(long getId);
		void Archiver(long getId);

	}
}
