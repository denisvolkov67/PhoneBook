import { switchMap } from 'rxjs/operators';
import { FavoritesService } from './../../api/favorites.service';
import { AuthService } from './../../api/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Favorites } from '../../model/favorites';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  login: string;
  favorites: Favorites[];

  constructor(private favoritesService: FavoritesService, private authService: AuthService,
    private route: ActivatedRoute, private router: Router) {
    authService.getName().subscribe(account => {
      this.login = account;
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const login = params.get('login');
      if (login) {
        this.getFavorites(login);
      }
    });
  }

  getFavorites(login: string) {
    this.favoritesService.favoritesGetFavoritesByLogin(login)
      .subscribe(result =>
        this.favorites = result,
        (err: any) => console.log(err)
      );
  }

  deleteButtonClick(id: number){
    this.favoritesService.favoritesDeleteFavoritesById(id)
    .subscribe( resilt =>
      this.getFavorites(this.login),
      (err: any) => console.log(err)
    )};

}
