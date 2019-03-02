import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { log } from 'util';


@Component({
  selector: 'app-question-create',
  templateUrl: './question-create.component.html',
  styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent implements OnInit {
  // modalRef: BsModalRef;
  constructor(public modalRef:BsModalRef,) { }

  createQuestionForm: FormGroup;

  ngOnInit() {
    this.createQuestionForm = new FormGroup({
      question:new FormControl('Hello',[Validators.required,Validators.minLength(4)]),
      category:new FormControl(''),
      points:new FormControl(),
      timeLimit:new FormControl(),
      answer:new FormControl(),
      type:new FormControl("2")
    });
  }

  saveQuestion(){
    log(this.createQuestionForm.value);
    
    log(this.createQuestionForm.valid+"");
  }

  // openModal(template: TemplateRef<any>) {
  //   this.modalRef = this.modalService.show(template);
  // }
}
