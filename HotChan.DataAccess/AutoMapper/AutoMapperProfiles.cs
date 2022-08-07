using AutoMapper;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;

namespace HotChan.DataAccess.AutoMapper
{
    internal class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PostDialogueDto, Post>();
        }
    }
}
