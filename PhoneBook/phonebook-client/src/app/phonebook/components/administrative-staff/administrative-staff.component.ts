import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from './../../api/user.service';
import { User } from './../../model/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-administrative-staff',
  templateUrl: './administrative-staff.component.html',
  styleUrls: ['./administrative-staff.component.scss']
})
export class AdministrativeStaffComponent implements OnInit {

  users: User[] = [];
  showBlock = false;

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
