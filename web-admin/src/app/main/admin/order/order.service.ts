import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = environment.apiUrl;
  
  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  add(order: any): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}/order/add`, order, this.httpOptions);   
  }

  getList(): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/order/list`, this.httpOptions);   
  }
}