import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from './../../api/user.service';
import { User } from './../../model/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  users: User[] = [];

  constructor(private userService: UserService) {
    this.userService.userGetUsersFromAdministrativeStaff().subscribe(x => {
      this.users = x;
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    });
  }

  ngOnInit() {
  }

}
