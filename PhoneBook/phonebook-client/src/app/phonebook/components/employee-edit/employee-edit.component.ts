import { AuthService } from './../../api/auth.service';
import { Employee } from './../../model/employee';
import { HttpErrorResponse } from '@angular/common/http';
import { EmployeesService } from './../../api/employees.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.scss']
})
export class EmployeeEditComponent implements OnInit {
  employeerGroup: FormGroup;
  error: string;
  logged: boolean;

  constructor(private employeesService: EmployeesService, private fb: FormBuilder, private route: ActivatedRoute,
              private router: Router, private authService: AuthService) {

    this.updateComponent();
    this.employeerGroup = this.fb.group({
      id: [0, Validators.required],
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

    this.route.paramMap.subscribe(params => {
      const empId = +params.get('id');
      if (empId) {
        this.getEmployee(empId);
      }
    });
  }

  getEmployee(id: number) {
    this.employeesService.employeesGetEmployeeById(id)
      .subscribe(
        (employee: Employee) => this.editEmployee(employee),
        (err: any) => console.log(err)
      );
  }

  editEmployee(employee: Employee) {
    this.employeerGroup.patchValue({
      id: employee.id,
      name: employee.name,
      position: employee.position,
      telephone: employee.telephone,
      mobile: employee.mobile,
      office: employee.office,
      email: employee.email,
      departmentId: employee.departmentId
    });
  }

  onSubmit(form: FormGroup) {
    this.employeesService.employeesEditEmployee(form.value).subscribe(c => {
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

