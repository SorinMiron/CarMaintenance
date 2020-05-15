import { Component, OnInit } from '@angular/core';
import { ServiceCalendarService } from '../shared/services/service-calendar.service';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { Timestamp } from 'rxjs/internal/operators/timestamp';

var periodicityNotSet = "Periodicity not set";
var referenceDate = "1/1/0001 12:00:00 AM"
@Component({
  selector: 'app-service-calendar',
  templateUrl: './service-calendar.component.html',
  styleUrls: ['./service-calendar.component.css']
})
export class ServiceCalendarComponent implements OnInit {

  constructor(private service:ServiceCalendarService, private toastr: ToastrService) { }
  serviceCalendars: any;
  ngOnInit(): void {
      this.service.getCarServiceCalendar().subscribe(
        res => { 
          this.serviceCalendars = this.mapData(res);
          
        },
        err => {
          this.toastr.error("Error on getting car calendars.");
          console.log(err);
        })
  }
  mapData(res)
  {
    var serviceCalendars = [];
    res.forEach(item => {
      var carCalendar;
      var carNameAndYear = item.carNameAndYear;
      var actualKilometers = item.actualKilometers;
      var nextRevisionKm = item.nextRevisionKm;
      var nextRevisionDate = this.mapDate(item.nextRevisionDate);
      var nextPti = this.mapDate(item.nextPti);
      var nextVig = this.mapDate(item.nextVig);
      var nextInsurance = this.mapDate(item.nextInsurance);
      var remainingCalendar = {}
      var remainingRevisionKm = item.remainingCalendar.remainingRevisionKm;
      var remainingRevisionDays = this.mapDays(item.remainingCalendar.remainingRevisionDays);
      var remainingPtiDays = this.mapDays(item.remainingCalendar.remainingPtiDays);
      var remainingVigDays = this.mapDays(item.remainingCalendar.remainingVigDays);
      var remainingInsuranceDays = this.mapDays(item.remainingCalendar.remainingInsuranceDays);
      remainingCalendar = {
        remainingRevisionKm : remainingRevisionKm,
        remainingRevisionDays : remainingRevisionDays,
        remainingPtiDays : remainingPtiDays,
        remainingVigDays : remainingVigDays,
        remainingInsuranceDays : remainingInsuranceDays
      }
      carCalendar = {
        carNameAndYear : carNameAndYear,
        actualKilometers : actualKilometers,
        nextRevisionKm : nextRevisionKm,
        nextRevisionDate : nextRevisionDate,
        nextPti : nextPti,
        nextVig: nextVig,
        nextInsurance : nextInsurance,
        remainingCalendar : remainingCalendar
      }
      serviceCalendars.push(carCalendar)
    })
  return serviceCalendars;
  }

  mapDate(stringValue){
    if (stringValue == null){
      return periodicityNotSet;
    }
    return moment(stringValue).format("DD-MM-YYYY");
  }

  mapDays(stringValue){
    if (stringValue == null){
      return periodicityNotSet;
    }
    return stringValue;
  
  }
}


