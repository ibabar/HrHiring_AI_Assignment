import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { HRHiringService } from './hrhiring.service';

@Component({
  selector: 'app-vacancy',
  templateUrl: './vacancy.component.html',
  styleUrls: ['./vacancy.component.css']
})
export class VacancyComponent implements OnInit {

  saveVacancy: any;
  vacancyList: any;
  submitted = false;

  constructor(private formBuilder: FormBuilder, private _hrhiringservice: HRHiringService, private router: Router,
    private jwtHelper: JwtHelperService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.vacancyList = [];
    this.saveVacancy = this.formBuilder.group({
      Id: [0, null],
      OpenPosition: ['', Validators.required],
      ApprovalOne: [0, null],
      ApprovalTwo: [0, null],
      ApprovedCandidate: [0, null]
    });
    this.GetVacancy(0);
  }

  get f() {
    return this.saveVacancy.controls;
  }

  public SaveVacancy() {
    this.submitted = true;
    if (this.saveVacancy.invalid) {
      this.toastr.error("please fill all the required fields", 'Required');
      return;
    }



    this._hrhiringservice.CreateVacancy(this.saveVacancy.value)
      .subscribe((responseData: any) => {
        if (responseData.isSuccess) {
          this.toastr.success(responseData.messsage, 'Success');
          this.GetVacancy(0);

        }
        else {
          this.toastr.error(responseData.messsage, 'Error');
        }
      });

  }

  public GetVacancy(id: any) {

    this._hrhiringservice.GetVacancy(id)
      .subscribe((responseData: any) => {
        this.vacancyList = responseData;
      });
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/Login"]);
  }
}
