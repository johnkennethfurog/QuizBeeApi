import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../_model/question';

@Component({
  selector: 'app-question-card',
  templateUrl: './question-card.component.html',
  styleUrls: ['./question-card.component.css']
})
export class QuestionCardComponent implements OnInit {

  @Input() question:Question;
  
  constructor() { }

  ngOnInit() {
  }

}
