import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  validationErrors: string[] = [];

  constructor(private http: HttpClient)
  {

  }

  ngOnInit(): void {
  }

  get404Error()
  {
    this.http.get(this.baseUrl + 'bugs/not-found').subscribe({
      next: resp => {console.log(resp)},
      error: error => {console.log(error)}
    })
  }

  get400Error()
  {
    this.http.get(this.baseUrl + 'bugs/bad-request').subscribe({
      next: resp => {console.log(resp)},
      error: error => {console.log(error)}
    })
  }

  get500Error()
  {
    this.http.get(this.baseUrl + 'bugs/server-error').subscribe({
      next: resp => {console.log(resp)},
      error: error => {console.log(error)}
    })
  }

  get401Error()
  {
    this.http.get(this.baseUrl + 'bugs/auth').subscribe({
      next: next => {console.log(next)},
      error: error => {console.log(error)}
    })
  }

  get400ValidationError()
  {
    this.http.get(this.baseUrl + 'account/register', {}).subscribe({
      next: resp => {console.log(resp);},
      error: error => {
        console.log(error);
        this.validationErrors = error;
      }
    })
  }
}
