import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

@Component({
  selector: 'app-control',
  templateUrl: './control.component.html',
  styleUrls: ['./control.component.css']
})
export class ControlComponent implements OnInit {

 constructor(public router:Router) { }

  ngOnInit(): void {
    if(localStorage.getItem("rol") == "Chef"){
      
    }
    else{
      this.router.navigate([""]);
    }
  }

}
