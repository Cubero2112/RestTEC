import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import {PedidoInterface} from "src/app/interfaces/pedido-interface"

@Injectable({
  providedIn: 'root'
})
export class PedidosService {


constructor( private http:HttpClient) { }

headers: HttpHeaders = new HttpHeaders({
	"Content-Type" : "application/json",
  	Authorization: "BASIC " + localStorage.getItem("user")
  });



 obtenerPedidosSinChef(){
 	const url_api = "https://localhost:44381/chef/getPedidos";
 	return this.http.get(url_api,{headers:this.headers});
 }

 obtenerPedidosDelChef(){
 	const url_api = "https://localhost:44381/chef/getMisPedidos";
 	return this.http.get(url_api,{headers:this.headers});
 }

 asignarmePedido(orden:number){
 	const url_api = "https://localhost:44381/chef/InitPedido/";
 	return this.http.post(url_api,{Orden: orden}, {headers:this.headers});
 }

 finalizarPedido(orden:number){
 	const url_api = "https://localhost:44381/chef/FinishPedido";
 	return this.http.post(url_api,{Orden: orden}, {headers:this.headers});
 }


}
