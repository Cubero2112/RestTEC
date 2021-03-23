import { Injectable } from '@angular/core';

import {HttpClient, HttpHeaders} from "@angular/common/http"
import {Observable} from "rxjs/internal/Observable";

import {UsuarioInterface} from "src/app/interfaces/usuario-interface";
import {NgForm} from "@angular/forms";


@Injectable({
  providedIn: 'root'
})
export class LoginService {

 constructor(private http:HttpClient) { }

  headers: HttpHeaders = new HttpHeaders({
  	"Content-Type" : "application/json",
  });

  verficar(user:UsuarioInterface){
 	const url_api = "https://localhost:44381/Login";
 	return this.http.post(url_api,
 		{
 			Username: user.Username,
 			Password: user.Password

 		});
 }
}
