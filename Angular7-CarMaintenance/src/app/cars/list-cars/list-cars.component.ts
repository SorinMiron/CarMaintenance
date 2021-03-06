import { Component, OnInit, OnDestroy } from '@angular/core';
import * as $ from "jquery" ;
import { ToastrService } from 'ngx-toastr';
import { Router, NavigationEnd } from '@angular/router';
import { CarService } from 'src/app/shared/services/car.service';

@Component({
  selector: 'app-list-cars',
  templateUrl: './list-cars.component.html',
  styleUrls: ["./list-cars.component.css"]
})
export class ListCarsComponent implements OnInit, OnDestroy{
  numericalPattern = "^\\d+$";
  currentDate = new Date();
  cars;
  mySubscription: any;
  constructor(private service:CarService, private toastr: ToastrService, private router: Router) {
  
this.router.routeReuseStrategy.shouldReuseRoute = function () {
  return false;
};
this.mySubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        // Trick the Router into believing it's last link wasn't previously loaded
        this.router.navigated = false;
      }
    });
  }

  ngOnInit(): void {
    this.currentDate.setDate(new Date().getDate()  + 1)
    this.initCars();
  }

  initCars(){
    this.service.getCars().subscribe(
      res => {
         this.cars = res;
         this.cars.forEach((item)=>{
           item.lastRevision = new Date(item.lastRevision);
           item.lastPti= new Date(item.lastPti);
           item.lastVig = new Date(item.lastVig);
           item.lastInsurance = new Date(item.lastInsurance);
         })
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
  onRowUpdating($event){
    let d = $.Deferred();  
    this.service.updateCar({...$event.oldData, ...$event.newData}).subscribe(
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
  
ngOnDestroy() {
  if (this.mySubscription) {
    this.mySubscription.unsubscribe();
  }
}

}
