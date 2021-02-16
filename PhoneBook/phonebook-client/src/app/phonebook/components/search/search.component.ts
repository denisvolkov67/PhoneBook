import { DeleteFavoritesByLoginIdCommand } from './../../model/deleteFavoritesByLoginIdCommand';
import { CreateFavoritesCommand } from './../../model/createFavoritesCommand';
import { FavoritesService } from './../../api/favorites.service';
import { AuthService } from './../../api/auth.service';
import { Employee } from './../../model/employee';
import { EmployeesService } from './../../api/employees.service';
import { switchMap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  employees: Employee[] = [];
  logged: boolean;
  login: string;

  constructor(private route: ActivatedRoute, private employeesService: EmployeesService, private authService: AuthService,
              private router: Router, private favoritesService: FavoritesService) {

    this.updateComponent();
    this.search();
    this.authService.getName().subscribe(account => {
      this.login = account;
    });
  }

  ngOnInit() {
  }

  search() {
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

  addFavoritesClick(id: number){
    const favoriteAdd: CreateFavoritesCommand = {
        login: this.login,
        employeeId: id
    };

    this.favoritesService.favoritesCreateFavorites(favoriteAdd)
    .subscribe(result =>
      this.search()),
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
      this.search()),
    (err: HttpErrorResponse) => {
      return console.log(err.error);
    };

  }

}
