import { Component, OnInit } from '@angular/core';
import {PlatillosService} from "src/app/services/platillos.service";
import {MenuService} from "src/app/services/menu.service";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {MenuInterface} from "src/app/interfaces/menu-interface";
import {Location} from "@angular/common";
import {Router} from '@angular/router';

@Component({
  selector: 'app-gestion-menu',
  templateUrl: './gestion-menu.component.html',
  styleUrls: ['./gestion-menu.component.css']
})
export class GestionMenuComponent implements OnInit {

  constructor(public router : Router, private dataMenu: MenuService, private dataPlatos: PlatillosService) {  }

  public menuDelDia = [];
  public todosLosPlatos = [];
  public platosAMostrar = [];

  public numeroDePlatos:number;

  ngOnInit(): void {
  	if(localStorage.getItem("rol") == "Admin"){
      this.obtenerMenu();
    }
    else{
      this.router.navigate([""]);
    }
  }

  obtenerMenu(){
    this.dataMenu.obtenerMenuDia()
    .subscribe((response:MenuInterface) => 
    {
      for (var i = 0; i < response.platillos.length; i++) {
        this.menuDelDia.push({"nombre": response.platillos[i].Nombre, "codigo": response.platillos[i].Codigo,
                              "precio": response.platillos[i].Precio, "calorias": response.platillos[i].Calorias});        
        }
      this.obtenerTodosLosPlatos(this.menuDelDia);
    });
  }

  obtenerTodosLosPlatos(menu){
    this.dataPlatos.obtenerTodosPlatos()
    .subscribe((response:PlatilloInterface) => 
    {      
      for(var a in response){
        this.todosLosPlatos.push({"nombre": response[a].Nombre, "codigo": response[a].Codigo,
                                  "precio": response[a].Precio, "calorias": response[a].Calorias})
      }

      this.obtenerPlatosAMostrar(menu,this.todosLosPlatos);


    }); 
  }

  obtenerPlatosAMostrar(menu, platos){

    for (var i = 0; i < menu.length; i++) {
        let codigo = menu[i].codigo;

        for (var a = 0; a < platos.length; a++) {
          if(platos[a].codigo == codigo){
            platos.splice(a,1);
          }
        }      
    }

    this.platosAMostrar = platos;

  }

  agregarPlatoMenu(codigo:number){
    console.log(codigo);
    this.dataMenu.agregarPlatoAlMenu(codigo)
    .subscribe(response=> location.reload());
  }

  eliminarPlato(codigo:number){
    this.dataMenu.eliminarPlatoDelMenu(codigo)
    .subscribe(response=> location.reload());
  }
}
