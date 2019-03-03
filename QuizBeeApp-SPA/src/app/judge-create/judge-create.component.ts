import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { EmitterService } from '../_services/emitter.service';
import { AlertifyService } from '../_services/alertify.service';
import { JudgeService } from '../_services/judge.service';
import { Judge } from '../_model/judge';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { log } from 'util';

@Component({
  selector: 'app-judge-create',
  templateUrl: './judge-create.component.html',
  styleUrls: ['./judge-create.component.css']
})
export class JudgeCreateComponent implements OnInit {

  constructor(public modalRef:BsModalRef,
    private emitterService: EmitterService,
    private alertify:AlertifyService,
    private judgeService:JudgeService) { }

    eventCode:string;
    judge:Judge;
    doCreateAnother:boolean;

    createJudgeForm:FormGroup;
    ngOnInit() {

      log("Judge create")
      this.createJudgeForm=new FormGroup({
        name: new FormControl("",Validators.required),
        emailAddress: new FormControl("",Validators.required)
      });

      if(this.judge){
        this.createJudgeForm.get('name').setValue(this.judge.name);
        this.createJudgeForm.get('emailAddress').setValue(this.judge.emailAddress);
      }
    }

    saveJudge()
    {
      if(this.createJudgeForm.valid)
      {
        if(this.judge)
        {
          this.updateJudge();
        }
        else{
          this.createNewJudge();
        }
      }
      else
      {
        this.alertify.error("Kindly provide necessary field");
      }
    }

    updateJudge(){
      this.setJudge();

      this.judgeService.updateJudge(this.judge).subscribe(x =>
        {
          this.emitterService.judgeUpatedEvent.emit(x);
          this.alertify.success("Judge updated");
          if(this.doCreateAnother)
          {
            this.reset();
          }
          else{
            this.modalRef.hide();
          }
        },error =>{
          this.alertify.error("Unable to update Judge");
        })
    }

    createNewJudge(){

      this.judge={
        name:'',
        eventCode:'',
        isVerify:true,
        emailAddress:'',
        isHead:false,
        id:0
      };

      this.setJudge();

      this.judgeService.createJudge(this.judge).subscribe(x =>
        {
          this.emitterService.judgeCreatedEvent.emit(x);
          this.alertify.success("Judge added");
          if(this.doCreateAnother)
          {
            this.reset();
          }
          else{
            this.modalRef.hide();
          }
        },error =>{
          this.alertify.error("Unable to create new Judge");
        })
    }

    setJudge(){
      this.judge.name = this.createJudgeForm.get('name').value;
      this.judge.eventCode = this.eventCode;
      this.judge.emailAddress = this.createJudgeForm.get('emailAddress').value;
    }

    reset(){
      this.createJudgeForm.reset();
      this.judge = null;
    }

    cancelClick(){

    }

}
