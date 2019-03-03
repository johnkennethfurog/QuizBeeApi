import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { EmitterService } from '../_services/emitter.service';
import { AlertifyService } from '../_services/alertify.service';
import { EventService } from '../_services/event.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-create',
  templateUrl: './event-create.component.html',
  styleUrls: ['./event-create.component.css']
})
export class EventCreateComponent implements OnInit {

  event:QuizbeeEvent;
  createEventForm:FormGroup;

  constructor(public modalRef:BsModalRef,
    private emitterService: EmitterService,
    private alertify:AlertifyService,
    private eventService:EventService) { }

  ngOnInit() {
    this.createEventForm=new FormGroup({
      name: new FormControl("",Validators.required),
      code: new FormControl("",Validators.required)
    });
    if(this.event){
      this.createEventForm.get('name').setValue(this.event.name);
      this.createEventForm.get('code').setValue(this.event.code);
    }
  }

  saveEvent()
  {
    if(this.createEventForm.valid)
    {
      if(this.event)
      {
        this.updateEvent();
      }
      else{
        this.createNewEvent();
      }
    }
    else
    {
      this.alertify.error("Kindly provide necessary field");
    }
  }

  updateEvent(){
    this.setEvent();
    this.eventService.updateEvent(this.event.id,this.event).subscribe(x =>
      {
        this.emitterService.eventUpatedEvent.emit(x);
        this.alertify.success("Event updated");
          this.modalRef.hide();
      },error =>{
        this.alertify.error("Unable to update event");
      })
  }

  createNewEvent(){
    this.event={
      name:"",
      code:"",
      id:0
    }

    this.setEvent();

      this.eventService.createEvent(this.event).subscribe(x =>
        {
          this.emitterService.eventCreatedEvent.emit(x);
          this.alertify.success("Event added");
            this.modalRef.hide();
        },error =>{
          this.alertify.error("Unable to create new event");
        })
  }

  setEvent(){
    this.event.name = this.createEventForm.get('name').value;
    this.event.code = this.createEventForm.get('code').value;
  }
}
