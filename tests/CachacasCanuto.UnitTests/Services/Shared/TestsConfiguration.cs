using AutoMapper;
using CachacasCanuto.Application.AutoMapper;

namespace CachacasCanuto.UnitTests.Services.Shared
{
    public abstract class TestsConfiguration
    {
        protected IMapper _mapper;
        
        protected TestsConfiguration()
        {
            if (_mapper is null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
    }
}
