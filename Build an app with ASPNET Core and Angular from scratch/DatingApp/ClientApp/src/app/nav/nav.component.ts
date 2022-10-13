import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {AccountService} from "../_services/account.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  _loggedIn: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
  login()
  {
    this.accountService.login(this.model).subscribe({
      next:
        response => {
          console.log(response);
          this._loggedIn = true;
      },
      error: error => {
        console.log(error);
      }
    });
  }

  logout()
  {
    this._loggedIn = false;
  }
}
