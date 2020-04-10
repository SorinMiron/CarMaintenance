import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NavbarComponent } from 'src/app/shared/navbar.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [],
  providers: [NavbarComponent]
})
export class LoginComponent implements OnInit {
formModel={
  UserName : '',
  Password : ''
}
  constructor(private service:UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    if(localStorage.getItem("token") != null){
      this.router.navigateByUrl('/home')
    }
  }

  onSubmit(form:NgForm){
    this.service.login(form.value).subscribe(
      (res:any)=>{
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role);
        this.router.navigateByUrl("/home");
      },
      err => {
        if(err.status == 400){
          this.toastr.error('Incorrect username or password', 'Authentication failed');
        }
        else{
          this.toastr.error('Error on login', 'Authentication failed');
          console.log(err);
        }
      }
    );
  }
}
