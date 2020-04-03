import { environment } from './../../../environments/environment';
/**
 * Phone Book
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 *
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { CreateEmployeeCommand } from '../model/createEmployeeCommand';
import { Employee } from '../model/employee';
import { UpdateEmployeeCommand } from '../model/updateEmployeeCommand';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class EmployeesService {

    protected basePath = environment.base_url;
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     *
     *
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public employeesCreateEmployee(body: CreateEmployeeCommand, observe?: 'body', reportProgress?: boolean): Observable<Employee>;
    public employeesCreateEmployee(body: CreateEmployeeCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Employee>>;
    public employeesCreateEmployee(body: CreateEmployeeCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Employee>>;
    public employeesCreateEmployee(body: CreateEmployeeCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (body === null || body === undefined) {
            throw new Error('Required parameter body was null or undefined when calling employeesCreateEmployee.');
        }

        let headers = this.defaultHeaders;

        // authentication (oauth) required
        this.configuration.accessToken = this.getAccessToken();
        if (this.configuration.accessToken) {
            headers = headers.set('Authorization', 'Bearer ' + this.configuration.accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<Employee>('post',`${this.basePath}/employee`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public employeesEditEmployee(body: UpdateEmployeeCommand, observe?: 'body', reportProgress?: boolean): Observable<UpdateEmployeeCommand>;
    public employeesEditEmployee(body: UpdateEmployeeCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UpdateEmployeeCommand>>;
    public employeesEditEmployee(body: UpdateEmployeeCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UpdateEmployeeCommand>>;
    public employeesEditEmployee(body: UpdateEmployeeCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (body === null || body === undefined) {
            throw new Error('Required parameter body was null or undefined when calling employeesEditEmployee.');
        }

        let headers = this.defaultHeaders;

        // authentication (oauth) required
        this.configuration.accessToken = this.getAccessToken();
        if (this.configuration.accessToken) {
            headers = headers.set('Authorization', 'Bearer ' + this.configuration.accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<UpdateEmployeeCommand>('post',`${this.basePath}/employee/update`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param id
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public employeesGetEmployeeById(id: number, observe?: 'body', reportProgress?: boolean): Observable<Employee>;
    public employeesGetEmployeeById(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Employee>>;
    public employeesGetEmployeeById(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Employee>>;
    public employeesGetEmployeeById(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling employeesGetEmployeeById.');
        }

        let headers = this.defaultHeaders;

        // authentication (oauth) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Employee>('get',`${this.basePath}/employee/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param id
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public employeesGetEmployeesByDepartmentId(id: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Employee>>;
    public employeesGetEmployeesByDepartmentId(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Employee>>>;
    public employeesGetEmployeesByDepartmentId(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Employee>>>;
    public employeesGetEmployeesByDepartmentId(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling employeesGetEmployeesByDepartmentId.');
        }

        let headers = this.defaultHeaders;

        // authentication (oauth) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<Employee>>('get',`${this.basePath}/employees/department/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param name
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public employeesGetEmployeesByName(name: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Employee>>;
    public employeesGetEmployeesByName(name: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Employee>>>;
    public employeesGetEmployeesByName(name: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Employee>>>;
    public employeesGetEmployeesByName(name: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (name === null || name === undefined) {
            throw new Error('Required parameter name was null or undefined when calling employeesGetEmployeesByName.');
        }

        let headers = this.defaultHeaders;

        // authentication (oauth) required
        if (this.configuration.accessToken) {
            const accessToken = typeof this.configuration.accessToken === 'function'
                ? this.configuration.accessToken()
                : this.configuration.accessToken;
            headers = headers.set('Authorization', 'Bearer ' + accessToken);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<Employee>>('get',`${this.basePath}/employees/${encodeURIComponent(String(name))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    getAccessToken() {
      return sessionStorage.getItem('access_token');
  }
}
