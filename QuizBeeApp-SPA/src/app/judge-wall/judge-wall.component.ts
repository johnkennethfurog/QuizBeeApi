import { Component, OnInit } from '@angular/core';
import { JudgeService } from '../_services/judge.service';
import { ItemToVerify } from '../_model/itemToVerify';
import { AlertifyService } from '../_services/alertify.service';
import { EmitterService } from '../_services/emitter.service';
import { SignalRService } from '../_services/signal-r.service';
import { JudgeVerdict } from '../_model/judgeVerdict';
import { log } from 'util';

@Component({
  selector: 'app-judge-wall',
  templateUrl: './judge-wall.component.html',
  styleUrls: ['./judge-wall.component.css']
})
export class JudgeWallComponent implements OnInit {

  itemsToVerify:ItemToVerify[];
  judgeId:number;

  constructor(private judgeService:JudgeService,
    private alertify:AlertifyService,
    private emitter:EmitterService,
    private signalR:SignalRService) { }

  ngOnInit() {

    this.signalR.startConnection();
    this.signalR.receiveItemToVerifyListener();

    this.judgeId = 3;
    this.LoadItemsToVerify();

    this.subscribeToEmitter();
  }

  subscribeToEmitter(){
    this.emitter.ItemToVerifyReceiveEvent.subscribe((items:ItemToVerify[]) =>{
      var item = items.find(x => x.judge == this.judgeId);
      this.itemsToVerify.push(item);
    });
  }

  LoadItemsToVerify(){
    this.judgeService.getItemsToVerify(this.judgeId).subscribe(items =>{
      this.itemsToVerify = items;
    },error =>{
      this.alertify.error("Unable to load items");
    })
  }

  verifyItem(itemToVerify:ItemToVerify,isTrue:boolean){
    var verdict: JudgeVerdict;
    verdict = {
      id:itemToVerify.id,
      status: isTrue ? 1 : 2,
      participantAnswer: itemToVerify.participantAnswer
    }

    log(""+itemToVerify.participantAnswer);

    this.judgeService.verifyItem(verdict).subscribe(()=>
    {
      var ind = this.itemsToVerify.indexOf(itemToVerify);
      this.alertify.success("Item verified");
      this.itemsToVerify.splice(ind,1);
    },error =>{
      this.alertify.error("Unable to verify item");
    });
  }
}
