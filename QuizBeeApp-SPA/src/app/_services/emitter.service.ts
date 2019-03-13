import { Injectable, EventEmitter } from '@angular/core';
import { Question } from '../_model/question';

@Injectable({
  providedIn: 'root'
})
export class EmitterService {

public questionCreatedEvent = new EventEmitter();
public questionUpdatedEvent = new EventEmitter();
public questionSelectedEvent= new EventEmitter();
public questionActiveEvent = new EventEmitter();

//signal R related
public questionReceivedEvent = new EventEmitter<Question>();
public questionTimerStartedEvent= new EventEmitter();
public questionAnswerDisplayedEvent= new EventEmitter();
public questionCancelledEvent= new EventEmitter();
public evaluatioPeriodStartedEvent= new EventEmitter();
public ItemToVerifyReceiveEvent= new EventEmitter();
public verificationReceiveEvent = new EventEmitter<boolean>();

public userUpatedEvent = new EventEmitter();
public userCreatedEvent = new EventEmitter();

public judgeUpatedEvent = new EventEmitter();
public judgeCreatedEvent = new EventEmitter();

public eventUpatedEvent = new EventEmitter();
public eventCreatedEvent = new EventEmitter();
public eventDeletedEvent = new EventEmitter();



constructor() { }

}
