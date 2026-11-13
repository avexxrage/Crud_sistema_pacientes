// Interfaces que reflejan los DTOs del API .NET

export interface Especialidad { id:number; nombre:string; descripcion?:string; activo:boolean; }
export interface Consultorio { id:number; numero:string; piso:number; descripcion?:string; activo:boolean; }
export interface Medico { id:number; nombres:string; apellidos:string; cedulaProfesional:string; telefono?:string; email?:string; activo:boolean; especialidadId:number; especialidadNombre?:string; }
export interface Paciente { id:number; nombres:string; apellidos:string; cedula:string; fechaNacimiento:string; genero:number; telefono?:string; email?:string; direccion?:string; tipoSangre?:string; activo:boolean; }
export interface Cita { id:number; fechaHora:string; motivo:string; estado:number; pacienteId:number; pacienteNombre?:string; medicoId:number; medicoNombre?:string; consultorioId:number; consultorioNumero?:string; }
export interface Consulta { id:number; fechaConsulta:string; motivoConsulta:string; observaciones?:string; peso?:number; altura?:number; citaId:number; pacienteNombre?:string; }
export interface Diagnostico { id:number; codigoCie10:string; descripcion:string; tipo:number; consultaId:number; }
export interface Medicamento { id:number; nombre:string; principioActivo?:string; presentacion?:string; concentracion?:string; stock:number; activo:boolean; }

export const Generos = [ {v:1,t:'Masculino'}, {v:2,t:'Femenino'}, {v:3,t:'Otro'} ];
export const EstadosCita = [ {v:1,t:'Programada'}, {v:2,t:'Atendida'}, {v:3,t:'Cancelada'} ];
export const TiposDiagnostico = [ {v:1,t:'Presuntivo'}, {v:2,t:'Definitivo'} ];
