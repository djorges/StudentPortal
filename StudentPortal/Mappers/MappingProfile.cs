using AutoMapper;
using StudentPortal.Entities;
using StudentPortal.Models;

namespace StudentPortal.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Perfil, PerfilDto>();
            CreateMap<PerfilDto, Perfil>();

            CreateMap<Empleado, EmpleadoDto>()
                .ForMember(target => target.NombrePerfil, opt => opt.MapFrom(source => source.Perfil.Nombre));
            CreateMap<EmpleadoDto, Empleado>()
                .ForMember(dest => dest.Perfil, opt => opt.Ignore());
        }
    }
}
