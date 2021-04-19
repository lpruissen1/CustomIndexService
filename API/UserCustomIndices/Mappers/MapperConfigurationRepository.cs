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
                cfg.CreateMap<CustomIndex, CustomIndexResponse>();
                cfg.CreateMap<CustomIndexRequest, CustomIndex>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<CustomIndexRule, Rule>();
                cfg.CreateMap<Rule, CustomIndexRule>().ForMember(x => x.Id, opt => opt.Ignore());
            });
        }
    }
}
