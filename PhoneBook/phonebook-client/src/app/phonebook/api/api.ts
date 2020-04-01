export * from './auth.service';
import { AuthService } from './auth.service';
export * from './departments.service';
import { DepartmentsService } from './departments.service';
export * from './employees.service';
import { EmployeesService } from './employees.service';
export const APIS = [AuthService, DepartmentsService, EmployeesService];
