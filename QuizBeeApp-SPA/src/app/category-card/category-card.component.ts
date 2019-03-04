import { Component, OnInit, Input } from '@angular/core';
import { CategoryQuestions } from '../_model/categoryQuestions';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.css']
})
export class CategoryCardComponent implements OnInit {

  @Input() category:CategoryQuestions;

  constructor() { }

  ngOnInit() {
  }

}
