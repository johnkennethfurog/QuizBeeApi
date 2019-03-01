import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { log } from 'util';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    private eventService:EventService,
    private alertify:AlertifyService) { }

    event:QuizbeeEvent;

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

}
