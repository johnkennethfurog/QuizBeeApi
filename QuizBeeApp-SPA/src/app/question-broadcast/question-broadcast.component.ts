import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../_services/event.service';
import { QuizbeeEvent } from '../_model/quizbeeEvent';
import { AlertifyService } from '../_services/alertify.service';
import { CategoryService } from '../_services/category.service';
import { CategoryQuestions } from '../_model/categoryQuestions';

@Component({
  selector: 'app-question-broadcast',
  templateUrl: './question-broadcast.component.html',
  styleUrls: ['./question-broadcast.component.css']
})
export class QuestionBroadcastComponent implements OnInit {
  event: QuizbeeEvent;
  categoryQuestions:CategoryQuestions[];

  constructor(private route:ActivatedRoute,
    private eventService:EventService,
    private alertify:AlertifyService,
    private categortService:CategoryService) { }

  ngOnInit() {
    this.loadQuestion();
  }

  loadQuestion(){
    this.categortService.getCategoryQuestions(+this.route.snapshot.params['id']).subscribe((categoryQuestions:CategoryQuestions[])=>{
          this.categoryQuestions = categoryQuestions;
        },error =>{
          this.alertify.error("Unable to load questions");
        });
  }

  // loadEvent(){
  //   this.eventService.getEvent(+this.route.snapshot.params['id']).subscribe((event:QuizbeeEvent)=>{
  //     this.event = event;
  //   },error =>{
  //     this.alertify.error("Unable to load event");
  //   });
  // }


}
