using HotChan.DataAccess.DataLoader;
using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Data
{
	public interface IPostQuery
	{
		public Task<List<Post>> GetPosts(
			HotChanContext hotchanContext,
			PostIdDL postIdDl,
			CancellationToken cancellationToken
			);


		public Task<Post> GetPostAsync(
			HotChanContext hotchanContext,
			PostIdDL postIdDl,
			CancellationToken cancellationToken,
			Guid PostId
			);
	}
}
