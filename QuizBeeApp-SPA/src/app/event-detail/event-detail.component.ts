import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { log } from 'util';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { QuestionCreateComponent } from '../question-create/question-create.component';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    private eventService:EventService,
    private alertify:AlertifyService,
    private modalService:BsModalService) { }

    event:QuizbeeEvent;
    modalRef: BsModalRef;
  ngOnInit() {
    this.loadEvent();
  }

  loadEvent(){
    this.eventService.getEvent(+this.route.snapshot.params['id']).subscribe((event:QuizbeeEvent)=>{
      this.event = event;
    },error =>{
      this.alertify.error("Unable to load event");
    });
  }
  openModalWithComponent() {
   
    this.modalRef = this.modalService.show(QuestionCreateComponent);
    this.modalRef.content.closeBtnName = 'Close';
  }

}
