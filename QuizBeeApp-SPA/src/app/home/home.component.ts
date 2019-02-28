import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  constructor(private router:Router) { }

  ngOnInit() {
  }

  registerToggle(){
   this.registerMode = true; 
  }

  cancelRegisterMode(cancel:boolean){
    this.registerMode = false;
  }
}
