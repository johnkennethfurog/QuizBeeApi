import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt'
import { log } from 'util';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = environment.apiUrl + "auth/";
jwtHelper = new JwtHelperService();
decodedToken : any;
visible: boolean = true;

constructor(private http: HttpClient) { }
  signin(model:any){
    return this.http.post(this.baseUrl+"signin",model)
    .pipe(map((response:any) => {
      const user = response;
      if(user) {
        localStorage.setItem("token",user.tokenString);
        this.decodeToken(user.tokenString);
        log(this.decodedToken);

      }
    }))
  }

  decodeToken(token:string){
    log(token);
    this.decodedToken = this.jwtHelper.decodeToken(token);
  }

  register(model:any){
    return this.http.post(this.baseUrl+"register",model)
    .pipe(map((response:any) => {
      const user = response;
      if(user) {
        localStorage.setItem("token",user.tokenString);
        this.decodeToken(user.tokenString);
        log(this.decodedToken);
      }
    }))
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  navBarShow(){
    this.visible= true;
  }

  navBarHide(){
    this.visible = false;
  }
}
