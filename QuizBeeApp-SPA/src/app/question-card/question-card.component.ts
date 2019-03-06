import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../_model/question';
import { EmitterService } from '../_services/emitter.service';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css']
})
export class QuestionCardComponent implements OnInit {

  @Input() question:Question;
  isSelected:boolean;
  
  constructor(private emiiter:EmitterService) { }

  ngOnInit() {
    this.subscribeToEmitter();
  }

  subscribeToEmitter(){
    this.emiiter.questionSelectedEvent.subscribe((qstn:Question)=>{
        this.isSelected = qstn.id == this.question.id;
    });
  }

}
