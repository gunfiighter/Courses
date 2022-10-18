import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {AccountService} from "../_services/account.service";
import {Observable} from "rxjs";
import {User} from "../_models/user";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {  }

  login()
  {
    this.accountService.login(this.model).subscribe({
      next:
        response => {
          this.router.navigateByUrl("/members");
      },
      error: error => {
        console.log(error);
        this.toastr.error(error.error);
      }
    });
  }

  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
