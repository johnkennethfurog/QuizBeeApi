import { Component, OnInit } from '@angular/core';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { EmitterService } from '../_services/emitter.service';
import { EventCreateComponent } from '../event-create/event-create.component';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {

  events: QuizbeeEvent[];
  modalRef: BsModalRef;

  constructor(private eventService:EventService,
    private emitterService:EmitterService,
    private alertify:AlertifyService,
    private modalService:BsModalService) { }

  ngOnInit() {
    this.loadEvent();
    this.subscribeToEmitter();
  }
  subscribeToEmitter()
  {
    this.emitterService.eventCreatedEvent.subscribe(evnt=>{
      this.events.push(evnt);
    });

    this.emitterService.eventDeletedEvent.subscribe(evnt =>{
      var ind = this.events.indexOf(evnt);
      this.events.splice(ind,1);
    });

    this.emitterService.eventUpatedEvent.subscribe(evnt=>{
      var ind = this.events.indexOf(evnt);
      this.events[ind] = evnt;
    });

  }

  loadEvent(){
    this.eventService.getEvents().subscribe((events:QuizbeeEvent[])=>{
      this.events = events;
    },error =>{
      this.alertify.error("Unable to load events");
    })
  }

  openModalForEvent(event?:QuizbeeEvent) {
    this.modalRef = this.modalService.show(EventCreateComponent);
    this.modalRef.content.closeBtnName = 'Close';    
  }

}
