import { Department } from './../../model/department';
import { DepartmentsService } from './../../api/departments.service';
import { HttpErrorResponse } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
import { EmployeesService } from './../../api/employees.service';
import { Employee } from './../../model/employee';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {
  employees: Employee[] = [];
  departments: Department[] = [];

  constructor(private route: ActivatedRoute, private employeesService: EmployeesService, private departmentsService: DepartmentsService) {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.departmentsService.departmentsGetSubsidiaryDepartments(m.get('id'));
      })
    )
    .subscribe(result => {
        this.departments = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );

    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.employeesService.employeesGetEmployeesByDepartmentId(m.get('id'));
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
