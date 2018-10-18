import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs';


@Injectable()
export class BaseService {
    constructor(private http: Http) { }

    /**
     * Base method to get all
     * @param url Url of the endpoint
     */
    protected getAll<T>(url: string, handleData: boolean = true): Observable<T> {
        let request;
        this.http.get(url, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base method to get by id
     * @param url Url of the endpoint
     * @param id Identifier of the entity to find
     */
    protected getById<T>(url: string, id: number | string, handleData: boolean = true): Observable<T> {
        let request;
        this.http.get(url + id, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base method to get by id (using JSON.parse(JSON.stringify) on the result)
     * @param url Url of the endpoint
     * @param id Identifier of the entity to find
     */
    protected getByIdWithJSONStr<T>(url: string, id: number | string, handleData: boolean = true): Observable<T> {
        let request;
        this.http.get(url + id, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base post entity
     * @param url Url endpoint to post
     * @param body Entity to create
     */
    protected post<T>(url: string, body: T, handleData: boolean = true): Observable<T> {
        let request;
        this.http.post(url, body, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base edit entity
     * @param url Url endpoint to put
     * @param body Entity to update
     */
    protected update<T>(url: string, body: T, handleData: boolean = true): Observable<T> {
        let request;
        this.http.put(url + body["id"], body, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base patch entity
     * @param url Url endpoint to patch
     * @param body Properties to update
     * @param handleData 
     */
    protected patch<T>(url: string, body: T, handleData: boolean = true): Observable<T> {
        let request;
        this.http.patch(url, body, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base delete entity
     * @param url Url endpoint to delete
     * @param id Identitfier of the entity to delete
     */
    protected delete<T>(url: string, id: number, handleData: boolean = false): Observable<T> {
        let request;
        this.http.delete(url + id, this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    /**
     * Base delete entity without id
     * @param url Url endpoint to delete
     */
    protected deleteWithoutId<T>(url: string, handleData: boolean = false): Observable<T> {
        let request;
        this.http.delete(url , this.jwt()).subscribe(res => request = res, error => this.handleError(error));

        return request;
    }

    private jwt() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Access-Control-Allow-Origin', '*');
        return new RequestOptions({ headers: headers, withCredentials: true });
    }

    protected handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            errMsg = `${error.status} - ${error.statusText || ''}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.log(errMsg);
        return Observable.throw(errMsg);
    };
}