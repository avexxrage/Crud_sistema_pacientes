import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApiService } from '../core/api.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <h1>Panel general</h1>
    <p class="sub">Sistema de gestión de pacientes — resumen de registros</p>
    <div class="home-grid">
      <a class="tile" routerLink="/pacientes"><div class="n">{{ counts['pacientes'] ?? '–' }}</div><div class="t">Pacientes</div></a>
      <a class="tile" routerLink="/medicos"><div class="n">{{ counts['medicos'] ?? '–' }}</div><div class="t">Médicos</div></a>
      <a class="tile" routerLink="/citas"><div class="n">{{ counts['citas'] ?? '–' }}</div><div class="t">Citas</div></a>
      <a class="tile" routerLink="/consultas"><div class="n">{{ counts['consultas'] ?? '–' }}</div><div class="t">Consultas</div></a>
      <a class="tile" routerLink="/especialidades"><div class="n">{{ counts['especialidades'] ?? '–' }}</div><div class="t">Especialidades</div></a>
      <a class="tile" routerLink="/consultorios"><div class="n">{{ counts['consultorios'] ?? '–' }}</div><div class="t">Consultorios</div></a>
      <a class="tile" routerLink="/medicamentos"><div class="n">{{ counts['medicamentos'] ?? '–' }}</div><div class="t">Medicamentos</div></a>
      <a class="tile" routerLink="/diagnosticos"><div class="n">{{ counts['diagnosticos'] ?? '–' }}</div><div class="t">Diagnósticos</div></a>
    </div>
  `
})
export class HomeComponent implements OnInit {
  counts: Record<string, number> = {};
  constructor(private api: ApiService) {}
  ngOnInit(): void {
    this.api.getAll<any>('pacientes').subscribe(d => this.counts['pacientes'] = d.length);
    this.api.getAll<any>('medicos').subscribe(d => this.counts['medicos'] = d.length);
    this.api.getAll<any>('citas').subscribe(d => this.counts['citas'] = d.length);
    this.api.getAll<any>('consultas').subscribe(d => this.counts['consultas'] = d.length);
    this.api.getAll<any>('especialidades').subscribe(d => this.counts['especialidades'] = d.length);
    this.api.getAll<any>('consultorios').subscribe(d => this.counts['consultorios'] = d.length);
    this.api.getAll<any>('medicamentos').subscribe(d => this.counts['medicamentos'] = d.length);
    this.api.getAll<any>('diagnosticos').subscribe(d => this.counts['diagnosticos'] = d.length);
  }
}
