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
import { Participant } from '../_model/participant';
import { ParticipantService } from '../_services/participant.service';
import { ParticipantCreateComponent } from '../participant-create/participant-create.component';
import { Judge } from '../_model/judge';
import { JudgeService } from '../_services/judge.service';

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
    private questionService:QuestionService,
    private participantService:ParticipantService,
    private judgeService:JudgeService) { }

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

    this.emitterService.userCreatedEvent.subscribe(participant=>{
      this.event.participants.push(participant);
    })

    this.emitterService.userUpatedEvent.subscribe((participant)=>{
      var ind = this.event.participants.findIndex(x => x.id == participant.id);
      this.event.participants[ind] = participant;
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

  // QUESTION REALED CODE
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


// JUDGE RELATED CODE

openModalForJudge(judge?:Judge) {
  const initialState={
    eventCode:this.event.code,
    judge:judge
  };

  this.modalRef = this.modalService.show(ParticipantCreateComponent,{initialState});
  this.modalRef.content.closeBtnName = 'Close';

}

  verifyPJudge(judge:Judge){

    this.judgeService.verify(judge.id).subscribe(x =>{
      judge.isVerify = true;
      this.alertify.success("Judge has been verify");
    },error =>{
      this.alertify.error("Unable to verify judge");
    })
  }
  
  updateJudge(judge:Judge){
    this.openModalForJudge(judge);
  }

  deleteJudge(judge:Judge){

    this.alertify.confirm("Delete judge","Are you sure ypu want to remove this judge?",()=>{
      this.judgeService.deleteJudge(judge.id).subscribe(x =>{
        var ind = this.event.judges.indexOf(judge);
        this.event.judges.splice(ind,1);
      },
      error =>{
        this.alertify.error("Unable to delete participant");
      })
  
    });

  }

  // PARTICIPANT RELATED CODE

openModalForParticipant(participant?:Participant) {
  const initialState={
    eventCode:this.event.code,
    participant:participant
  };

  this.modalRef = this.modalService.show(ParticipantCreateComponent,{initialState});
  this.modalRef.content.closeBtnName = 'Close';

}

  verifyParticipant(participant:Participant){

    this.participantService.verify(participant.id).subscribe(x =>{
      participant.isVerify = true;
      this.alertify.success("Participant has been verify");
    },error =>{
      this.alertify.error("Unable to verify participant");
    })
  }
  
  updateParticipant(participant:Participant){
    this.openModalForParticipant(participant);
  }

  deleteParticipant(participant:Participant){

    this.alertify.confirm("Delete participant","Are you sure ypu want to remove this participant?",()=>{
      this.participantService.deleteParticipant(participant.id).subscribe(x =>{
        var ind = this.event.participants.indexOf(participant);
        this.event.participants.splice(ind,1);
      },
      error =>{
        this.alertify.error("Unable to delete participant");
      })
  
    });

  }
  

}
