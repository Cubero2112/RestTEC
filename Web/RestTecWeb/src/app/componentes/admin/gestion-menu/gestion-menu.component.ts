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

  /*Se crean listas para almacenar la info proveniente del server
  Inicializan vacías
  */
  public menuDelDia = [];
  public todosLosPlatos = [];
  public platosAMostrar = [];

  public numeroDePlatos:number;


/*Al iniciar el componente se verifica el rol del usuario que esta loggeado
si no hay nadie loggeado lo manda a la pagina de inicio de sesio*/

  ngOnInit(): void {
  	if(localStorage.getItem("rol") == "Admin"){
      this.obtenerMenu();
    }
    else{
      this.router.navigate([""]);
    }
  }

  /*Metodo para obtener el menu del día alojado en el servidor
  Hace uso de las listas anteriormente creadas
  No retorna ningun valor, sino que llama a otro metodo*/


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

  /*Entrada: menu => una lista con el menu del día guardado en el servidor
  Obtiene todos los platos que están disponibles, aun si no estan incluidos en el menu
  Llama finalmente a un metodo que se encarga de mostrar los platos al usuario*/

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

  /* Entrada: menu => una lista con el menu del día guardado en el servidor
              platos => todos los platos del sistema
     Se encarga de sacar la interseccion entre las dos listas que recibe y muestra al usuario el resultado*/
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

  /*Metodo para agregar un plato al menu
  Entrada:  codigo:number => codigo del plato que se quiere agregar*/

  agregarPlatoMenu(codigo:number){
    console.log(codigo);
    this.dataMenu.agregarPlatoAlMenu(codigo)
    .subscribe(response=> location.reload());
  }

  /*Metodo para eliminar  un plato al menu
  Entrada:  codigo:number => codigo del plato que se quiere agregar*/
  
  eliminarPlato(codigo:number){
    this.dataMenu.eliminarPlatoDelMenu(codigo)
    .subscribe(response=> location.reload());
  }
}
