import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styles: []
})
export class AdminPanelComponent implements OnInit {
  constructor(private router: Router, private service: UserService, private toastr: ToastrService) { }
  customers;
  ngOnInit(): void {
    this.service.getCustomers().subscribe(
      res => {
         this.customers = res;
      },
      err =>{
        this.toastr.error("Error on getting customers")
        console.log(err);
      }
    )
  }

}
