import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServiceCalendarService {

  readonly  BaseURI = "http://localhost:52672/api";

  constructor(private http: HttpClient) { }
  getCarServiceCalendar(){
    return this.http.get(this.BaseURI+'/ServiceCalendar/GetServiceCalendar');
  }
}
