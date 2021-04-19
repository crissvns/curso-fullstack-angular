import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  eventosFiltrados: any = [];
  eventos: any = [];
  imagemAltura: number = 50;
  imagemMargem: number = 2;
  mostrarImagem: Boolean = false;

  constructor(private http: HttpClient) { }

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista): this.eventos;
  }

  filtrarEventos(filtrarPor: string) : any
  {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }

  ngOnInit() {
    this.getEventos();
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos()
  {
    this.http.get('http://localhost:5000/Evento').subscribe(
      response => {
        this.eventos = response;
      }, error => {
        console.log(error);
      }
    );
  }
}
