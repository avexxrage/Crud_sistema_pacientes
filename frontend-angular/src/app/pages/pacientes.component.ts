import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Generos, Paciente } from '../core/models';

@Component({
  selector: 'app-pacientes',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Pacientes</h1>
    <p class="sub">Registro de pacientes</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Nombres *</label><input type="text" formControlName="nombres"><div class="err" *ngIf="form.get('nombres')?.touched && form.get('nombres')?.invalid">Campo obligatorio</div></div>
        <div><label>Apellidos *</label><input type="text" formControlName="apellidos"><div class="err" *ngIf="form.get('apellidos')?.touched && form.get('apellidos')?.invalid">Campo obligatorio</div></div>
        <div><label>Cédula *</label><input type="text" formControlName="cedula"><div class="err" *ngIf="form.get('cedula')?.touched && form.get('cedula')?.invalid">Campo obligatorio</div></div>
        <div><label>Fecha de nacimiento *</label><input type="date" formControlName="fechaNacimiento"><div class="err" *ngIf="form.get('fechaNacimiento')?.touched && form.get('fechaNacimiento')?.invalid">Campo obligatorio</div></div>
        <div><label>Género *</label><select formControlName="genero"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of generos" [ngValue]="o.v">{{o.t}}</option></select><div class="err" *ngIf="form.get('genero')?.touched && form.get('genero')?.invalid">Campo obligatorio</div></div>
        <div><label>Teléfono</label><input type="text" formControlName="telefono"></div>
        <div><label>Email</label><input type="email" formControlName="email"></div>
        <div><label>Dirección</label><input type="text" formControlName="direccion"></div>
        <div><label>Tipo de sangre</label><input type="text" formControlName="tipoSangre"></div>
        <div><label>Activo</label><select formControlName="activo"><option [ngValue]="true">Sí</option><option [ngValue]="false">No</option></select></div>
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
        <thead><tr><th>Nombre</th><th>Cédula</th><th>Nacimiento</th><th>Teléfono</th><th>Sangre</th><th>Estado</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.nombres}} {{item.apellidos}}</td><td>{{item.cedula}}</td><td>{{item.fechaNacimiento | date:'dd/MM/yyyy'}}</td><td>{{item.telefono}}</td><td>{{item.tipoSangre}}</td><td><span class="badge" [class.on]="item.activo" [class.off]="!item.activo">{{item.activo?'Activo':'Inactivo'}}</span></td>
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
export class PacientesComponent implements OnInit {
  items: Paciente[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';

  generos = Generos;

  endpoint = 'pacientes';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      nombres: ['', [Validators.required]],
      apellidos: ['', [Validators.required]],
      cedula: ['', [Validators.required]],
      fechaNacimiento: ['', [Validators.required]],
      genero: ['', [Validators.required]],
      telefono: [''],
      email: [''],
      direccion: [''],
      tipoSangre: [''],
      activo: [true]
    });
  }

  ngOnInit(): void {
    this.load();

  }

  load(): void {
    this.api.getAll<Paciente>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Paciente>(this.endpoint, this.editId, body)
      : this.api.create<Paciente>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Paciente): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      nombres: item.nombres,
      apellidos: item.apellidos,
      cedula: item.cedula,
      fechaNacimiento: (item.fechaNacimiento ?? '').substring(0,10),
      genero: item.genero,
      telefono: item.telefono,
      email: item.email,
      direccion: item.direccion,
      tipoSangre: item.tipoSangre,
      activo: item.activo
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
    this.form.reset({ nombres: '', apellidos: '', cedula: '', fechaNacimiento: '', genero: '', telefono: '', email: '', direccion: '', tipoSangre: '', activo: true });
  }
}
