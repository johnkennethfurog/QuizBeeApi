import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from '../_services/signal-r.service';
import { HttpClient } from '@angular/common/http';
import { EmitterService } from '../_services/emitter.service';
import { Question } from '../_model/question';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-question-display',
  templateUrl: './question-display.component.html',
  styleUrls: ['./question-display.component.css']
})
export class QuestionDisplayComponent implements OnInit,OnDestroy {

  interval;
  timerMs:number;
  selectedQstn:Question;
  displayAnswer:boolean = false;

  constructor(private signalR:SignalRService,
    private emitter:EmitterService,
    private authService:AuthService) { }


  ngOnDestroy(){
    this.authService.navBarShow();
  }
  ngOnInit() {
    this.signalR.startConnection();
    
    this.signalR.receiveQuestipnListener();
    this.signalR.cancelQuestionListener();
    this.signalR.displayAnswerListner();
    this.signalR.timerStartListner();

    this.authService.navBarHide();
    this.subscribeToEmitter();
  }

  subscribeToEmitter(){
    this.emitter.questionReceivedEvent.subscribe((question:Question)=>
    {
      this.displayAnswer = false;
      this.selectedQstn = question;
      this.timerMs = this.selectedQstn.timeLimit * 1000;
    });
    this.emitter.questionTimerStartedEvent.subscribe(()=>{
      this.startCountDown();
    });
    this.emitter.questionAnswerDisplayedEvent.subscribe(()=>{
      this.displayAnswer = true;
    });
    this.emitter.questionCancelledEvent.subscribe(()=>{
      this.selectedQstn = null;
    });
  }

  startCountDown(){
    this.interval = setInterval(() => {
      if(this.timerMs > 0) {
        this.timerMs-=1000;
      }
      else{
        clearInterval(this.interval);
      }
    },1000);

  }

}
