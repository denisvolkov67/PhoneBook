import { HttpErrorResponse } from '@angular/common/http';
import { Employee } from './../../model/employee';
import { Component, OnInit, ViewChild } from '@angular/core';
import {  Validators, FormBuilder  } from '@angular/forms';
import { EmployeesService } from '../../api/api';

@Component({
  selector: 'app-employee-import',
  templateUrl: './employee-import.component.html',
  styleUrls: ['./employee-import.component.scss']
})

export class EmployeeImportComponent implements OnInit {
  @ViewChild('deleteInput', { static: true }) deleteInput;
  @ViewChild('newInput', { static: true }) newInput;

  saveFileForm: any;
  employees: Employee[] = [];
  successResult: boolean = false;

  constructor(private service: EmployeesService, private formBuilder: FormBuilder) {}
  ngOnInit(): void {
    this.saveFileForm = this.formBuilder.group({
    });
  }

  onExpSubmit() {
      this.successResult = false;
      debugger;
      if (this.saveFileForm.invalid) {
          return;
      }
      let formData = new FormData();
      formData.append('DeleteUpload', this.deleteInput.nativeElement.files[0]);
      formData.append('NewUpload', this.newInput.nativeElement.files[0]);
      this.service.importFile(formData)
      .subscribe(result => {
        this.employees = result;
        if (this.employees.length == 0) {
          this.successResult = true;
        }
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }
}
