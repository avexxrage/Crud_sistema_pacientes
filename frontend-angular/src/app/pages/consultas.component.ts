import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Cita, Consulta } from '../core/models';

@Component({
  selector: 'app-consultas',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Consultas</h1>
    <p class="sub">Consultas realizadas</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Cita *</label><select formControlName="citaId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of citas" [ngValue]="o.id">{{ '#' + o.id + ' - ' + (o.pacienteNombre || '') }}</option></select><div class="err" *ngIf="form.get('citaId')?.touched && form.get('citaId')?.invalid">Campo obligatorio</div></div>
        <div><label>Fecha *</label><input type="datetime-local" formControlName="fechaConsulta"><div class="err" *ngIf="form.get('fechaConsulta')?.touched && form.get('fechaConsulta')?.invalid">Campo obligatorio</div></div>
        <div><label>Motivo *</label><input type="text" formControlName="motivoConsulta"><div class="err" *ngIf="form.get('motivoConsulta')?.touched && form.get('motivoConsulta')?.invalid">Campo obligatorio</div></div>
        <div style="grid-column:1/-1"><label>Observaciones</label><textarea rows="2" formControlName="observaciones"></textarea></div>
        <div><label>Peso (kg)</label><input type="number" formControlName="peso"></div>
        <div><label>Altura (m)</label><input type="number" formControlName="altura"></div>
        </div>
        <div class="actions">
          <button type="submit" class="btn">{{ editId ? 'Actualizar' : 'Guardar' }}</button>
          <button type="button" class="btn-light" *ngIf="editId" (click)="resetForm()">Cancelar</button>
        </div>
      </form>
    </div>

    <div class="card">
      <h2>Listado</h2>
      <table>
        <thead><tr><th>Fecha</th><th>Paciente</th><th>Motivo</th><th>Peso</th><th>Altura</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.fechaConsulta | date:'dd/MM/yyyy HH:mm'}}</td><td>{{item.pacienteNombre}}</td><td>{{item.motivoConsulta}}</td><td>{{item.peso}}</td><td>{{item.altura}}</td>
            <td>
              <button class="btn-sm btn-edit" (click)="edit(item)">Editar</button>
              <button class="btn-sm btn-del" (click)="remove(item.id)">Eliminar</button>
            </td>
          </tr>
          <tr *ngIf="items.length === 0"><td colspan="20" class="empty">Sin registros todavía.</td></tr>
        </tbody>
      </table>
    </div>
  `
})
export class ConsultasComponent implements OnInit {
  items: Consulta[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';
  citas: Cita[] = [];


  endpoint = 'consultas';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      citaId: ['', [Validators.required]],
      fechaConsulta: ['', [Validators.required]],
      motivoConsulta: ['', [Validators.required]],
      observaciones: [''],
      peso: [''],
      altura: ['']
    });
  }

  ngOnInit(): void {
    this.load();
    this.api.getAll<Cita>('citas').subscribe(d => this.citas = d);
  }

  load(): void {
    this.api.getAll<Consulta>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Consulta>(this.endpoint, this.editId, body)
      : this.api.create<Consulta>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Consulta): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      citaId: item.citaId,
      fechaConsulta: (item.fechaConsulta ?? '').substring(0,16),
      motivoConsulta: item.motivoConsulta,
      observaciones: item.observaciones,
      peso: item.peso,
      altura: item.altura
    });
  }

  remove(id: number): void {
    if (!confirm('¿Eliminar este registro?')) return;
    this.api.delete(this.endpoint, id).subscribe({
      next: () => this.load(),
      error: (e) => this.error = 'No se pudo eliminar (puede tener registros relacionados).'
    });
  }

  resetForm(): void {
    this.editId = null;
    this.error = '';
    this.form.reset({ citaId: '', fechaConsulta: '', motivoConsulta: '', observaciones: '', peso: '', altura: '' });
  }
}
