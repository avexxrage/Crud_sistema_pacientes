import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Cita, Consultorio, EstadosCita, Medico, Paciente } from '../core/models';

@Component({
  selector: 'app-citas',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Citas</h1>
    <p class="sub">Agenda de citas médicas</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Fecha y hora *</label><input type="datetime-local" formControlName="fechaHora"><div class="err" *ngIf="form.get('fechaHora')?.touched && form.get('fechaHora')?.invalid">Campo obligatorio</div></div>
        <div><label>Motivo *</label><input type="text" formControlName="motivo"><div class="err" *ngIf="form.get('motivo')?.touched && form.get('motivo')?.invalid">Campo obligatorio</div></div>
        <div><label>Paciente *</label><select formControlName="pacienteId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of pacientes" [ngValue]="o.id">{{ o.nombres + ' ' + o.apellidos }}</option></select><div class="err" *ngIf="form.get('pacienteId')?.touched && form.get('pacienteId')?.invalid">Campo obligatorio</div></div>
        <div><label>Médico *</label><select formControlName="medicoId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of medicos" [ngValue]="o.id">{{ o.nombres + ' ' + o.apellidos }}</option></select><div class="err" *ngIf="form.get('medicoId')?.touched && form.get('medicoId')?.invalid">Campo obligatorio</div></div>
        <div><label>Consultorio *</label><select formControlName="consultorioId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of consultorios" [ngValue]="o.id">{{ o.numero }}</option></select><div class="err" *ngIf="form.get('consultorioId')?.touched && form.get('consultorioId')?.invalid">Campo obligatorio</div></div>
        <div><label>Estado *</label><select formControlName="estado"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of estadoscita" [ngValue]="o.v">{{o.t}}</option></select><div class="err" *ngIf="form.get('estado')?.touched && form.get('estado')?.invalid">Campo obligatorio</div></div>
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
        <thead><tr><th>Fecha</th><th>Paciente</th><th>Médico</th><th>Consultorio</th><th>Motivo</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.fechaHora | date:'dd/MM/yyyy HH:mm'}}</td><td>{{item.pacienteNombre}}</td><td>{{item.medicoNombre}}</td><td>{{item.consultorioNumero}}</td><td>{{item.motivo}}</td>
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
export class CitasComponent implements OnInit {
  items: Cita[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';
  pacientes: Paciente[] = [];
  medicos: Medico[] = [];
  consultorios: Consultorio[] = [];
  estadoscita = EstadosCita;

  endpoint = 'citas';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      fechaHora: ['', [Validators.required]],
      motivo: ['', [Validators.required]],
      pacienteId: ['', [Validators.required]],
      medicoId: ['', [Validators.required]],
      consultorioId: ['', [Validators.required]],
      estado: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.load();
    this.api.getAll<Paciente>('pacientes').subscribe(d => this.pacientes = d);
    this.api.getAll<Medico>('medicos').subscribe(d => this.medicos = d);
    this.api.getAll<Consultorio>('consultorios').subscribe(d => this.consultorios = d);
  }

  load(): void {
    this.api.getAll<Cita>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Cita>(this.endpoint, this.editId, body)
      : this.api.create<Cita>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Cita): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      fechaHora: (item.fechaHora ?? '').substring(0,16),
      motivo: item.motivo,
      pacienteId: item.pacienteId,
      medicoId: item.medicoId,
      consultorioId: item.consultorioId,
      estado: item.estado
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
    this.form.reset({ fechaHora: '', motivo: '', pacienteId: '', medicoId: '', consultorioId: '', estado: '' });
  }
}
