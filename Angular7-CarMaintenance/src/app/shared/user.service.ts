import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http:HttpClient) { }
  readonly  BaseURI = "http://localhost:52672/api";
  formModel = this.fb.group({
    UserName : ['', Validators.required],
    Email : ['', Validators.email],
    FullName : [''],
    Passwords : this.fb.group({
      Password : ['', [Validators.required, Validators.minLength(6)]],
      ConfirmPassword : ['',  Validators.required]
    }, {validator : this.comparePasswords})


  })

  comparePasswords(fb:FormGroup){
    let confirmPasswordControl = fb.get('ConfirmPassword');
    
    if(confirmPasswordControl.errors == null || 'passwordMismatch' in confirmPasswordControl.errors ){
      if(fb.get('Password').value != confirmPasswordControl.value ){
        confirmPasswordControl.setErrors({passwordMismatch : true})
      }
      else
      {
        confirmPasswordControl.setErrors(null);
      }

    }
  }

  register(){
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    }
    return this.http.post(this.BaseURI + "/ApplicationUser/Register", body);
  }

  login(formData){
    return this.http.post(this.BaseURI + "/ApplicationUser/Login", formData);
  }

  getUserProfile(){
    var tokenHeader = new HttpHeaders({'Authorization':'Bearer ' + localStorage.getItem('token')})
    return this.http.get(this.BaseURI+'/UserProfile', {headers: tokenHeader})
  }
}
