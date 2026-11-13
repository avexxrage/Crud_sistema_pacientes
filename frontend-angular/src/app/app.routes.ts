import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/home.component').then(m => m.HomeComponent) },
  { path: 'especialidades', loadComponent: () => import('./pages/especialidades.component').then(m => m.EspecialidadesComponent) },
  { path: 'consultorios', loadComponent: () => import('./pages/consultorios.component').then(m => m.ConsultoriosComponent) },
  { path: 'medicamentos', loadComponent: () => import('./pages/medicamentos.component').then(m => m.MedicamentosComponent) },
  { path: 'medicos', loadComponent: () => import('./pages/medicos.component').then(m => m.MedicosComponent) },
  { path: 'pacientes', loadComponent: () => import('./pages/pacientes.component').then(m => m.PacientesComponent) },
  { path: 'citas', loadComponent: () => import('./pages/citas.component').then(m => m.CitasComponent) },
  { path: 'consultas', loadComponent: () => import('./pages/consultas.component').then(m => m.ConsultasComponent) },
  { path: 'diagnosticos', loadComponent: () => import('./pages/diagnosticos.component').then(m => m.DiagnosticosComponent) },
  { path: '**', redirectTo: '' }
];
