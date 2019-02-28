import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { log } from 'util';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  model:any={};
  constructor(private authService:AuthService,
    private alertifyService:AlertifyService) { }

  ngOnInit() {
  }

  register(){
    log("register");
    this.authService.register(this.model).subscribe(next =>{
      this.alertifyService.success("Regitration success!");
      log("register success");
    },error =>{
      this.alertifyService.error("Registration failed");
      log("register failed");
    })
  }

  cancel(){
    log("cancel register");
    this.cancelRegister.emit(false);
  }

}
