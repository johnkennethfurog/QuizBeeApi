import { Component, OnInit } from '@angular/core';
import { log } from 'util';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService,
    private alertifyService: AlertifyService,
    private route:Router) { }
  model:any = {};
  ngOnInit() {
  }

  login() {
    this.authService.signin(this.model).subscribe(next =>{
      this.alertifyService.success("Login success");
      log("Login success");
    },error =>{
      this.alertifyService.error("Login failed");
      log("login failed");
    }, ()=>{
      this.route.navigate(['/event']);
    })
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem("token");
    this.route.navigate(['/home']);
    log("user logged out");
  }

}
