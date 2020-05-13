import { Component, OnInit } from '@angular/core';
import { ServiceCalendarService } from '../shared/services/service-calendar.service';
import { ToastrService } from 'ngx-toastr';

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
          this.serviceCalendars = res;
          console.log(this.serviceCalendars);
        },
        err => {
          this.toastr.error("Error on getting car calendars.");
          console.log(err);
        })
  }

}
