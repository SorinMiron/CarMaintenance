import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder) { }

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
    //passwordMismatch
    //confirmPasswordControl.errors={passwordMismatch:true}
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
}
