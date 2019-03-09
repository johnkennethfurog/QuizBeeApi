import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../_services/event.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { AlertifyService } from '../_services/alertify.service';
import { CategoryService } from '../_services/category.service';
import { CategoryQuestions } from '../_model/categoryQuestions';
import { EmitterService } from '../_services/emitter.service';
import { Question } from '../_model/question';
import { Observable, timer } from 'rxjs';
import { CountdownComponent,Config } from 'ngx-countdown';
import { log } from 'util';
import { QuestionService } from '../_services/question.service';
import { QuestionState } from '../_enum/question-state.enum';

@Component({
  selector: 'app-question-broadcast',
  templateUrl: './question-broadcast.component.html',
  styleUrls: ['./question-broadcast.component.css']
})
export class QuestionBroadcastComponent implements OnInit {
  @ViewChild(CountdownComponent) counter: CountdownComponent;
  
  event: QuizbeeEvent;
  categoryQuestions:CategoryQuestions[];
  selectedQstn:Question;
  interval;
  timerMs:number;
  state:QuestionState= QuestionState.None;

  constructor(private route:ActivatedRoute,
    private eventService:EventService,
    private alertify:AlertifyService,
    private categortService:CategoryService,
    private emiiter:EmitterService,
    private questionService:QuestionService) { }

  ngOnInit() {
    this.loadQuestion();
    this.subscribeToEmitter();
    
  }

  subscribeToEmitter(){
    this.emiiter.questionSelectedEvent.subscribe((qstn:Question)=>{
      if(this.state != QuestionState.None)
      {
        this.alertify.confirm("Quetion on going","A question is currently on going, do you want to cancel current question?",
        ()=>{
          this.setQuestion(qstn);       
        })
      }
      else{
        this.setQuestion(qstn);       
      }
    });
  }

  config:Config;

  setQuestion(qstn:Question){

    this.state = QuestionState.None;
    this.selectedQstn = qstn;
    clearInterval(this.interval);
    this.timerMs = this.selectedQstn.timeLimit * 1000;
  }

  loadQuestion(){
    this.categortService.getCategoryQuestions(+this.route.snapshot.params['id']).subscribe((categoryQuestions:CategoryQuestions[])=>{
          this.categoryQuestions = categoryQuestions;
        },error =>{
          this.alertify.error("Unable to load questions");
        });
  }

  private clock: Observable<Date>;


  broadcastQuestion(){
    this.questionService.broadcastQuestion(this.selectedQstn).subscribe(next =>{
      this.state = QuestionState.QuestionDisplayed;
      this.emiiter.questionActiveEvent.emit(this.selectedQstn);
    },error =>{
      this.alertify.error("Unable to display question");
    });
  }

  startTimer(){

    this.questionService.startTimer().subscribe(next =>{
      this.state = QuestionState.TimerStarted;
      this.startCountDown();
  
    },error =>{
      this.alertify.error("Unable to start timer for this question");
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

  displayAnswer(){
    this.questionService.showAnswer().subscribe(next =>{
        this.state = QuestionState.AnswerDisplayed;
        clearInterval(this.interval);
        this.timerMs = 5000;
  
    },error =>{
      this.alertify.error("Unable to display answer for this question");
    });
  }

  cancelQuestion(){
    this.questionService.cancel().subscribe(next =>{
      this.state = QuestionState.None;
      clearInterval(this.interval);
  
    },error =>{
      this.alertify.error("Unable to cancel the display of  this question");
    });
  }

  cancel(){
    if(this.state == QuestionState.QuestionDisplayed || this.state == QuestionState.TimerStarted || this.state == QuestionState.AnswerDisplayed)
    {
      this.alertify.confirm("Close this question","Answer is on going are you sure you want to close this question?",()=>{
        this.cancelQuestion();
      });
    }
    else{
      this.cancelQuestion();
    }
  }

  startEvaluationPeriod(){
    this.questionService.startEvaluationPeriod().subscribe(()=>{
      this.state = QuestionState.EvaluationPeriod;
      this.startCountDown();
    },error =>{
      this.alertify.error("Unable to start evaluation period");
    })
  }

}