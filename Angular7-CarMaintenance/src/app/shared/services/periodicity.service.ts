import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PeriodicityService {
  readonly  BaseURI = "http://localhost:52672/api";

  constructor(private http: HttpClient) { }
  getCarPeriodicity(){
    return this.http.get(this.BaseURI+'/Periodicity/GetCarsPeriodicityByUserId');
  }

  updateCarPeriodicity(resource){
    var body = {
      CarId: resource.carId,
      RevisionKm: resource.revisionKm,
      RevisionMonths: resource.revisionMonths,
      PtiMonths: resource.ptiMonths,
      VigMonths: resource.vigMonths
    }
    return this.http.post(this.BaseURI + "/Periodicity/UpdatePeriodicity", body);
  }
}
