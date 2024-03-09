using AutoMapper;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, ApplicationUser>().ReverseMap();
                config.CreateMap<SecurityQuestionRequestDto, SecurityQuestions>().ReverseMap();
            });
            return mappingConfig;

        }
    }
}
