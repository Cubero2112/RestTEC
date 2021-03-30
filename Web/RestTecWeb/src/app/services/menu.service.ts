import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {NgForm} from "@angular/forms";
import {Observable} from "rxjs/internal/Observable";

@Injectable({
  providedIn: 'root'
})
export class MenuService {

 constructor( private http:HttpClient) { }


 headers: HttpHeaders = new HttpHeaders({
  	"Content-Type" : "application/json",
  	Authorization: "BASIC " + localStorage.getItem("user")
  });


  /*headers: HttpHeaders = new HttpHeaders({
  	"Content-Type" : "application/x-www-form-urlencoded;charset=utf-8",
  	Authorization: "BASIC YWRtaW46YWRtaW4x"
  });*/

  obtenerMenuDia(){
 	const url_api = "https://localhost:44381/menu/get";
 	return this.http.get(url_api,{headers:this.headers});
 }

 agregarPlatoAlMenu(codigo){
 	const url_api = "https://localhost:44381/menu/savePlatillo";
 	return this.http.post(url_api,{
 			Codigo: codigo
 		},{headers:this.headers});
 }

 eliminarPlatoDelMenu(codigo){
 	const url_api = "https://localhost:44381/menu/deletePlatillo/" + codigo;
 	return this.http.delete(url_api,{headers:this.headers});
 }
}
