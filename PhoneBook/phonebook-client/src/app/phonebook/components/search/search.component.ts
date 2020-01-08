import { switchMap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from './../../api/user.service';
import { User } from './../../model/user';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  users: User[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.userService.userGetUsersByName(m.get('name'));
      })
    )
    .subscribe(result => {
        this.users = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

  ngOnInit() {
  }

}
