import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmitterService {

public questionCreatedEvent = new EventEmitter();
public questionUpdatedEvent = new EventEmitter();

constructor() { }

}
