import { Component, OnInit, Input } from '@angular/core';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { EventService } from '../_services/event.service';
import { EmitterService } from '../_services/emitter.service';
import { AlertifyService } from '../_services/alertify.service';
import { EventCreateComponent } from '../event-create/event-create.component';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {
  @Input() event:QuizbeeEvent;
  constructor(private eventService:EventService,
    private modalService:BsModalService,
    private emitterService:EmitterService,
    private alertify:AlertifyService) { }

  modalRef: BsModalRef;


  ngOnInit() {
  }


    // EVENT RELATED CODE        
      updateEvent(){
        const initialState={
          event:this.event
        };
      
        this.modalRef = this.modalService.show(EventCreateComponent,{initialState});
        this.modalRef.content.closeBtnName = 'Close';
  
      }
    
      deleteEvent(){
    
        this.alertify.confirm("Delete Event","Are you sure ypu want to remove this Event?",()=>{
          this.eventService.deleteEvent(this.event.id).subscribe(x =>{
            this.emitterService.eventDeletedEvent.emit(this.event);
          },
          error =>{
            this.alertify.error("Unable to delete Event");
          })
      
        });
    
      }
}
