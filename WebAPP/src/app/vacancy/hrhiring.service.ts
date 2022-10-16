import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Applicant, Position, Products } from '../Models/Products';
import configurl from '../../assets/config/config.json'


@Injectable({
  providedIn: 'root'
})
export class HRHiringService {

  url = configurl.apiServer.url + '/api/hiring/';
  constructor(private http: HttpClient) { }

  CreateVacancy(position: Position): Observable<Position> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Position>(this.url + 'CreateVacancy', position, httpHeaders);
  }

  GetVacancy(id = 0): Observable<Response> {
    return this.http.get<Response>(this.url + "GetVacancy/" + id);
  }

  GetOpenVacancy(id = 0): Observable<Response> {
    return this.http.get<Response>(this.url + "GetOpenVacancy/" + id);
  }

  AddCandidate(position: Applicant): Observable<Applicant> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Applicant>(this.url + 'AddCandidate', position, httpHeaders);
  }

  GetApplicant(id = 0): Observable<Response> {
    return this.http.get<Response>(this.url + "GetApplicant/" + id);
  }

  GiveApproval(position: Applicant): Observable<Applicant> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Applicant>(this.url + 'GiveApproval', position, httpHeaders);
  }

  getProductList(): Observable<Products[]> {
    return this.http.get<Products[]>(this.url + 'ProductsList');
  }
  postProductData(productData: Products): Observable<Products> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Products>(this.url + 'CreateProduct', productData, httpHeaders);
  }
  updateProduct(product: Products): Observable<Products> {
    const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
    return this.http.post<Products>(this.url + 'UpdateProduct?id=' + product.productId, product, httpHeaders);
  }
  deleteProductById(id: number): Observable<number> {
    return this.http.post<number>(this.url + 'DeleteProduct?id=' + id, null);
  }
  getProductDetailsById(id: string): Observable<Products> {
    return this.http.get<Products>(this.url + 'ProductDetail?id=' + id);
  }

}
