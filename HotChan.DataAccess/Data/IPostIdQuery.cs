using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;

namespace HotChan.DataAccess.Data
{
	public interface IPostIdQuery
	{
		public Post GetPostById(HotChanContext context, Guid PostId);
	}
}
