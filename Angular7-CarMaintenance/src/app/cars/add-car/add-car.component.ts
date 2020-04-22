import { Component, OnInit } from '@angular/core';
import { NgbModal  }from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styles: []
})
export class AddCarComponent implements OnInit {

  constructor(private fb:FormBuilder, private modalService: NgbModal) { 
  }
  visible = true;
  formModel = this.fb.group({
    // CarYear : [{year:2020, month: 6, day: 11}],
    CarName : [],
    CarDetails: [],
    CarYear : [],
    CarActualKilometers: [],
    CarLastRevision: [],
    CarLastPti:[],
    CarLastVig:[]
  })
  
  ngOnInit(): void {

  }
  onSubmit(){
    var body = {
      CarName: this.formModel.value.CarName,
      CarDetails: this.formModel.value.CarDetails,
      CarYear: this.formModel.value.CarYear.getFullYear(),
      CarActualKilometers: this.formModel.value.CarActualKilometers,
      CarLastRevision: this.formModel.value.CarLastRevision,
      CarLastPti: this.formModel.value.CarLastPti,
      CarLastVig: this.formModel.value.CarLastVig
    }
    console.log(body);
  }

  open(content) {
    this.modalService.open(content);
  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
}
