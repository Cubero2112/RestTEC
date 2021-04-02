import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {PedidosService} from "src/app/services/chef/pedidos.service";
import {PedidoInterface} from "src/app/interfaces/pedido-interface";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";

@Component({
  selector: 'app-asignar',
  templateUrl: './asignar.component.html',
  styleUrls: ['./asignar.component.css']
})
export class AsignarComponent implements OnInit {

  constructor(public router:Router, public dataApi:PedidosService ) { }

  ngOnInit(): void {
    if(localStorage.getItem("rol") == "Chef"){
      this.obtenerPedidos();      
    }
    else{
      this.router.navigate([""]);
    }
  }

  public pedidos:PedidoInterface[] = [];
  public platillosFromPedidos:PlatilloInterface[] = [];


   obtenerPedidos(){

     this.dataApi.obtenerPedidosSinChef()
     .subscribe((response:PedidoInterface[])=>{
       for (var i = 0; i < response.length; i++) {
         this.pedidos.push(response[i]);
       }
     });
   }

  agregar(orden:number){
  	 if (confirm('¿Quiere hacerse cargo de la orden ' + orden + "?")){
  	 	this.dataApi.asignarmePedido(orden)
       .subscribe(response => location.reload());
    }
  }

}
