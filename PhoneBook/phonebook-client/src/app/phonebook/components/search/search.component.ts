import { Employee } from './../../model/employee';
import { EmployeesService } from './../../api/employees.service';
import { switchMap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  employees: Employee[] = [];

  constructor(private route: ActivatedRoute, private employeesService: EmployeesService) {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.employeesService.employeesGetEmployeesByName(m.get('name'));
      })
    )
    .subscribe(result => {
        this.employees = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

  ngOnInit() {
  }

}
