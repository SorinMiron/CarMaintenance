import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
userDetails;
  constructor(private service: UserService, private toastr: ToastrService ) { }

  ngOnInit(): void {
   this.service.getUserProfile().subscribe(
     res => {
        this.userDetails = res;
     },
     err =>{
       this.toastr.error("Error on getting user infos.")
       console.log(err);
     }
   )
  }
  
}
