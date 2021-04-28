import { Evento } from './../_models/Evento';
import { EventoService } from './../_services/evento.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale)

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  eventosFiltrados: Evento[] = [];
  evento: Evento;
  eventos: Evento[] = [];
  imagemAltura: number = 50;
  imagemMargem: number = 2;
  mostrarImagem: Boolean = false;
  registerForm: FormGroup;
  modoSalvar = 'post';
  bodyDeletarEvento = '';
  file: File;

  constructor(private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService) {
    this.localeService.use('pt-br');
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(+this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
        this.toastr.success('Deletado com sucesso');
      }, error => {
        this.toastr.error(error);
      }
    );
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.evento.imagemURL = '';
    this.registerForm.patchValue(evento);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = _eventos;
      }, error => {
        this.toastr.error(error);
      }
    );
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  onFileChange(event) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
      console.log(this.file);
    }
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar == 'post') {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postUpload(this.file).subscribe();
        let nomeArquivo = this.evento.imagemURL.split('\\');
        this.evento.imagemURL = nomeArquivo[nomeArquivo.length - 1];

        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com sucesso');
          }, error => {
            this.toastr.error(error);
          }
        );
      }
      else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);

        this.eventoService.postUpload(this.file).subscribe();
        let nomeArquivo = this.evento.imagemURL.split('\\');
        this.evento.imagemURL = nomeArquivo[nomeArquivo.length - 1];

        this.eventoService.putEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com sucesso');
          }, error => {
            this.toastr.error(error);
          }
        );
      }
    }
  }

  validation() {
    this.registerForm = this.fb.group({
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }
}
