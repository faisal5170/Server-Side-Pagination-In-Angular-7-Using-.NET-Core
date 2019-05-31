import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { User } from './models/user';
import { Observable } from 'rxjs';

@Injectable()
export class AppService {

    private apiURL: string = 'http://localhost:49469/api/';

    constructor(private http: HttpClient) {

    }

    getAllEmployees(): Observable<User[]> {
        return this.http.get<User[]>(this.apiURL + 'DatatableApi');        
    }

    getAllEmployeesWithPaging(dtParams: any): Observable<any> {
        return this.http.put(this.apiURL + 'DatatableApi', dtParams);        
    }
}
