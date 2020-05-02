import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http:HttpClient) { }
  readonly  BaseURI = "http://localhost:52672/api";

  insertCar(carDetails:Object){
    return this.http.post(this.BaseURI+'/Car/InsertCar', carDetails);
  }

  getCars(){
    return this.http.get(this.BaseURI+'/Car/GetCarsByUserId');
  }

  removeCar(carId){
    return this.http.post(this.BaseURI+'/Car/RemoveCar', JSON.stringify(carId), {headers: this.getHeaderForJson()});
  }

  getHeaderForJson(){
    return new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }

}
