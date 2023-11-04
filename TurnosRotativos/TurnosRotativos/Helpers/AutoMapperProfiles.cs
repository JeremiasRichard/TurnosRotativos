using AutoMapper;
using TurnosRotativos.DTOs;
using TurnosRotativos.Models;

namespace TurnosRotativos.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Empleado,EmpleadoCreacionDTO>().ReverseMap();
            CreateMap<Empleado,EmpleadoMuestraDTO>().ReverseMap();
            CreateMap<EmpleadoCreacionDTO, EmpleadoMuestraDTO>().ReverseMap();
            CreateMap<JornadaCreacionDTO, Jornada>().ReverseMap();

            CreateMap<JornadaCreacionMuestraDTO, Jornada>().ReverseMap()
                .ForMember(dest => dest.Concepto,
                opt => opt.MapFrom(src => src.Concepto.Nombre))
                .ForMember(dest => dest.NroDocumento,
                opt => opt.MapFrom(src => src.Empleado.NroDocumento))
                .ForMember(dest => dest.NombreCompleto,
                 opt => opt.MapFrom(src => src.Empleado.Nombre + " " + src.Empleado.Apellido))
                .ForMember(dest => dest.HsTrabajadas,
                opt => opt.MapFrom(src => src.HorasTrabajadas));

            CreateMap<JornadaMuestraDTO, Jornada>().ReverseMap()
                 .ForMember(dest => dest.Concepto,
                opt => opt.MapFrom(src => src.Concepto.Nombre))
                .ForMember(dest => dest.NroDocumento,
                opt => opt.MapFrom(src => src.Empleado.NroDocumento))
                .ForMember(dest => dest.NombreCompleto,
                 opt => opt.MapFrom(src => src.Empleado.Nombre + " " + src.Empleado.Apellido))
                .ForMember(dest => dest.HsTrabajadas,
                opt => opt.MapFrom(src => src.HorasTrabajadas));
        }
    }
}
