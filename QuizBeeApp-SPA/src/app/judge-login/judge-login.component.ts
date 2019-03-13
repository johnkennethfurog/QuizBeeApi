import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { JudgeService } from '../_services/judge.service';
import { AlertifyService } from '../_services/alertify.service';
import { Judge } from '../_model/judge';
import { Router } from '@angular/router';

@Component({
  selector: 'app-judge-login',
  templateUrl: './judge-login.component.html',
  styleUrls: ['./judge-login.component.css']
})
export class JudgeLoginComponent implements OnInit,OnDestroy {

  refNo:string ="";

  constructor(private authService:AuthService,
    private judgeService:JudgeService,
    private alertify:AlertifyService,
    private router:Router) { }

  ngOnInit() {
    this.authService.navBarHide();
  }

  ngOnDestroy(){
    this.authService.navBarShow();
  }

  signIn(){
    if(this.refNo.length == 0)
    {
      this.alertify.error("You need to provide reference number");
      return;
    }
    
    this.judgeService.signIn(this.refNo).subscribe((judge:Judge) =>{
      localStorage.setItem("judgeId",""+judge.id);
      localStorage.setItem("judgeName",judge.name);
      this.router.navigate(['/judge-wall']);

    },error =>{
      this.alertify.error("Unable to sign in");
    });
  }

}
