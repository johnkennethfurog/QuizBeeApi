import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Category } from '../_model/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

baseUrl = environment.apiUrl + "admin/";

constructor(private httpClient: HttpClient) { }

getCategories():Observable<Category[]>{
  return this.httpClient.get<Category[]>(this.baseUrl+'category');
}

}
