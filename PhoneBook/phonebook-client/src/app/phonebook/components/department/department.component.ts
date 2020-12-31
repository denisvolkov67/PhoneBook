import { AuthService } from './../../api/auth.service';
import { Department } from './../../model/department';
import { DepartmentsService } from './../../api/departments.service';
import { HttpErrorResponse } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
import { EmployeesService } from './../../api/employees.service';
import { Employee } from './../../model/employee';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {
  employees: Employee[] = [];
  departments: Department[] = [];
  department: Department;
  logged: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService,
              private departmentsService: DepartmentsService, private employeesService: EmployeesService)   {

    this.updateComponent();
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.departmentsService.departmentsGetDepartmentById(m.get('id'));
      })
    )
    .subscribe(result => {
        this.department = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );

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

  editButtonClick(employeeId: number) {
    this.router.navigate(['/employee/edit', employeeId]);
  }

  updateComponent() {
    this.authService.getRole()
    .subscribe(result => {
      this.logged = JSON.parse(result);
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    }
    );
  }

}

