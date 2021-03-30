import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {NgForm} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class MenuService {

 constructor( private http:HttpClient) { }

  headers: HttpHeaders = new HttpHeaders({
  	Authorization: "BASIC " + localStorage.getItem("user")
  });

  obtenerMenuDia(){
 	const url_api = "https://localhost:44381/menu/get";
 	return this.http.get(url_api,{headers:this.headers});
 }

 agregarPlatoAlMenu(codigo){
 	const url_api = "https://localhost:44381/menu/savePlatillo/" + codigo;
 	return this.http.post(url_api,{headers:this.headers});
 }

 eliminarPlatoDelMenu(codigo){
 	const url_api = "https://localhost:44381/menu/deletePlatillo/" + codigo;
 	return this.http.delete(url_api,{headers:this.headers});
 }
}
