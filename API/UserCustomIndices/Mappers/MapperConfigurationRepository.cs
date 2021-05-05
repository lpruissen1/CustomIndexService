using AutoMapper;
using Database.Model.User.CustomIndices;
using UserCustomIndices.Core.Model;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Database.Model.User.CustomIndices;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Mappers
{
    public static class MapperConfigurationRepository
    {
        public static IConfigurationProvider Create()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomIndex, CustomIndexRequest>();
                cfg.CreateMap<CustomIndexRequest, CustomIndex>().ForMember(x => x.Id, opt => opt.Ignore());
            });
        }
    }
}
