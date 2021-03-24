import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import {Observable} from "rxjs/internal/Observable";

import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {NgForm} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class ReportesService {

constructor( private http:HttpClient) { }

headers: HttpHeaders = new HttpHeaders({
	"Content-Type" : "application/json",
  	Authorization: "BASIC " + localStorage.getItem("user")
  });

getReportes(){
	const url_api = "https://localhost:44381/getReporte";
 	return this.http.get(url_api,{headers:this.headers});
 }

}
