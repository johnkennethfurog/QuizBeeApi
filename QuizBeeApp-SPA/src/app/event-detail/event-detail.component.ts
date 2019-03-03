import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { log } from 'util';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { QuestionCreateComponent } from '../question-create/question-create.component';
import { CategoryService } from '../_services/category.service';
import { Category } from '../_model/category';
import { Question } from '../_model/question';
import { EmitterService } from '../_services/emitter.service';
import { QuestionService } from '../_services/question.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    private eventService:EventService,
    private alertify:AlertifyService,
    private modalService:BsModalService,
    private categoryService:CategoryService,
    private emitterService:EmitterService,
    private questionService:QuestionService) { }

    event:QuizbeeEvent;
    modalRef: BsModalRef;
    categories: Category[];
  ngOnInit() {
    this.loadEvent();
    this.loadCategory();
    this.subscribeToEmitter();
  }

  subscribeToEmitter(){
    this.emitterService.questionCreatedEvent.subscribe((question)=>{
      this.event.quizItems.push(question);
    })

    this.emitterService.questionUpdatedEvent.subscribe((question)=>{
      var ind = this.event.quizItems.findIndex(x => x.id == question.id);
      this.event.quizItems[ind] = question;
    });
  }

  loadCategory(){
    this.categoryService.getCategories().subscribe((categories:Category[])=>{
      this.categories = categories;
    })
  }

  loadEvent(){
    this.eventService.getEvent(+this.route.snapshot.params['id']).subscribe((event:QuizbeeEvent)=>{
      this.event = event;
    },error =>{
      this.alertify.error("Unable to load event");
    });
  }
  openModalWithComponent(question?:Question) {
   const initialState={
     categories:this.categories,
     eventId:this.event.id,
     qstn:question
   };
  
    this.modalRef = this.modalService.show(QuestionCreateComponent,{initialState});
    this.modalRef.content.closeBtnName = 'Close';
  }

  updateQuestion(question:Question) {
    this.openModalWithComponent(question);
    log(question.question + " click");
  }

  deleteQuestion(question:Question){
    this.alertify.confirm("Delete question","Are you sure ypu want to remove this question?",()=>{
      this.delteQuestionFromServer(question);
    });
  }

  delteQuestionFromServer(question:Question){
    this.questionService.delteQuestion(question.id).subscribe(next =>{
      var ind = this.event.quizItems.indexOf(question);
      this.event.quizItems.splice(ind,1);
      this.alertify.success("Question deleted");
    },
    error =>{
      this.alertify.error("Unable to remove question");
    })
  }

  

}
