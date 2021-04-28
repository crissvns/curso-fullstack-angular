import { Evento } from './../_models/Evento';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  baseURL = 'http://localhost:5000/evento';

  constructor(private http: HttpClient) { }

  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  getAllEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoById(id: Number): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${id}`);
  }

  getEventoByTema(Tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${Tema}`);
  }

  postEvento(evento: Evento) {
    return this.http.post(this.baseURL, evento);
  }

  postUpload(file: File){
    const fileToUpload = <File> file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  putEvento(evento: Evento) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }
}
