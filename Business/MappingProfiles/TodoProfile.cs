using AutoMapper;
using Business.Dtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<CreateTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();
        }
    }
}
