import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { HRHiringService } from '../vacancy/hrhiring.service';

@Component({
  selector: 'app-applicant',
  templateUrl: './applicant.component.html',
  styleUrls: ['./applicant.component.css']
})
export class ApplicantComponent implements OnInit {

  saveApplicant: any;
  applicantList: any;
  vacancyList: any;
  submitted = false;
  constructor(private formBuilder: FormBuilder, private _hrhiringservice: HRHiringService, private router: Router,
    private jwtHelper: JwtHelperService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.applicantList = [];
    this.vacancyList = [];
    this.saveApplicant = this.formBuilder.group({
      Id: [0, null],
      Name: ['', Validators.required],
      Email: ['', Validators.required],
      MobileNumber: ['', Validators.required],
      Resume: ['', null],
      VacancyId: ['', Validators.required],
      VacancyName: ['', null],
      ImgPath: ['', Validators.required]
    });
    this.GetApplicant(0);
    this.GetVacancy(0);
  }

  get f() {
    return this.saveApplicant.controls;
  }

  public GetVacancy(id: any) {
    this._hrhiringservice.GetOpenVacancy(id)
      .subscribe((responseData: any) => {
        responseData.forEach((item: any) => {
          this.vacancyList.push({ Value: item.id, Text: item.openPosition });
        });
      });
  }
  public SaveApplicant() {
    this.submitted = true;
    if (this.saveApplicant.invalid) {
      this.toastr.error("please fill all the required fields", 'Required');
      return;
    }

    // imgPath: this.response.dbPat



    this._hrhiringservice.AddCandidate(this.saveApplicant.value)
      .subscribe((responseData: any) => {
        if (responseData.isSuccess) {
          this.toastr.success(responseData.messsage, 'Success');
          this.GetApplicant(0);

        }
        else {
          this.toastr.error(responseData.messsage, 'Error');
        }
      });

  }

  public GetApplicant(id: any) {

    this._hrhiringservice.GetApplicant(id)
      .subscribe((responseData: any) => {
        this.applicantList = responseData;
      });
  }

  response: any = { dbPath: '' };
  uploadFinished = (event: any) => {
    this.response = event;
    this.saveApplicant.patchValue({ ImgPath: this.response.dbPath });
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/Login"]);
  }

}
