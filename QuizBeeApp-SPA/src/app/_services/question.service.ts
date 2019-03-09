import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Question } from '../_model/question';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

baseUrl = environment.apiUrl + "admin/";

constructor(private httpClient: HttpClient) { }

createQuestion(questionForm:Question):Observable<Question>{
  return this.httpClient.post<Question>(this.baseUrl+'question',questionForm);
}

updateQuestion(questionForm:Question,questionId:number):Observable<Question>{
  return this.httpClient.put<Question>(this.baseUrl+'question/'+questionId,questionForm);
}

delteQuestion(questionId:number):Observable<boolean>{
  return this.httpClient.delete<boolean>(this.baseUrl+'question/'+questionId);
}

broadcastQuestion(question:Question){
  return this.httpClient.post(this.baseUrl+'broadcastQuestion',question);
}

startTimer(){
  return this.httpClient.get(this.baseUrl+'startTimer');
}

showAnswer(){
  return this.httpClient.get(this.baseUrl+'showAnswer');
}

cancel(){
  return this.httpClient.get(this.baseUrl+'cancelQuestion');
}

startEvaluationPeriod(){
  return this.httpClient.get(this.baseUrl+'startEvaluationPeriod');
}

}
