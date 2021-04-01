import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-asignar',
  templateUrl: './asignar.component.html',
  styleUrls: ['./asignar.component.css']
})
export class AsignarComponent implements OnInit {

  constructor(public router:Router) { }

  ngOnInit(): void {
    if(localStorage.getItem("rol") == "Chef"){
      
    }
    else{
      this.router.navigate([""]);
    }
  }

  agregar(){
  	 if (confirm('¿Quiere hacerse cargo?')){
  	 	console.log("Agregado");
    }
  }

}
