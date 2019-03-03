import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Judge } from '../_model/judge';

@Injectable({
  providedIn: 'root'
})
export class JudgeService {

  baseUrl = environment.apiUrl + "judge/";
  constructor(private httpClient:HttpClient) { }
  
  verify(judgeId:number):Observable<boolean>{
    return this.httpClient.put<boolean>(this.baseUrl+'verify/'+judgeId,{});
  }
  
  createJudge(judge:Judge):Observable<Judge>{
    return this.httpClient.post<Judge>(this.baseUrl+'admin',judge);
  }
  
  updateJudge(judge:Judge):Observable<Judge>{
    return this.httpClient.put<Judge>(this.baseUrl+'admin',judge);
  }
  
  deleteJudge(judgeId:number):Observable<boolean>{
    return this.httpClient.delete<boolean>(this.baseUrl+'admin/'+judgeId);
  }

}
