using AutoMapper;

namespace Present.WebMvc.App_Start.ObjectMapping.Users
{
    public static class UsersMapping
    {
        public static IConfiguration MapUsers(this IConfiguration mapper)
        {
         //   mapper.CreateMap<FetchViewFilter, FetchViewFilterSettingsRequest>();
           
            return mapper;
        }
    }
}