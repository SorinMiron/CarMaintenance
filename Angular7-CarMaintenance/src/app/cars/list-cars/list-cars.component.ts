import { Component, OnInit, ViewChild } from '@angular/core';
import { CarService } from 'src/app/shared/car.service';
import * as $ from "jquery" ;
import { ToastrService } from 'ngx-toastr';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-list-cars',
  templateUrl: './list-cars.component.html',
  styles: []
})
export class ListCarsComponent implements OnInit{
  cars;
  constructor(private service:CarService, private toastr: ToastrService) {
   }
  
  ngOnInit(): void {
    this.service.getCars().subscribe(
      res => {
         this.cars = res;
      },
      err =>{
        this.toastr.error("Error on getting cars");
        console.log(err);
      }
    )
  }

  onRowRemoving(e) {
    let d = $.Deferred();  
    this.service.removeCar(e.key.id).subscribe(
      res => {
        this.toastr.success("Car " + e.key.name + " successfully deleted.")
        d.resolve();  
      },
      err =>{
        this.toastr.error("Error on deleting car.")
        console.log(err);
        d.reject();
      }
    );
    e.cancel = d.promise();
  }
}
