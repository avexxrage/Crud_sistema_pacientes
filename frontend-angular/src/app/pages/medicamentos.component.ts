import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Medicamento } from '../core/models';

@Component({
  selector: 'app-medicamentos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Medicamentos</h1>
    <p class="sub">Inventario de medicamentos</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Nombre *</label><input type="text" formControlName="nombre"><div class="err" *ngIf="form.get('nombre')?.touched && form.get('nombre')?.invalid">Campo obligatorio</div></div>
        <div><label>Principio activo</label><input type="text" formControlName="principioActivo"></div>
        <div><label>Presentación</label><input type="text" formControlName="presentacion"></div>
        <div><label>Concentración</label><input type="text" formControlName="concentracion"></div>
        <div><label>Stock *</label><input type="number" formControlName="stock"><div class="err" *ngIf="form.get('stock')?.touched && form.get('stock')?.invalid">Campo obligatorio</div></div>
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
        <thead><tr><th>Nombre</th><th>Principio activo</th><th>Presentación</th><th>Concentración</th><th>Stock</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>{{item.nombre}}</td><td>{{item.principioActivo}}</td><td>{{item.presentacion}}</td><td>{{item.concentracion}}</td><td>{{item.stock}}</td>
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
export class MedicamentosComponent implements OnInit {
  items: Medicamento[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';



  endpoint = 'medicamentos';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      nombre: ['', [Validators.required]],
      principioActivo: [''],
      presentacion: [''],
      concentracion: [''],
      stock: [0, [Validators.required, Validators.min(0)]],
      activo: [true]
    });
  }

  ngOnInit(): void {
    this.load();

  }

  load(): void {
    this.api.getAll<Medicamento>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Medicamento>(this.endpoint, this.editId, body)
      : this.api.create<Medicamento>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Medicamento): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      nombre: item.nombre,
      principioActivo: item.principioActivo,
      presentacion: item.presentacion,
      concentracion: item.concentracion,
      stock: item.stock,
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
    this.form.reset({ nombre: '', principioActivo: '', presentacion: '', concentracion: '', stock: 0, activo: true });
  }
}
