import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

import {UsuarioInterface} from "src/app/interfaces/usuario-interface";

import {LoginService} from "src/app/services/login.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public router : Router, public loginApi:LoginService) { }


  public datosUsuario:UsuarioInterface = {
    Email:null,
    ID:null,
    Password:null,
    Role:null,
    Username:null,
    Token:null
  }



  ngOnInit(){

  	if(localStorage.getItem("rol") == "admin"){
  		this.router.navigate(['admin/menu']);
  	}
  	else if(localStorage.getItem("rol") == "chef"){
  		alert("Es un chef");
  	}

    }


  verificarCredenciales():void{
    this.loginApi.verficar(this.datosUsuario)
    .subscribe((response:UsuarioInterface)=>
      {
      	alert("admitido");
        localStorage.setItem("rol", response.Role);
        localStorage.setItem("user", response.Token);
        
      });
    }

}
