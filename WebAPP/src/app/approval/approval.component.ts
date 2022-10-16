import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { HRHiringService } from '../vacancy/hrhiring.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-approval',
  templateUrl: './approval.component.html',
  styleUrls: ['./approval.component.css']
})
export class ApprovalComponent implements OnInit {
  applicantList: any;
  showApprovalOneButton: boolean = false;
  showApprovalTwoButton: boolean = false;
  constructor(private formBuilder: FormBuilder, private _hrhiringservice: HRHiringService, private router: Router,
    private jwtHelper: JwtHelperService, private toastr: ToastrService) { }

  role: string = "";
  ngOnInit(): void {
    const token: any = localStorage.getItem("jwt");
    this.jwtHelper.decodeToken(token).Role
    let role = this.jwtHelper.decodeToken(token).Role;
    this.showApprovalOneButton = false;
    this.showApprovalTwoButton = false;

    if (role == "manager") {
      this.showApprovalOneButton = true;
    }
    else if (role == "director") {
      this.showApprovalTwoButton = true;
    }

    this.GetApplicant(0);
  }


  public GetApplicant(id: any) {

    this._hrhiringservice.GetApplicant(id)
      .subscribe((responseData: any) => {
        this.applicantList = responseData;
      });
  }

  public Approve(vac: any) {
    
    Swal.fire({
      title: "Are you sure to Approve ?",
      text: "After final approval all candidate will be makred as rejected.",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, Want to continue!",
    }).then((result) => {
      if (result.value) {
        var approval: any = {
          VacancyId: vac.vacancyId,
          CandidateID: vac.id
        }
        this._hrhiringservice.GiveApproval(approval)
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
    });
  }
  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/Login"]);
  }


}
