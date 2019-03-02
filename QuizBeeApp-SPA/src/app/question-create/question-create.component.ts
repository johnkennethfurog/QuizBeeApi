import { Component, OnInit, TemplateRef, AfterViewInit, EventEmitter } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { log } from "util";
import { Category } from "../_model/category";
import { Question } from "../_model/question";
import { stringify } from "@angular/compiler/src/util";
import { QuestionService } from "../_services/question.service";
import { AlertifyService } from "../_services/alertify.service";

@Component({
  selector: "app-question-create",
  templateUrl: "./question-create.component.html",
  styleUrls: ["./question-create.component.css"]
})
export class QuestionCreateComponent implements OnInit,AfterViewInit {
  // modalRef: BsModalRef;
  constructor(
    public modalRef: BsModalRef,
    private questionService: QuestionService,
    private alertifyService: AlertifyService
  ) {}

  createQuestionForm: FormGroup;

  categories: Category[];
  qstn: Question;
  eventId: number;

  public questionCreatedEvent = new EventEmitter();
  public questionUpdatedEvent = new EventEmitter();

  ngAfterViewInit(){
    this.createQuestionForm.get('categoryName').setValue(this.qstn.category.description);
  }

  ngOnInit() {
    this.createQuestionForm = new FormGroup({
      question: new FormControl("Hello", [
        Validators.required,
        Validators.minLength(4)
      ]),
      categoryName: new FormControl("Easy"),
      points: new FormControl("1"),
      timeLimit: new FormControl("1"),
      type: new FormControl("0"),

      choiceA: new FormControl("aaa"),
      choiceB: new FormControl("bbb"),
      choiceC: new FormControl("ccc"),
      choiceD: new FormControl("ddd"),

      multipleChoiceAnswer: new FormControl(""),
      trueOrFalseAnswer: new FormControl("True"),
      identificationAnswer: new FormControl("")
    });

    if(this.qstn){
      this.setQuestion();
    }
  }

  setQuestion(){
    log("Type : "+this.qstn.type);
    this.createQuestionForm.get('question').setValue(this.qstn.question);
    this.createQuestionForm.get('categoryName').setValue(this.qstn.category.description);
    this.createQuestionForm.get('points').setValue(this.qstn.point);
    this.createQuestionForm.get('timeLimit').setValue(this.qstn.timeLimit);
    this.createQuestionForm.get('type').setValue(this.qstn.type.toString());
    
    this.createQuestionForm.get('choiceA').setValue(this.qstn.questionChoices[0]);
    this.createQuestionForm.get('choiceB').setValue(this.qstn.questionChoices[1]);
    this.createQuestionForm.get('choiceC').setValue(this.qstn.questionChoices[2]);
    this.createQuestionForm.get('choiceD').setValue(this.qstn.questionChoices[3]);
    
    switch(this.qstn.type)
    {
      case 0:
      this.createQuestionForm.get('trueOrFalseAnswer').setValue(this.qstn.answer);
        break;
      case 1:
      this.createQuestionForm.get('multipleChoiceAnswer').setValue(this.qstn.answer);
        break;
      case 2:
      this.createQuestionForm.get('identificationAnswer').setValue(this.qstn.answer);
        break;
    }

    log("Type : "+this.qstn.type);
    log("Category : "+this.createQuestionForm.get('categoryName').value);

    // this.createQuestionForm.get('').setValue('');
    // this.createQuestionForm.get('').setValue('');
    // this.createQuestionForm.get('').setValue('');
  }

  saveQuestion() {
    log(this.createQuestionForm.value);

    log(this.createQuestionForm.valid + "");

    if (this.createQuestionForm.valid) {
      if (this.qstn) {
        this.updateQuestion();
      } else {
        this.createNewQuestion();
      }
    }
  }

  updateQuestion(){
    this.setQuestionForSaving();

    this.questionService.updateQuestion(this.qstn,this.qstn.id).subscribe(
      (question: Question) => {
        this.alertifyService.success("Question updated");
        this.questionUpdatedEvent.emit(question);
      },
      error => {
        this.alertifyService.error("Unable to save question");
      }
    );

  }

  createNewQuestion(){
    this.qstn = {
      timeLimit: 0,
      question: "",
      type: 0,
      categoryId: 0,
      answer: "",
      point: 0,
      eventId: 0,
      questionChoices: []
    };

    this.setQuestionForSaving();
    this.questionService.createQuestion(this.qstn).subscribe(
      (question: Question) => {
        this.alertifyService.success("Question added");
        this.questionCreatedEvent.emit(question);
      },
      error => {
        this.alertifyService.error("Unable to save question");
      },
    );

  }

  setQuestionForSaving() {
    this.qstn.timeLimit = this.createQuestionForm.get("timeLimit").value;
    this.qstn.question = this.createQuestionForm.get("question").value;
    this.qstn.point = this.createQuestionForm.get("points").value;
    this.qstn.type = this.createQuestionForm.get("type").value;
    this.qstn.categoryId = this.categories.find(
      x => x.description == this.createQuestionForm.get("categoryName").value
    ).id;
    this.qstn.answer = this.getAnswer();
    this.qstn.eventId = this.eventId;
    this.setChoices();
  }

  setChoices() {
    this.qstn.questionChoices = [];
    this.qstn.questionChoices.push(
      this.createQuestionForm.get("choiceA").value
    );
    this.qstn.questionChoices.push(
      this.createQuestionForm.get("choiceB").value
    );
    this.qstn.questionChoices.push(
      this.createQuestionForm.get("choiceC").value
    );
    this.qstn.questionChoices.push(
      this.createQuestionForm.get("choiceD").value
    );
  }

  getAnswer(): string {
    if (this.qstn.type == 0) {
      return this.createQuestionForm.get("trueOrFalseAnswer").value;
    } else if (this.qstn.type == 1) {
      return this.createQuestionForm.get("multipleChoiceAnswer").value;
    } else {
      return this.createQuestionForm.get("identificationAnswer").value;
    }
  }

  cancelClick(){
    }

  // openModal(template: TemplateRef<any>) {
  //   this.modalRef = this.modalService.show(template);
  // }
}
