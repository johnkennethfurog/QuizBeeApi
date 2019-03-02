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
    private categoryService:CategoryService) { }

    event:QuizbeeEvent;
    modalRef: BsModalRef;
    categories: Category[];
  ngOnInit() {
    this.loadEvent();
    this.loadCategory();
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
  openModalWithComponent() {
   const initialState={
     categories:this.categories,
     eventId:this.event.id
   };
    this.modalRef = this.modalService.show(QuestionCreateComponent,{initialState});
    this.modalRef.content.closeBtnName = 'Close';
  }

}
