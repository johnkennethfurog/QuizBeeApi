import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { log } from 'util';
import { Question } from '../_model/question';
import { EmitterService } from './emitter.service';
import { ItemToVerify } from '../_model/itemToVerify';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

private hubConnection : signalR.HubConnection

baseUrl = environment.apiUrl + "admin/";

constructor(private emitter:EmitterService) { }

public startConnection= () =>{
  this.hubConnection = new signalR.HubConnectionBuilder()
                      .withUrl(environment.apiUrlRoot+"broadcast")
                      .build();
  this.hubConnection
  .start()
  .then(()=> log("hub connection started"))
  .catch(err => log("error connecting to hub : " + err ));
}

public receiveQuestipnListener = () => {
  this.hubConnection.on('receiveQuestion', (question:Question) => {
    log("data reveiced: " + question.question);
    this.emitter.questionReceivedEvent.emit(question);
  });
}

public timerStartListner = () => {
  this.hubConnection.on('startTimer', () => {
    log("timer started");
    this.emitter.questionTimerStartedEvent.emit();
  });
}

public displayAnswerListner = () => {
  this.hubConnection.on('showAnswer', () => {
    log("answer displayed");
    this.emitter.questionAnswerDisplayedEvent.emit();
  });
}

public cancelQuestionListener = () => {
  this.hubConnection.on('cancelQuestion', () => {
    log("quetion cancelled");
    this.emitter.questionCancelledEvent.emit();
  });
}

public evaluationPeriodListener = () => {
  this.hubConnection.on('startEvaluationPeriod', () => {
    log("evaluation period start");
    this.emitter.evaluatioPeriodStartedEvent.emit();
  });
}

public receiveItemToVerifyListener = () => {
  this.hubConnection.on('broadcastVerification', (itemToVerify:ItemToVerify[]) => {
    log("ite mto verify:" + itemToVerify.length);
    this.emitter.ItemToVerifyReceiveEvent.emit(itemToVerify);
  });
}

public receiveVerificationEventListener = () => {
  this.hubConnection.on('verificationEvent', (isNew:boolean) => {
    this.emitter.verificationReceiveEvent.emit(isNew);
  });
}

}
