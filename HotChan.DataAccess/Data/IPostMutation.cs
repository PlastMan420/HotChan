using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Data
{
	interface IPostMutation
	{
		public Task<Guid> AddPost(HotChanContext hotchanContext, PostDialogueDto req);
	}
}
