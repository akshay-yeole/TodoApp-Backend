using AutoMapper;
using TA.Entities.DTOs;
using TA.Entities.Models;

namespace TA.WebApi.Common
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Todo, TodoDTO>();
            CreateMap<TodoDTO, Todo> ();
        }
    }
}
