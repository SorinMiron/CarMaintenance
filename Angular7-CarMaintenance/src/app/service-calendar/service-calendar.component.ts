import { Component, OnInit } from '@angular/core';
import { ServiceCalendarService } from '../shared/services/service-calendar.service';

@Component({
  selector: 'app-service-calendar',
  templateUrl: './service-calendar.component.html',
  styleUrls: ['./service-calendar.component.css']
})
export class ServiceCalendarComponent implements OnInit {

  constructor(private service:ServiceCalendarService) { }

  ngOnInit(): void {
      this.service.getCarServiceCalendar().subscribe(
        res => {console.log(res)},
        err => {console.log(err)}
      )
  }

}
