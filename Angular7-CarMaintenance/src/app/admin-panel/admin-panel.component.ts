import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { DxDataGridComponent } from 'devextreme-angular';
import * as $ from "jquery" ;

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styles: []
})


export class AdminPanelComponent implements OnInit {
  constructor(private router: Router, private service: UserService, private toastr: ToastrService) { }
  customers;
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent
  ngOnInit(): void {
    this.service.getCustomers().subscribe(
      res => {
         this.customers = res;
         this.dataGrid.instance.option("errorRowEnabled", false);
      },
      err =>{
        this.toastr.error("Error on getting customers")
        console.log(err);
      }
    )
  }

  onRowRemoving(e) {
    let d = $.Deferred();  
    this.service.removeCustomer(e.key.id).subscribe(
      res => {
        this.toastr.success("Customer " + e.key.userName + " successfully deleted.")
        d.resolve();  
      },
      err =>{
        this.toastr.error("Error on deleting customer")
        console.log(err);
        d.reject();
      }
    );
    e.cancel = d.promise();
  }

}
