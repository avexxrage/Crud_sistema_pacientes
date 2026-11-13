import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

/// Servicio genérico que centraliza las llamadas HTTP al API (CRUD REST).
@Injectable({ providedIn: 'root' })
export class ApiService {
  private base = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getAll<T>(endpoint: string): Observable<T[]> {
    return this.http.get<T[]>(`${this.base}/${endpoint}`);
  }
  getById<T>(endpoint: string, id: number): Observable<T> {
    return this.http.get<T>(`${this.base}/${endpoint}/${id}`);
  }
  create<T>(endpoint: string, body: unknown): Observable<T> {
    return this.http.post<T>(`${this.base}/${endpoint}`, body);
  }
  update<T>(endpoint: string, id: number, body: unknown): Observable<T> {
    return this.http.put<T>(`${this.base}/${endpoint}/${id}`, body);
  }
  delete(endpoint: string, id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/${endpoint}/${id}`);
  }
}
