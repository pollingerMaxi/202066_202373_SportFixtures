import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class BaseService {
    constructor(private http: Http) { }

    /**
     * Base method to get all
     * @param url Url of the endpoint
     */
    protected async getAll(url: string) {
        let request = await this.http.get(url, this.jwt()).toPromise();
        return this.extractData(request);
    }

    /**
     * Base method to get by id
     * @param url Url of the endpoint
     * @param id Identifier of the entity to find
     */
    protected async getById(url: string, id: number | string) {
        let request = await this.http.get(url + id, this.jwt()).toPromise();
        return this.extractData(request);
    }

    /**
     * Base post entity
     * @param url Url endpoint to post
     * @param body Entity to create
     */
    protected async post(url: string, body: any) {
        let request = await this.http.post(url, body, this.jwt()).toPromise();
        return this.extractData(request);
    }

    /**
     * Base edit entity
     * @param url Url endpoint to put
     * @param body Entity to update
     */
    protected async update(url: string, body: any) {
        let request = await this.http.put(url + body["id"], body, this.jwt()).toPromise();
        return this.extractData(request);
    }

    /**
     * Base patch entity
     * @param url Url endpoint to patch
     * @param body Properties to update
     * @param handleData 
     */
    protected async patch(url: string, body: any) {
        let request = await this.http.patch(url, body, this.jwt()).toPromise();
        return this.extractData(request);
    }

    /**
     * Base delete entity
     * @param url Url endpoint to delete
     * @param id Identitfier of the entity to delete
     */
    protected async delete(url: string, id: number) {
        let request = await this.http.delete(url + id, this.jwt()).toPromise();
        return this.extractData(request);
    }

    private jwt() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Access-Control-Allow-Origin', '*');
        return new RequestOptions({ headers: headers, withCredentials: true });
    }

    protected extractData<T>(res: any): T {
        let body = <T>res.json();
        return body || <T>{};
    };
}