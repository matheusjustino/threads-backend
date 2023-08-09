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
        this.CreateMap<Thread, ThreadDTO>();
        this.CreateMap<Community, CommunityDTO>();
    }
}
