import { Component, OnInit } from '@angular/core';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {

  events: QuizbeeEvent[];

  constructor(private eventService:EventService,
    private alertify:AlertifyService) { }

  ngOnInit() {
    this.loadEvent();
  }

  loadEvent(){
    this.eventService.getEvents().subscribe((events:QuizbeeEvent[])=>{
      this.events = events;
    },error =>{
      this.alertify.error("Unable to load events");
    })
  }

}
