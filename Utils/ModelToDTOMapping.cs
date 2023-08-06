namespace ThreadsBackend.Utils;

using AutoMapper;
using ThreadsBackend.DTOs.Community;
using ThreadsBackend.DTOs.User;
using ThreadsBackend.DTOs.Thread;
using ThreadsBackend.Models;

public class ModelToDTOMapping : Profile
{
    public ModelToDTOMapping()
    {
        CreateMap<User, UserDTO>();
        CreateMap<Thread, ThreadDTO>();
        CreateMap<Community, CommunityDTO>();
    }
}
