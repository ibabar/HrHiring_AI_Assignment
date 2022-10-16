import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ProductsComponent } from './products/products.component';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './guards/auth-guard.service';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VacancyComponent } from './vacancy/vacancy.component';
import { ApplicantComponent } from './applicant/applicant.component';
import { ApprovalComponent } from './approval/approval.component';
import { UploadComponent } from './upload/upload.component';

//all components routes
const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'Login', component: LoginComponent },
  { path: 'Home', component: HomepageComponent },
  { path: 'product', component: ProductsComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'Vacancy', component: VacancyComponent, canActivate: [AuthGuard] },
  { path: 'Applicants', component: ApplicantComponent, canActivate: [AuthGuard] },
  { path: 'Approval', component: ApprovalComponent, canActivate: [AuthGuard] },
];

//function is use to get jwt token from local storage
export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    HomepageComponent,
    LoginComponent,
    VacancyComponent,
    ApplicantComponent,
    ApprovalComponent,
    UploadComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7299"],
        disallowedRoutes: []
      }
    }),
    ToastrModule.forRoot()
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
