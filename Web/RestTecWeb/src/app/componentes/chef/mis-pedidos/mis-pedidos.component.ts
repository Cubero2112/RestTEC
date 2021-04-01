import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-mis-pedidos',
  templateUrl: './mis-pedidos.component.html',
  styleUrls: ['./mis-pedidos.component.css']
})
export class MisPedidosComponent implements OnInit {

	constructor(public router:Router) { }

	  ngOnInit(): void {
	    if(localStorage.getItem("rol") == "Chef"){
	      
	    }
	    else{
	      this.router.navigate([""]);
	    }
	  }

}
