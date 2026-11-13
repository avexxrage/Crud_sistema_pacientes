using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Enums;

namespace SistemaPacientes.Infrastructure.Persistence;

/// <summary>
/// Siembra (seed) de datos de ejemplo para poder probar el sistema de inmediato.
/// </summary>
public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Especialidades.Any()) return; // Ya inicializado

        var cardiologia = new Especialidad { Nombre = "Cardiología", Descripcion = "Enfermedades del corazón" };
        var pediatria = new Especialidad { Nombre = "Pediatría", Descripcion = "Atención a niños" };
        var medicinaGeneral = new Especialidad { Nombre = "Medicina General", Descripcion = "Atención general" };
        context.Especialidades.AddRange(cardiologia, pediatria, medicinaGeneral);

        var c1 = new Consultorio { Numero = "101", Piso = 1, Descripcion = "Planta baja" };
        var c2 = new Consultorio { Numero = "205", Piso = 2, Descripcion = "Ala norte" };
        context.Consultorios.AddRange(c1, c2);

        var m1 = new Medico { Nombres = "Laura", Apellidos = "Gómez", CedulaProfesional = "MED-0001", Email = "laura.gomez@hospital.com", Especialidad = cardiologia };
        var m2 = new Medico { Nombres = "Carlos", Apellidos = "Ramírez", CedulaProfesional = "MED-0002", Email = "carlos.ramirez@hospital.com", Especialidad = pediatria };
        context.Medicos.AddRange(m1, m2);

        var p1 = new Paciente { Nombres = "Ana", Apellidos = "Martínez", Cedula = "001-1234567-8", FechaNacimiento = new DateTime(1990, 5, 12), Genero = Genero.Femenino, Telefono = "809-555-1010", TipoSangre = "O+" };
        var p2 = new Paciente { Nombres = "Pedro", Apellidos = "Fernández", Cedula = "402-7654321-0", FechaNacimiento = new DateTime(1985, 11, 3), Genero = Genero.Masculino, Telefono = "829-555-2020", TipoSangre = "A-" };
        context.Pacientes.AddRange(p1, p2);

        var med1 = new Medicamento { Nombre = "Paracetamol", PrincipioActivo = "Acetaminofén", Presentacion = "Tableta", Concentracion = "500mg", Stock = 200 };
        var med2 = new Medicamento { Nombre = "Amoxicilina", PrincipioActivo = "Amoxicilina", Presentacion = "Cápsula", Concentracion = "500mg", Stock = 120 };
        context.Medicamentos.AddRange(med1, med2);

        var cita1 = new Cita { FechaHora = DateTime.UtcNow.AddDays(1), Motivo = "Chequeo general", Estado = EstadoCita.Programada, Paciente = p1, Medico = m1, Consultorio = c1 };
        context.Citas.Add(cita1);

        context.SaveChanges();

        var consulta1 = new Consulta { Cita = cita1, MotivoConsulta = "Dolor de cabeza", Observaciones = "Paciente estable", Peso = 65m, Altura = 1.68m };
        context.Consultas.Add(consulta1);
        context.SaveChanges();

        context.SignosVitales.Add(new SignoVital { Consulta = consulta1, PresionArterial = "120/80", FrecuenciaCardiaca = 72, Temperatura = 36.5m, SaturacionOxigeno = 98 });
        var diag1 = new Diagnostico { Consulta = consulta1, CodigoCie10 = "R51", Descripcion = "Cefalea", Tipo = TipoDiagnostico.Definitivo };
        context.Diagnosticos.Add(diag1);
        context.SaveChanges();

        context.Tratamientos.Add(new Tratamiento { Diagnostico = diag1, Descripcion = "Analgésico", Indicaciones = "1 tableta cada 8 horas", DuracionDias = 3 });

        var receta1 = new Receta { Consulta = consulta1, Notas = "Tomar con alimentos" };
        context.Recetas.Add(receta1);
        context.SaveChanges();

        context.RecetaDetalles.Add(new RecetaDetalle { Receta = receta1, Medicamento = med1, Dosis = "500mg", Frecuencia = "Cada 8 horas", Duracion = "3 días", Cantidad = 9 });
        context.SaveChanges();
    }
}
