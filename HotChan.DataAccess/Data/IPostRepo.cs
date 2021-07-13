using HotChan.DataBase.Models;
using System;
using System.Collections.Generic;

namespace HotChan.DataAccess.Data
{
	public interface IPostRepo
	{
		public Post GetPost(Guid Id);
		public IEnumerable<Post> PostCatalog();
	}
}
