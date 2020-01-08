import { UserService } from './phonebook/api/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Phone Book';
  phoneBookGroup: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {
    this.phoneBookGroup = this.fb.group({
      search: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit() {}

  onSubmit(form: FormGroup) {
    console.log(form.value.search);
    this.userService.userGetUsersByName(form.value.search).subscribe(r =>  {
      console.log(r);
      this.router.navigate(['/users', form.value.search]);
    });
  }
}
