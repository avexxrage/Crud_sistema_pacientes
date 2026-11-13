import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Consultorio } from '../core/models';

@Component({
  selector: 'app-consultorios',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Consultorios</h1>
    <p class="sub">Espacios físicos de atención</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Número *</label><input type="text" formControlName="numero"><div class="err" *ngIf="form.get('numero')?.touched && form.get('numero')?.invalid">Campo obligatorio</div></div>
        <div><label>Piso *</label><input type="number" formControlName="piso"><div class="err" *ngIf="form.get('piso')?.touched && form.get('piso')?.invalid">Campo obligatorio</div></div>
        <div><label>Descripción</label><input type="text" formControlName="descripcion"></div>
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
        <thead><tr><th>Número</th><th>Piso</th><th>Descripción</th><th>Estado</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.numero}}</td><td>{{item.piso}}</td><td>{{item.descripcion}}</td><td><span class="badge" [class.on]="item.activo" [class.off]="!item.activo">{{item.activo?'Activo':'Inactivo'}}</span></td>
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
export class ConsultoriosComponent implements OnInit {
  items: Consultorio[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';



  endpoint = 'consultorios';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      numero: ['', [Validators.required]],
      piso: [0, [Validators.required, Validators.min(0)]],
      descripcion: [''],
      activo: [true]
    });
  }

  ngOnInit(): void {
    this.load();

  }

  load(): void {
    this.api.getAll<Consultorio>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Consultorio>(this.endpoint, this.editId, body)
      : this.api.create<Consultorio>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Consultorio): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      numero: item.numero,
      piso: item.piso,
      descripcion: item.descripcion,
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
    this.form.reset({ numero: '', piso: 0, descripcion: '', activo: true });
  }
}
