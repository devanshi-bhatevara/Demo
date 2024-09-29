import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Demo } from '../models/demo.model';
import { ApiResponse } from '../models/ApiResponse{T}';
import { AddDemo } from '../models/addDemo.model';

@Injectable({
  providedIn: 'root'
})
export class DemoService {

  private apiUrl= "http://localhost:5292/api/";

  constructor(private http:HttpClient) 
  { }

  getAll() : Observable<ApiResponse<Demo[]>>{
    return this.http.get<ApiResponse<Demo[]>>(this.apiUrl+'Demo/GetAll');
  }

  add(add: AddDemo): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(`${this.apiUrl}Demo/Create`, add);
  }

  delete(id: number): Observable<ApiResponse<Demo>> {
    return this.http.delete<ApiResponse<Demo>>(`${this.apiUrl}Demo/Remove/${id}`);
  }

  getById(id: number): Observable<ApiResponse<Demo>> {
    return this.http.get<ApiResponse<Demo>>(`${this.apiUrl}Demo/GetDemoById/${id}`)
  }

  modify(edit: Demo): Observable<ApiResponse<Demo>> {
    return this.http.put<ApiResponse<Demo>>(`${this.apiUrl}Demo/Modify`, edit);
  }

}
