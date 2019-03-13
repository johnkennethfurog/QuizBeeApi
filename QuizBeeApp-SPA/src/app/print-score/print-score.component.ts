import { Component, OnInit } from '@angular/core';
import { ParticipantScore } from '../_model/participantScore';
import { ParticipantService } from '../_services/participant.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, Route, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-print-score',
  templateUrl: './print-score.component.html',
  styleUrls: ['./print-score.component.css']
})
export class PrintScoreComponent implements OnInit {

  participantScores:ParticipantScore[];
  constructor(private participantService:ParticipantService,
    private alertify:AlertifyService,
    private router:Router,
    private route:ActivatedRoute) { }

  ngOnInit() {
    this.loadScores();
  }

  loadScores(){
    this.participantService.getParticipantScores(+this.route.snapshot.params["id"])
    .subscribe((scores:ParticipantScore[]) =>{
      this.participantScores = scores;
    },error =>{
      this.alertify.error("Unable to fetch scores");
    });
  }

   PrintElem()
{
  window.print();}
}
