export * from './departments.service';
import { DepartmentsService } from './departments.service';
export * from './employees.service';
import { EmployeesService } from './employees.service';
export * from './user.service';
import { UserService } from './user.service';
export const APIS = [DepartmentsService, EmployeesService, UserService];
