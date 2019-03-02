import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { log } from 'util';
import { Category } from '../_model/category';
import { Question } from '../_model/question';
import { stringify } from '@angular/compiler/src/util';
import { QuestionService } from '../_services/question.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-question-create',
  templateUrl: './question-create.component.html',
  styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit {
  // modalRef: BsModalRef;
  constructor(public modalRef:BsModalRef,
    private questionService:QuestionService,
    private alertifyService:AlertifyService) { }

  createQuestionForm: FormGroup;

  categories:Category[];
  qstn:Question = {timeLimit:0,
    question:"",
    type:0,
    categoryId:0,
    answer:"",
    points:0,
  eventId:0,
questionChoices:[]};
  eventId:number;

  ngOnInit() {


    this.createQuestionForm = new FormGroup({
      question:new FormControl('Hello',[Validators.required,Validators.minLength(4)]),
      categoryName:new FormControl('Easy'),
      points:new FormControl('1'),
      timeLimit:new FormControl('1'),
      type:new FormControl("0"),

      choiceA:new FormControl("aaa"),
      choiceB:new FormControl("bbb"),
      choiceC:new FormControl("ccc"),
      choiceD:new FormControl("ddd"),

      multipleChoiceAnswer:new FormControl(""),
      trueOrFalseAnswer:new FormControl("True"),
      identificationAnswer:new FormControl("")
    });
  }

  saveQuestion(){
    log(this.createQuestionForm.value);
    
    log(this.createQuestionForm.valid+"");

    if(this.createQuestionForm.valid){
     
      
     
      this.qstn.timeLimit = this.createQuestionForm.get('timeLimit').value;
      this.qstn.question = this.createQuestionForm.get('question').value;
      this.qstn.points = this.createQuestionForm.get('points').value;
      this.qstn.type = this.createQuestionForm.get('type').value;
      this.qstn.categoryId = this.categories.find(x => x.description == this.createQuestionForm.get('categoryName').value).id;
      this.qstn.answer = this.getAnswer();
      this.qstn.eventId = this.eventId;
      this.setChoices();

      this.questionService.createQuestion(this.qstn).subscribe((question:Question)=>{
        this.alertifyService.success("Question added");
      },
      error =>{
        this.alertifyService.error("Unable to save question");
      });
    }

  }

  setChoices(){
    this.qstn.questionChoices=[];
    this.qstn.questionChoices.push(this.createQuestionForm.get('choiceA').value)
    this.qstn.questionChoices.push(this.createQuestionForm.get('choiceB').value)
    this.qstn.questionChoices.push(this.createQuestionForm.get('choiceC').value)
    this.qstn.questionChoices.push(this.createQuestionForm.get('choiceD').value)
  }

  getAnswer():string{

    if(this.qstn.type == 0){
      return this.createQuestionForm.get('trueOrFalseAnswer').value;
    }
    else if(this.qstn.type ==1){
      return this.createQuestionForm.get('multipleChoiceAnswer').value;
    }
    else{
      return this.createQuestionForm.get('identificationAnswer').value;
    }
  }

  // openModal(template: TemplateRef<any>) {
  //   this.modalRef = this.modalService.show(template);
  // }
}
