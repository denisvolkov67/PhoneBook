import { DepartmentsService } from './../../api/departments.service';
import { Department } from './../../model/department';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  departments: Department[] = [];

  constructor(private departmentsService: DepartmentsService) {
    this.departmentsService.departmentsGetTopLevel().subscribe(x => {
      this.departments = x;
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    });
  }


  ngOnInit() {
  }

}
