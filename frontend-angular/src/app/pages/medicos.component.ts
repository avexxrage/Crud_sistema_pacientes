import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Especialidad, Medico } from '../core/models';

@Component({
  selector: 'app-medicos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Médicos</h1>
    <p class="sub">Profesionales registrados</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Nombres *</label><input type="text" formControlName="nombres"><div class="err" *ngIf="form.get('nombres')?.touched && form.get('nombres')?.invalid">Campo obligatorio</div></div>
        <div><label>Apellidos *</label><input type="text" formControlName="apellidos"><div class="err" *ngIf="form.get('apellidos')?.touched && form.get('apellidos')?.invalid">Campo obligatorio</div></div>
        <div><label>Cédula profesional *</label><input type="text" formControlName="cedulaProfesional"><div class="err" *ngIf="form.get('cedulaProfesional')?.touched && form.get('cedulaProfesional')?.invalid">Campo obligatorio</div></div>
        <div><label>Teléfono</label><input type="text" formControlName="telefono"></div>
        <div><label>Email</label><input type="email" formControlName="email"></div>
        <div><label>Especialidad *</label><select formControlName="especialidadId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of especialidades" [ngValue]="o.id">{{ o.nombre }}</option></select><div class="err" *ngIf="form.get('especialidadId')?.touched && form.get('especialidadId')?.invalid">Campo obligatorio</div></div>
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
        <thead><tr><th>Nombre</th><th>Cédula</th><th>Especialidad</th><th>Email</th><th>Estado</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.nombres}} {{item.apellidos}}</td><td>{{item.cedulaProfesional}}</td><td>{{item.especialidadNombre}}</td><td>{{item.email}}</td><td><span class="badge" [class.on]="item.activo" [class.off]="!item.activo">{{item.activo?'Activo':'Inactivo'}}</span></td>
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
export class MedicosComponent implements OnInit {
  items: Medico[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';
  especialidades: Especialidad[] = [];


  endpoint = 'medicos';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      nombres: ['', [Validators.required]],
      apellidos: ['', [Validators.required]],
      cedulaProfesional: ['', [Validators.required]],
      telefono: [''],
      email: [''],
      especialidadId: ['', [Validators.required]],
      activo: [true]
    });
  }

  ngOnInit(): void {
    this.load();
    this.api.getAll<Especialidad>('especialidades').subscribe(d => this.especialidades = d);
  }

  load(): void {
    this.api.getAll<Medico>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Medico>(this.endpoint, this.editId, body)
      : this.api.create<Medico>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Medico): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      nombres: item.nombres,
      apellidos: item.apellidos,
      cedulaProfesional: item.cedulaProfesional,
      telefono: item.telefono,
      email: item.email,
      especialidadId: item.especialidadId,
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
    this.form.reset({ nombres: '', apellidos: '', cedulaProfesional: '', telefono: '', email: '', especialidadId: '', activo: true });
  }
}
