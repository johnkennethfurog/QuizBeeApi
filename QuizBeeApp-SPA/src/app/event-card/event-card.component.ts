import { Component, OnInit, Input } from '@angular/core';
import { QuizbeeEvent } from '../_model/quizbeeEvent';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {
@Input() event:QuizbeeEvent;
  constructor() { }

  ngOnInit() {
  }

}
