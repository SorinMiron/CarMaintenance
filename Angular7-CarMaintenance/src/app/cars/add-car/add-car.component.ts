import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { NgbModal  }from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styles: []
})
export class AddCarComponent implements OnInit, OnChanges {

  carYearIsNotSet: boolean;
  CarActualKilometersIsNumerical: boolean = true;
  currentDate = {
    year :  new Date().getFullYear(),
    month :  new Date().getMonth(),
    day :  new Date().getDay()
  };
  constructor(private fb:FormBuilder, private modalService: NgbModal) { 
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
    var body = {
      CarName: this.formModel.value.CarName,
      CarDetails: this.formModel.value.CarDetails,
      CarYear: this.formModel.value.CarYear.getFullYear(),
      CarActualKilometers: this.formModel.value.CarActualKilometers,
      CarLastRevision: this.formModel.value.CarLastRevision,
      CarLastPti: this.formModel.value.CarLastPti,
      CarLastVig: this.formModel.value.CarLastVig
    }
    this.modalService.dismissAll();
    this.formModel.reset();
    //todo send data to server
    console.log(body);
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
