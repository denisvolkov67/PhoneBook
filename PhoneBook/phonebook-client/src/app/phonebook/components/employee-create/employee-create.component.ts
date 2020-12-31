import { AuthService } from './../../api/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeesService } from './../../api/employees.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.scss']
})
export class EmployeeCreateComponent implements OnInit {
  employeerGroup: FormGroup;
  logged: boolean;

  constructor(private employeesService: EmployeesService, private fb: FormBuilder, private route: ActivatedRoute,
              private router: Router, private authService: AuthService) {

    this.updateComponent();
    this.employeerGroup = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(1)]],
      position: ['', [Validators.required, Validators.minLength(1)]],
      telephone: [''],
      mobile: [''],
      office: [''],
      email: [''],
      departmentId: ['', Validators.required]
      });
    }

  ngOnInit() {
    console.log('ngOnInit');
  }


  onSubmit(form: FormGroup) {
    this.employeesService.employeesCreateEmployee(form.value).subscribe(c => {
      this.router.navigate(['/employee', c.id]);
    },
    (err: HttpErrorResponse) => {
      console.log(err);
    }
    );
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
