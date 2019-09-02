using AutoMapper;
using DomainLayer;
using DomainLayer.Identity;
using DtoLayer;

namespace KODOTIFront.Config
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ApplicationUser, UserGetDto>().ReverseMap();
                cfg.CreateMap<ArtistCreateDto, Artist>();
                cfg.CreateMap<ArtistUpdateDto, Artist>();
            });
        }
    }
}
