import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { QuizbeeEvent } from '../_model/quizbeeEvent';

@Injectable({
  providedIn: 'root'
})
export class EventService {
baseUrl = environment.apiUrl + "admin/";
constructor(private httpClient: HttpClient) { }

getEvents():Observable<QuizbeeEvent[]>{
  return this.httpClient.get<QuizbeeEvent[]>(this.baseUrl + 'event');
}

getEvent(id:number):Observable<QuizbeeEvent>{
  return this.httpClient.get<QuizbeeEvent>(this.baseUrl + 'event/'+id);
}

createEvent(event:QuizbeeEvent):Observable<QuizbeeEvent>{
  return this.httpClient.post<QuizbeeEvent>(this.baseUrl+'event',event);
}

updateEvent(eventId:number,event:QuizbeeEvent):Observable<QuizbeeEvent>{
  return this.httpClient.put<QuizbeeEvent>(this.baseUrl+'event/'+eventId,event);
}

deleteEvent(eventId:number):Observable<boolean>{
  return this.httpClient.delete<boolean>(this.baseUrl+'event/'+eventId);
}

}
