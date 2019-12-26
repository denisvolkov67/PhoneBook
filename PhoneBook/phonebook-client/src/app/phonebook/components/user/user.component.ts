import { User } from './../../model/user';
import { UserService } from './../../api/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user?: User;

  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.userService.userGetUserByLogin(m.get('login'));
      })
    )
    .subscribe(result => {
        this.user = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

  ngOnInit() {
  }

}
