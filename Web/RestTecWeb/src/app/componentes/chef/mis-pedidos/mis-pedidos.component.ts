import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {PedidosService} from "src/app/services/chef/pedidos.service";
import {PedidoInterface} from "src/app/interfaces/pedido-interface";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";

@Component({
  selector: 'app-mis-pedidos',
  templateUrl: './mis-pedidos.component.html',
  styleUrls: ['./mis-pedidos.component.css']
})
export class MisPedidosComponent implements OnInit {

	constructor(public router:Router, public dataApi:PedidosService) { }

	  ngOnInit(): void {
	    if(localStorage.getItem("rol") == "Chef"){
	      this.obtenerMisPedidos();
	    }
	    else{
	      this.router.navigate([""]);
	    }
	  }

	public pedidos:PedidoInterface[] = [];
  	public platillosFromPedidos:PlatilloInterface[] = [];

	obtenerMisPedidos(){
		this.dataApi.obtenerPedidosDelChef()
     	.subscribe((response:PedidoInterface[])=>{
       		for (var i = 0; i < response.length; i++) {
         		this.pedidos.push(response[i]);
         	}
     });
	}

	finalizar(orden:number){

		if (confirm('¿Quiere finalizar la orden ' + orden + "?")){
  	 		this.dataApi.finalizarPedido(orden)
  	 		.subscribe(response=>location.reload());
  	 	}
	}

}
