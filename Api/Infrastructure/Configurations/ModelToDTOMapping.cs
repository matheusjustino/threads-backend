namespace ThreadsBackend.Api.Infrastructure.Configurations;

using AutoMapper;
using ThreadsBackend.Api.Domain.Entities;
using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.DTOs.User;
using ThreadsBackend.Api.Domain.DTOs.Thread;

public class ModelToDTOMapping : Profile
{
    public ModelToDTOMapping()
    {
        this.CreateMap<User, UserDTO>();
            // .ForMember(dest => dest.Threads, opt => opt.Ignore())
            // .ForMember(dest => dest.Communities, opt => opt.Ignore());
            this.CreateMap<Thread, ThreadDTO>();
            // .ForMember(dest => dest.Author, opt => opt.Ignore())
            // .ForMember(dest => dest.Community, opt => opt.Ignore())
            // .ForMember(dest => dest.Comments, opt => opt.Ignore());
            this.CreateMap<Community, CommunityDTO>();
            // .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            // .ForMember(dest => dest.Members, opt => opt.Ignore())
            // .ForMember(dest => dest.Threads, opt => opt.Ignore());
    }
}
