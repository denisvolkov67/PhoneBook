import { ActivatedRoute } from '@angular/router';
import { EmployeesService } from './../../api/employees.service';
import { Employee } from './../../model/employee';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee-info',
  templateUrl: './employee-info.component.html',
  styleUrls: ['./employee-info.component.scss']
})
export class EmployeeInfoComponent implements OnInit {
  employee: Employee;

  constructor(private employeesService: EmployeesService, private route: ActivatedRoute) { }

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
      .subscribe(result =>
        this.employee = result,
        (err: any) => console.log(err)
      );
  }

}
