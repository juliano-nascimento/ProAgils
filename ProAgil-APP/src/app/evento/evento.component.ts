import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-evento',
  templateUrl: './evento.component.html',
  styleUrls: ['./evento.component.css']
})
export class EventoComponent implements OnInit {
  _filtroLista: string;

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
      this._filtroLista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }
  
  eventosFiltrados: any = [];
  eventos: any = [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEvento(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== - 1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
  getEventos() {
    this.eventos = this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.eventos = response;
       }, error => {
         console.log(error);
       });
  }
}
