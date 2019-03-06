import { Component, OnInit, Input } from '@angular/core';
import { CategoryQuestions } from '../_model/categoryQuestions';
import { Question } from '../_model/question';
import { EmitterService } from '../_services/emitter.service';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.css']
})
export class CategoryCardComponent implements OnInit {

  @Input() category:CategoryQuestions;

  constructor(private emitter:EmitterService) { }

  ngOnInit() {
  }

  selectQuestion(qstn:Question){
    this.emitter.questionSelectedEvent.emit(qstn);
  }

}
