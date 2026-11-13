using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Application.Mappings;

/// <summary>
/// Perfil de AutoMapper. Centraliza la conversión Entidad &lt;-&gt; DTO en un solo lugar.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Especialidad, EspecialidadDto>();
        CreateMap<EspecialidadCreateDto, Especialidad>();
        CreateMap<Consultorio, ConsultorioDto>();
        CreateMap<ConsultorioCreateDto, Consultorio>();
        CreateMap<MedicoCreateDto, Medico>();
        CreateMap<Paciente, PacienteDto>();
        CreateMap<PacienteCreateDto, Paciente>();
        CreateMap<CitaCreateDto, Cita>();
        CreateMap<ConsultaCreateDto, Consulta>();
        CreateMap<SignoVital, SignoVitalDto>();
        CreateMap<SignoVitalCreateDto, SignoVital>();
        CreateMap<Diagnostico, DiagnosticoDto>();
        CreateMap<DiagnosticoCreateDto, Diagnostico>();
        CreateMap<Tratamiento, TratamientoDto>();
        CreateMap<TratamientoCreateDto, Tratamiento>();
        CreateMap<Medicamento, MedicamentoDto>();
        CreateMap<MedicamentoCreateDto, Medicamento>();
        CreateMap<Receta, RecetaDto>();
        CreateMap<RecetaCreateDto, Receta>();
        CreateMap<RecetaDetalleCreateDto, RecetaDetalle>();

        // Mapeos con nombres de entidades relacionadas (requieren Include en el servicio)
        CreateMap<Medico, MedicoDto>()
            .ForMember(d => d.EspecialidadNombre, o => o.MapFrom(s => s.Especialidad != null ? s.Especialidad.Nombre : null));

        CreateMap<Cita, CitaDto>()
            .ForMember(d => d.PacienteNombre, o => o.MapFrom(s => s.Paciente != null ? s.Paciente.Nombres + " " + s.Paciente.Apellidos : null))
            .ForMember(d => d.MedicoNombre, o => o.MapFrom(s => s.Medico != null ? s.Medico.Nombres + " " + s.Medico.Apellidos : null))
            .ForMember(d => d.ConsultorioNumero, o => o.MapFrom(s => s.Consultorio != null ? s.Consultorio.Numero : null));

        CreateMap<Consulta, ConsultaDto>()
            .ForMember(d => d.PacienteNombre, o => o.MapFrom(s => s.Cita != null && s.Cita.Paciente != null ? s.Cita.Paciente.Nombres + " " + s.Cita.Paciente.Apellidos : null));

        CreateMap<RecetaDetalle, RecetaDetalleDto>()
            .ForMember(d => d.MedicamentoNombre, o => o.MapFrom(s => s.Medicamento != null ? s.Medicamento.Nombre : null));
    }
}
