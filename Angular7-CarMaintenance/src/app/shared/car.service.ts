import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http:HttpClient) { }
  readonly  BaseURI = "http://localhost:52672/api";

  insertCar(carDetails:Object){
    return this.http.post(this.BaseURI+'/Car/InsertCar', carDetails);
  }
}
