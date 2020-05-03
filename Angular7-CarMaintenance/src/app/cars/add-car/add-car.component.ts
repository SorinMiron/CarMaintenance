import { Component, OnInit, OnChanges } from '@angular/core';
import { NgbModal  }from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, Validators } from '@angular/forms';
import { CarService } from 'src/app/shared/car.service';
import { ToastrService } from 'ngx-toastr';
import { ListCarsComponent } from '../list-cars/list-cars.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ["./add-car.component.css"],
  providers: [ListCarsComponent]
})
export class AddCarComponent implements OnInit, OnChanges {
  carYearIsNotSet: boolean;
  CarActualKilometersIsNumerical: boolean = true;
  currentDate = {
    year :  new Date().getUTCFullYear(),
    month :  new Date().getUTCMonth() + 1,
    day :  new Date().getUTCDate()
  };
  constructor(private fb:FormBuilder, private modalService: NgbModal , private carService: CarService, private toastr: ToastrService, private listCarComponent: ListCarsComponent, private router: Router) { 
  }
  visible = true;
  formModel = this.fb.group({
    // CarYear : [{year:2020, month: 6, day: 11}],
    CarName : ['', Validators.required],
    // CarDetails is not mandatory
    CarDetails: [],
    //for CarYear validations was added manually
    CarYear : [''],
    CarActualKilometers: ['', Validators.required],
    CarLastRevision: ['', [Validators.required]],
    CarLastPti: ['',[Validators.required]],
    CarLastVig: ['', [Validators.required]]
  })
 
  ngOnInit(): void {
    this.ngOnChanges();
  }

  ngOnChanges() : void{
      this.formModel.get('CarYear').valueChanges.subscribe(value => {
        this.carYearIsNotSet = (value == "" || value == null) ? true : false; 
      });
    }
  onSubmit(){
    if(this.formModel.value.CarYear == "" || this.formModel.value.CarYear == null || !this.CarActualKilometersIsNumerical  || this.formModel.invalid){
      this.carYearIsNotSet = true;

      if(this.formModel.invalid){
        Object.keys(this.formModel.controls).forEach(key => {
          const ctrl = this.formModel.get(key);
          ctrl.markAsTouched({ onlySelf: true });
      });
    }
    return;
  }
    //validate car year > last pti/ last revision / blabla
    var carDetails = {
      Name: this.formModel.value.CarName,
      Details: this.formModel.value.CarDetails,
      Year: this.formModel.value.CarYear.getFullYear(),
      ActualKilometers: this.formModel.value.CarActualKilometers,
      LastRevision: this.formModel.value.CarLastRevision,
      LastPti: this.formModel.value.CarLastPti,
      LastVig: this.formModel.value.CarLastVig
    }
    this.modalService.dismissAll();
    this.formModel.reset();
    
    this.carService.insertCar(carDetails).subscribe(
      res => {
        this.toastr.success("Car successfully added");
        this.listCarComponent.ngOnInit();
        this.router.navigateByUrl("/mycars");
      },
      err =>{
        this.toastr.error("Error on adding car")
        console.log(err);
      }
    )

  }

  open(content) {
    
    this.modalService.open(content);
    this.carYearIsNotSet = false;
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      this.CarActualKilometersIsNumerical = false;
      return false;
    }
    this.CarActualKilometersIsNumerical = true;
    return true;
  }
}
