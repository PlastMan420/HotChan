using AutoMapper;
using HotChan.Models;

namespace HotChan.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<PostDialogueDto, Post>();
		}
	}
}
