import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'ProductWebAPP';
  MenueList: any;
  hideAll: boolean = false;

  constructor(private jwtHelper: JwtHelperService, private router: Router) {
  }

  ngOnInit() {
    if (!this.isUserAuthenticated()) {
      this.hideAll = true;
    }
    else {
      this.hideAll = false;
    }

    this.MenueList = [];

    const token: any = localStorage.getItem("jwt");
    debugger;
    if (token) {
      let role = this.jwtHelper.decodeToken(token).Role;

      if (role == "manager") {
        this.MenueList.push({ name: "Approval", route: "/Approval" });
      }
      else if (role == "officer") {
        this.MenueList.push({ name: "Add Vacancy", route: "/Vacancy" });
        this.MenueList.push({ name: "Add Applicatn", route: "/Applicants" });

      }
      else if (role == "director") {
        this.MenueList.push({ name: "Approval", route: "/Approval" });
      }
    }

  }


  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
}

