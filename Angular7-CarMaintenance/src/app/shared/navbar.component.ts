import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: []
})



export class NavbarComponent implements OnInit {
  role: string;
  constructor(private router: Router ) { }
  ngOnInit(): void {
    this.role = localStorage.getItem("role");
  }
  onLogout(){
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    this.router.navigate(['/user/login']);
  }
}
