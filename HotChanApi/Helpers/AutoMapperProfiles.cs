using AutoMapper;
using HotChanShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			// Entity Models
			CreateMap<PostDialogueDto, Post>();

			// View Models
			CreateMap<Post, PostReply>();

		}
	}
}
