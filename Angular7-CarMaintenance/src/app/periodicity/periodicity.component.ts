import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import * as $ from "jquery" ;
import { PeriodicityService } from '../shared/services/periodicity.service';
@Component({
  selector: 'app-periodicity',
  templateUrl: './periodicity.component.html',
  styleUrls: ['./periodicity.component.css']
})

export class PeriodicityComponent implements OnInit {
  carsPeriodicity;

  numericalPatternMax50000 = "^([1-8][0-9]{3}|9[0-8][0-9]{2}|99[0-8][0-9]|999[0-9]|[1-4][0-9]{4}|50000)$";
  numericalPatternMax36 = "^([1-9]|[12][0-9]|3[0-6])$";
  constructor(private service: PeriodicityService, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.initCars();
  }
  initCars(){
    this.service.getCarPeriodicity().subscribe(
      res => {
        this.carsPeriodicity = res;
      },
      err =>{
        this.toastr.error("Error on getting cars periodicity");
        console.log(err);
      }
    )
  }

  onRowUpdating($event){
    let d = $.Deferred();  
    this.service.updateCarPeriodicity({...$event.oldData, ...$event.newData}).subscribe(
      res => {
        this.toastr.success("Car updated successfully");
        d.resolve();  
      },
      err =>{
        this.toastr.error("Error on updating cars");
        console.log(err);
        d.reject();
      }
    );
    $event.cancel = d.promise();
  }
  
}


