import { AuthService } from './phonebook/api/auth.service';
import { EmployeesService } from './phonebook/api/employees.service';
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
  login: string;

  constructor(private fb: FormBuilder, private employeesService: EmployeesService, private router: Router,
              private authService: AuthService) {
    this.phoneBookGroup = this.fb.group({
      search: ['', [Validators.required, Validators.minLength(3)]]
    });

    authService.getName().subscribe(account => {
      this.login = account;
    });
  }


  ngOnInit() {}

  onSubmit(form: FormGroup) {
    this.employeesService.employeesGetEmployeesByName(form.value.search).subscribe(r =>  {
      this.router.navigate(['/employees', form.value.search]);
    });
  }
}
