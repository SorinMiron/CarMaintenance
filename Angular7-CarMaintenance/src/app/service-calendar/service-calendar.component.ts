import { Component, OnInit } from '@angular/core';
import { ServiceCalendarService } from '../shared/services/service-calendar.service';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';

var periodicityNotSet = "Periodicity not set";

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
          console.log("get data");
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
      var nextRevisionKm = this.mapKm(item.nextRevisionKm);
      var nextRevisionDate = this.mapDate(item.nextRevisionDate);
      var nextPti = this.mapDate(item.nextPti);
      var nextVig = this.mapDate(item.nextVig);
      var nextInsurance = this.mapDate(item.nextInsurance);
      var remainingCalendar = {}
      var remainingRevisionKm = this.mapKm(item.remainingCalendar.remainingRevisionKm);
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

  mapKm(stringValue){
    if (stringValue == null){
      return periodicityNotSet;
    }
    if(stringValue <= 500 && stringValue >=0){
      return stringValue + " (Soon)";
    }
    if(stringValue < 0 ){
      return stringValue + " (Expired)"
    }
    return stringValue;
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
    if(stringValue <= 30 && stringValue > 0){
      return stringValue + " (Soon)";
    }
    if(stringValue == 0){
      return stringValue + " (Today)";
    }
    if(stringValue < 0){
      return stringValue + " (Expired)"
    }
    return stringValue;
  
  } 

  getStyleByTextValue(textValue){
    textValue = textValue.toString();
    if(textValue.includes('(Soon)'))
    {
      return{
        'color': '#ffcc00',
        'font-weight':  'bold'
      }
    }
    else if(textValue.includes('(Expired)') || textValue.includes('(Today)'))
    {
      return {
        'color' : '#dd0000',
        'font-weight':  'bold'
      }
    }
      return;
  }
 
}


