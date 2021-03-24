import { Component, OnInit } from '@angular/core';
import {ReportesService} from "src/app/services/reportes.service";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {ReportesInterface} from "src/app/interfaces/reportes-interface";
import {UsuarioInterface} from "src/app/interfaces/usuario-interface";
import {Location} from "@angular/common";
import {Router} from '@angular/router';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit {

  constructor(public router : Router, private dataApi: ReportesService) { }


  encabezado_1:string;
  encabezado_2:string;

  public  reporteVendidos = [];
  public reporteGanancias = [];
  public reporteFeedback = [];

  public datosAMostrar = [];

  ngOnInit(): void {
  	if(localStorage.getItem("rol") == "admin"){

      this.dataApi.getReportes()
      .subscribe((response:ReportesInterface) => this.cargarDatos(response));
      
      this.cargarVendidos(); 

    }
    else{
      this.router.navigate([""]);
    }
  }


  cargarDatos(data:ReportesInterface){    

    console.log(data.PlatillosMejorFeedBack[0].Nombre);

    for (var i = 0; i < 10; i++) {
      this.reporteVendidos.push({"nombre":data.PlatillosMasVendidos[i].Nombre, "valor": data.PlatillosMasVendidos[i].NumeroVentas});
    } 

     for (var i = 0; i < 10; i++) {
      this.reporteGanancias.push({"nombre":data.PlatillosConMasGanancias[i].Nombre, "valor": ((data.PlatillosConMasGanancias[i].NumeroVentas as number) *  (data.PlatillosConMasGanancias[i].Precio as number))});
    } 

     for (var i = 0; i < 10; i++) {
      this.reporteFeedback.push({"nombre":data.PlatillosMejorFeedBack[i].Nombre, "valor": data.PlatillosMejorFeedBack[i].Feedback});
    } 
  }

  cargarVendidos():void{
    this.datosAMostrar = this.reporteVendidos;
    this.encabezado_1 = "Platos";
    this.encabezado_2 = "Ventas";    

  }

  cargarGanancias():void{
    this.datosAMostrar =this.reporteGanancias;
    this.encabezado_1 = "Platos";
    this.encabezado_2 = "Ganancias (¢)";
     
  }
  
  cargarFeedback():void{
    this.datosAMostrar = this.reporteFeedback;
    this.encabezado_1 = "Platos";
    this.encabezado_2 = "Feedback";
    
  }

  cargarUsuarios():void{  
    this.datosAMostrar = [];  
    this.encabezado_1 = "Usuarios";
    this.encabezado_2 = "Ventas";

    
  }

}
