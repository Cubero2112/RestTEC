import { Component, OnInit } from '@angular/core';
import {PlatillosService} from "src/app/services/platillos.service";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {Location} from "@angular/common";
import {Router} from '@angular/router';

@Component({
  selector: 'app-gestion-menu',
  templateUrl: './gestion-menu.component.html',
  styleUrls: ['./gestion-menu.component.css']
})
export class GestionMenuComponent implements OnInit {

  constructor(public router : Router, private dataApi: PlatillosService) {  }

  ngOnInit(): void {
  	if(localStorage.getItem("rol") == "admin"){
      
    }
    else{
      this.router.navigate([""]);
    }
  }

}
