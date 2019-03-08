import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { EmitterService } from '../_services/emitter.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Participant } from '../_model/participant';
import { log } from 'util';
import { AlertifyService } from '../_services/alertify.service';
import { ParticipantService } from '../_services/participant.service';

@Component({
  selector: 'app-participant-create',
  templateUrl: './participant-create.component.html',
  styleUrls: ['./participant-create.component.css']
})
export class ParticipantCreateComponent implements OnInit {

  constructor(public modalRef:BsModalRef,
    private emitterService: EmitterService,
    private alertify:AlertifyService,
    private participantService:ParticipantService) { }

    eventCode:string;
    participant:Participant;
    doCreateAnother:boolean;

    createParticipantForm:FormGroup;
  ngOnInit() {

    log("participant create")
    this.createParticipantForm=new FormGroup({
      name: new FormControl("",Validators.required)
    });

    if(this.participant){
      this.createParticipantForm.get('name').setValue(this.participant.name);
    }
  }

  saveParticipant()
  {
    if(this.createParticipantForm.valid)
    {
      if(this.participant)
      {
        this.updateParticipant();
      }
      else{
        this.createNewParticipant();
      }
    }
    else
    {
      this.alertify.error("Kindly provide necessary field");
    }
  }

  updateParticipant(){
    this.setParticipant();

    this.participantService.updateParticipant(this.participant).subscribe(x =>
      {
        this.emitterService.userUpatedEvent.emit(x);
        this.alertify.success("Participant updated");
        if(this.doCreateAnother)
        {
          this.reset();
        }
        else{
          this.modalRef.hide();
        }
      },error =>{
        this.alertify.error("Unable to update participant");
      })
  }

  createNewParticipant(){

    this.participant={
      name:'',
      eventCode:'',
      isVerify:true,
      id:0,
      referenceNumber:''
    };

    this.setParticipant();

    this.participantService.createParticipant(this.participant).subscribe(x =>
      {
        this.emitterService.userCreatedEvent.emit(x);
        this.alertify.success("Participant added");

        if(this.doCreateAnother)
        {
          this.reset();
        }
        else{
          this.modalRef.hide();
        }
      },error =>{
        this.alertify.error("Unable to create new participant");
      })
  }

  setParticipant(){
    this.participant.name = this.createParticipantForm.get('name').value;
    this.participant.eventCode = this.eventCode;
  }

  reset(){
    this.createParticipantForm.reset();
    this.participant = null;
  }

  cancelClick(){

  }
}
