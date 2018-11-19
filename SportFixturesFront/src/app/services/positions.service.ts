import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { PositionsTable } from '../shared/interfaces/positions-table';
import { BaseService } from './base.service';
import { AppSettings } from '../config/appSettings';

@Injectable()
export class PositionsService extends BaseService {

    constructor(protected _http: Http) {
        super(_http);
    }

    public getPositions(sportId: string) {
        return this.getAll(AppSettings.ApiEndpoints.getPositions + sportId)
            .then(res => <PositionsTable[]>res.data)
            .then(data => { return data; });
    }
}