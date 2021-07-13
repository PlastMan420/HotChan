using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Data
{
	public class PostRepo : IPostRepo
	{
		private readonly HotChanContext _hotchanContext;

		public PostRepo(HotChanContext context)
		{
			_hotchanContext = context;
		}

		public Post GetPost(Guid PostId)
		{
			return _hotchanContext.Posts.FirstOrDefault(x => x.PostId == PostId);
		}

		public IEnumerable<Post> PostCatalog()
		{
			return _hotchanContext.Posts;
		}
	}
}
