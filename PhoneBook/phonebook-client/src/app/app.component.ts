import { EmployeesService } from './phonebook/api/employees.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Phone Book';
  phoneBookGroup: FormGroup;
  faSearch = faSearch;

  constructor(private fb: FormBuilder, private employeesService: EmployeesService, private router: Router) {
    this.phoneBookGroup = this.fb.group({
      search: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit() {}

  onSubmit(form: FormGroup) {
    console.log(form.value.search);
    this.employeesService.employeesGetEmployeesByName(form.value.search).subscribe(r =>  {
      console.log(r);
      this.router.navigate(['/employees', form.value.search]);
    });
  }
}
