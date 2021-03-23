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

    }


  verificarCredenciales():void{
    this.loginApi.verficar(this.datosUsuario)
    .subscribe((response:UsuarioInterface)=>
      {
        alert(response.Token + "\n" + response.Role);
      });
    }

}
