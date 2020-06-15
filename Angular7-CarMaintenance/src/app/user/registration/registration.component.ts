import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['../user.component.css'],
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.formModel.reset();
  }

  onSubmit(){
    this.service.register().subscribe(
      (res:any) => {
        if(res.succeeded){
    this.toastr.success('New user created', 'Registration successful.')
          this.service.formModel.reset();
        }else{
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error("Username is already taken", "Registration failed.")
                break;
            
              default:
                this.toastr.error(element.description, 'Registration failed.')
                break;
            }
          });
        }
      },
      err => {
        this.toatr.error("Registration failed.")
        console.log(err);
      } 
    )
  }
}
