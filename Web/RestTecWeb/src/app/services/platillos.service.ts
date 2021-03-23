import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import {Observable} from "rxjs/internal/Observable";

import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {NgForm} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class PlatillosService {
constructor(private http:HttpClient) { }

  headers: HttpHeaders = new HttpHeaders({
  	"Content-Type" : "application/json",
  	Authorization: "BASIC QXB1MTM6QXB1Q29udHJhMTM="
  });


  public platilloActual: PlatilloInterface = {
  	Codigo: null,
	Nombre: null,
	Precio: null,
	Descripcion: null,
	Calorias: null,
	Tipo: null,
	Puntuacion: null,
	NumeroVentas: null,
	TiempoPreparacion: null,
	Feedback: null,
  }


 obtenerTodosPlatos(){
 	const url_api = "https://localhost:44381/getPlatillos";
 	return this.http.get(url_api,{headers:this.headers});
 }

 insertarPlato(platillo:NgForm){
 	const url_api = "https://localhost:44381/insertPlatillo";
 	return this.http.post(url_api,
 		{
 			Nombre: platillo.value.Nombre,
 			Precio: platillo.value.Precio,
 			Calorias: platillo.value.Calorias,
 			Tipo: platillo.value.Tipo,
 			Descripcion: platillo.value.Descripcion,
 			TiempoPreparacion: platillo.value.TiempoPreparacion
 		},
 		{headers:this.headers});
 }

 actualizarPlato(platillo:NgForm){
 	const url_api = "https://localhost:44381/actualizarPlatillo";
 	return this.http.put(url_api,
 		{
 			Codigo: platillo.value.Codigo,
 			Nombre: platillo.value.Nombre,
 			Precio: platillo.value.Precio,
 			Calorias: platillo.value.Calorias,
 			Tipo: platillo.value.Tipo,
 			Descripcion: platillo.value.Descripcion,
 			TiempoPreparacion: platillo.value.TiempoPreparacion
 		},
 		{headers:this.headers});
 }

 eliminarPlato(platillo: PlatilloInterface){
 	const url_api = "https://localhost:44381/eliminarPlatillo";
 	return this.http.put(url_api,
 		{
 			Codigo: platillo.Codigo,
 			Nombre: platillo.Nombre,
 			Precio: platillo.Precio,
 			Calorias: platillo.Calorias,
 			Tipo: platillo.Tipo,
 			Descripcion: platillo.Descripcion,
 			TiempoPreparacion: platillo.TiempoPreparacion
 		},
 		{headers:this.headers});
 }
}
