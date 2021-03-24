import { Component, OnInit } from '@angular/core';
import {PlatillosService} from "src/app/services/platillos.service";
import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {Location} from "@angular/common";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-modal-platos',
  templateUrl: './modal-platos.component.html',
  styleUrls: ['./modal-platos.component.css']
})
export class ModalPlatosComponent implements OnInit {

  constructor(public dataApiService: PlatillosService) { }

  public platillos: PlatilloInterface;
  private Location: Location;

  ngOnInit(): void {
  }

  guardarPlatillo(platilloForm: NgForm):void{
  	if(platilloForm.value.Codigo == null){
  		this.dataApiService.insertarPlato(platilloForm).subscribe(response=>location.reload());
  	}else{
  		this.dataApiService.actualizarPlato(platilloForm).subscribe(response=>location.reload());
  	}

  }
}
