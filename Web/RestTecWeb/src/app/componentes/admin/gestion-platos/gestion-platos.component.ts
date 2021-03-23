import { Component, OnInit } from '@angular/core';

import {PlatillosService} from "src/app/services/platillos.service";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {Location} from "@angular/common";

@Component({
  selector: 'app-gestion-platos',
  templateUrl: './gestion-platos.component.html',
  styleUrls: ['./gestion-platos.component.css']
})
export class GestionPlatosComponent implements OnInit {

  constructor(private dataApi: PlatillosService) { }

  public platillos: PlatilloInterface;
  private Location: Location;

  ngOnInit(): void {

  	this.obtenerPlatosServidor();
  }

  obtenerPlatosServidor():void{
  	this.dataApi.obtenerTodosPlatos()
  	.subscribe((response:PlatilloInterface) => (this.platillos = response));
  }


  preActualizar(platillo: PlatilloInterface): void{
    this.dataApi.platilloActual = Object.assign({},platillo);
  }

  eliminarPlato(platillo: PlatilloInterface):void{
    if (confirm('¿Está seguro que quiere eliminar el plato?')){
      this.dataApi.eliminarPlato(platillo)
      .subscribe(response=>location.reload());
    }    
  }

  limpiarForm():void{

  this.dataApi.platilloActual.Codigo =  null;
  this.dataApi.platilloActual.Nombre =  null;
  this.dataApi.platilloActual.Precio =  null;
  this.dataApi.platilloActual.Descripcion =  null;
  this.dataApi.platilloActual.Calorias =  null;
  this.dataApi.platilloActual.Tipo =  null;
  this.dataApi.platilloActual.Puntuacion =  null;
  this.dataApi.platilloActual.NumeroVentas =  null;
  this.dataApi.platilloActual.TiempoPreparacion =  null;
  this.dataApi.platilloActual.Feedback =  null;
  }

}
