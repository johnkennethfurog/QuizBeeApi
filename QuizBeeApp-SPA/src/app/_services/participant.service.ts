import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Participant } from '../_model/participant';

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {
baseUrl = environment.apiUrl + "participant/";
constructor(private httpClient:HttpClient) { }

verify(participantId:number):Observable<boolean>{
  return this.httpClient.put<boolean>(this.baseUrl+'verify/'+participantId,{});
}

createParticipant(participant:Participant):Observable<Participant>{
  return this.httpClient.post<Participant>(this.baseUrl+'admin',participant);
}

updateParticipant(participant:Participant):Observable<Participant>{
  return this.httpClient.put<Participant>(this.baseUrl+'admin',participant);
}

deleteParticipant(participantId:number):Observable<boolean>{
  return this.httpClient.delete<boolean>(this.baseUrl+'admin/'+participantId);
}

}
