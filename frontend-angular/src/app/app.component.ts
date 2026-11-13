import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="app">
      <aside class="sidebar">
        <div class="brand">Sistema de Pacientes<small>CRUD .NET N-Capas + Angular</small></div>
        <nav class="nav">
        <a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Inicio</a>
        <a routerLink="/pacientes" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Pacientes</a>
        <a routerLink="/medicos" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Médicos</a>
        <a routerLink="/especialidades" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Especialidades</a>
        <a routerLink="/consultorios" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Consultorios</a>
        <a routerLink="/citas" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Citas</a>
        <a routerLink="/consultas" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Consultas</a>
        <a routerLink="/diagnosticos" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Diagnósticos</a>
        <a routerLink="/medicamentos" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Medicamentos</a>
        </nav>
      </aside>
      <main class="content"><router-outlet></router-outlet></main>
    </div>
  `
})
export class AppComponent {}
