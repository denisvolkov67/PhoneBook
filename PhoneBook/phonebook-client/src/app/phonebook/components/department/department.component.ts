import { DeleteFavoritesByLoginIdCommand } from './../../model/deleteFavoritesByLoginIdCommand';
import { CreateFavoritesCommand } from './../../model/createFavoritesCommand';
import { FavoritesService } from './../../api/favorites.service';
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
  prevDepartmentLink: string;
  logged: boolean;
  login: string;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService,
              private departmentsService: DepartmentsService, private employeesService: EmployeesService,
              private favoritesService: FavoritesService)   {

    this.getRole();

    this.getDepartment();

    this.getPreviousDepartment();

    this.getSubsidiaryDepartments();

    this.getEmployeesByDepartmentId();

    this.authService.getName().subscribe(account => {
      this.login = account;
    });
  }

  ngOnInit() {
  }

  editButtonClick(employeeId: number) {
    this.router.navigate(['/employee/edit', employeeId]);
  }

  getRole() {
    this.authService.getRole()
    .subscribe(result => {
      this.logged = JSON.parse(result);
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    }
    );
  }

  getDepartment(){
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.departmentsService.departmentsGetDepartmentById(m.get('id'));
      })
    )
    .subscribe(result => {
        this.department = result;
        this.getPreviousDepartment();
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

  getPreviousDepartment() {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.departmentsService.departmentsGetPreviousDepartment(m.get('id'));
      })
    )
    .subscribe(result => {
        this.prevDepartmentLink = "/department/" + result.id;
      },
      (err: HttpErrorResponse) => {
        this.prevDepartmentLink = "/home";
        return console.log(err.error);
      }
    );
  }

  getSubsidiaryDepartments() {
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
  }

  getEmployeesByDepartmentId() {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        return this.employeesService.employeesGetEmployeesByDepartmentId(m.get('id'));
      })
    )
    .subscribe(result => {
        this.employees = result;
      });
  }

  addFavoritesClick(id: number){
    const favoriteAdd: CreateFavoritesCommand = {
        login: this.login,
        employeeId: id
    };

    this.favoritesService.favoritesCreateFavorites(favoriteAdd)
    .subscribe(result =>
      this.getEmployeesByDepartmentId()),
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    };

  }

  deleteFavoritesClick(id: number){
    const favoriteDelete: DeleteFavoritesByLoginIdCommand = {
        login: this.login,
        employeeId: id
    };

    this.favoritesService.favoritesDeleteFavoritesByLoginId(favoriteDelete)
    .subscribe(result =>
      this.getEmployeesByDepartmentId()),
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    };
  }
}
