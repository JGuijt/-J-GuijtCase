import { Injectable } from '@angular/core';
import { Cursus } from './cursus';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class CursusService {

  constructor(private http: HttpClient) { }

private cursusUrl = 'https://localhost:44305/api/CursusDb';


  getCursus(): Observable<Cursus[]> {

    return this.http.get<Cursus[]>(this.cursusUrl);    
    
  }

  

}
