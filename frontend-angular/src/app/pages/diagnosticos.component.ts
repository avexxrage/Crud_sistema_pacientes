import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../core/api.service';
import { Consulta, Diagnostico, TiposDiagnostico } from '../core/models';

@Component({
  selector: 'app-diagnosticos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <h1>Diagnósticos</h1>
    <p class="sub">Diagnósticos por consulta</p>

    <div class="card">
      <h2>{{ editId ? 'Editar registro #' + editId : 'Nuevo registro' }}</h2>
      <div class="err" *ngIf="error" style="margin-bottom:10px">{{ error }}</div>
      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="grid">
        <div><label>Consulta *</label><select formControlName="consultaId"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of consultas" [ngValue]="o.id">{{ '#' + o.id + ' - ' + (o.motivoConsulta || '') }}</option></select><div class="err" *ngIf="form.get('consultaId')?.touched && form.get('consultaId')?.invalid">Campo obligatorio</div></div>
        <div><label>Código CIE-10 *</label><input type="text" formControlName="codigoCie10"><div class="err" *ngIf="form.get('codigoCie10')?.touched && form.get('codigoCie10')?.invalid">Campo obligatorio</div></div>
        <div><label>Descripción *</label><input type="text" formControlName="descripcion"><div class="err" *ngIf="form.get('descripcion')?.touched && form.get('descripcion')?.invalid">Campo obligatorio</div></div>
        <div><label>Tipo *</label><select formControlName="tipo"><option [ngValue]="''" disabled>Seleccione…</option><option *ngFor="let o of tiposdiagnostico" [ngValue]="o.v">{{o.t}}</option></select><div class="err" *ngIf="form.get('tipo')?.touched && form.get('tipo')?.invalid">Campo obligatorio</div></div>
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
        <thead><tr><th>Consulta</th><th>CIE-10</th><th>Descripción</th><th>Tipo</th><th style="width:120px"></th></tr></thead>
        <tbody>
          <tr *ngFor="let item of items">
            <td>#{{item.consultaId}}</td><td>{{item.codigoCie10}}</td><td>{{item.descripcion}}</td><td>{{item.tipo===1?'Presuntivo':'Definitivo'}}</td>
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
export class DiagnosticosComponent implements OnInit {
  items: Diagnostico[] = [];
  form: FormGroup;
  editId: number | null = null;
  error = '';
  consultas: Consulta[] = [];
  tiposdiagnostico = TiposDiagnostico;

  endpoint = 'diagnosticos';

  constructor(private api: ApiService, private fb: FormBuilder) {
    this.form = this.fb.group({
      consultaId: ['', [Validators.required]],
      codigoCie10: ['', [Validators.required]],
      descripcion: ['', [Validators.required]],
      tipo: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.load();
    this.api.getAll<Consulta>('consultas').subscribe(d => this.consultas = d);
  }

  load(): void {
    this.api.getAll<Diagnostico>(this.endpoint).subscribe(d => this.items = d);
  }

  submit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const body: any = { ...this.form.value };
    Object.keys(body).forEach(k => { if (body[k] === '') body[k] = null; });
    const req = this.editId
      ? this.api.update<Diagnostico>(this.endpoint, this.editId, body)
      : this.api.create<Diagnostico>(this.endpoint, body);
    req.subscribe({
      next: () => { this.resetForm(); this.load(); },
      error: (e) => this.error = 'No se pudo guardar. Revise los datos. ' + (e?.error?.detalle ?? '')
    });
  }

  edit(item: Diagnostico): void {
    this.editId = item.id;
    this.error = '';
    this.form.patchValue({
      consultaId: item.consultaId,
      codigoCie10: item.codigoCie10,
      descripcion: item.descripcion,
      tipo: item.tipo
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
    this.form.reset({ consultaId: '', codigoCie10: '', descripcion: '', tipo: '' });
  }
}
