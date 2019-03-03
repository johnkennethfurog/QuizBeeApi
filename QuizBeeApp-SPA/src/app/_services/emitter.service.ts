import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmitterService {

public questionCreatedEvent = new EventEmitter();
public questionUpdatedEvent = new EventEmitter();

public userUpatedEvent = new EventEmitter();
public userCreatedEvent = new EventEmitter();

public judgeUpatedEvent = new EventEmitter();
public judgeCreatedEvent = new EventEmitter();

public eventUpatedEvent = new EventEmitter();
public eventCreatedEvent = new EventEmitter();
public eventDeletedEvent = new EventEmitter();

constructor() { }

}
