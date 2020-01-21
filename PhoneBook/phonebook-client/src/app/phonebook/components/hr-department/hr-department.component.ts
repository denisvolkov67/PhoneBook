import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from './../../api/user.service';
import { User } from './../../model/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hr-department',
  templateUrl: './hr-department.component.html',
  styleUrls: ['./hr-department.component.scss']
})
export class HrDepartmentComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService) {
    this.userService.userGetUsersFromHRDepartment().subscribe(x => {
      this.users = x;
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    });
  }

  ngOnInit() {
  }

}
