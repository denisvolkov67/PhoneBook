export * from './auth.service';
import { AuthService } from './auth.service';
export * from './departments.service';
import { DepartmentsService } from './departments.service';
export * from './employees.service';
import { EmployeesService } from './employees.service';
export * from './favorites.service';
import { FavoritesService } from './favorites.service';
export const APIS = [AuthService, DepartmentsService, EmployeesService, FavoritesService];
